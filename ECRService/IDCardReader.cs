using System;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Forms.Integration;

namespace ECRService
{
    public delegate void  IDCardReadCompletedHandler(object sender, IDCardReadCompletedEventArgs e);

    public class IDCardReadCompletedEventArgs : EventArgs
    {
        public Exception Error { get; }
        public IDCardInfo IDCard { get; }

        public IDCardReadCompletedEventArgs(Exception Error, IDCardInfo IDCard)
        {
            this.Error = Error;
            this.IDCard = IDCard;
        }
    }

    internal class IDCardReadCompletedListener
    {
        public Dispatcher dispatcher { get; }
        public IDCardReadCompletedHandler handler { get; }

        public IDCardReadCompletedListener(Dispatcher dispatcher, IDCardReadCompletedHandler handler)
        {
            this.dispatcher = dispatcher;
            this.handler = handler;
        }
    }

    public class IDCardReaderException : Exception
    {
        public IDCardReaderException(string message) : base(message)
        {
        }
    }

    public class IDCardReader
    {
        static private IDCardReader idCardReader = null;
        private Dispatcher dispatcher = null;
        private DispatcherTimer timer = null;
        private ManualResetEventSlim readyEvent = null;
        private WindowsFormsHost formsHost = null;
        private AxSYNCARDOCXLib.AxSynCardOcx idCardControl = null;

        private IDCardReadCompletedListener listener = null;

        private IDCardReader()
        {
            readyEvent = new ManualResetEventSlim();

            // 启动工作线程
            Thread worker = new Thread(_ => ReadIDCardThreadProc());
            worker.SetApartmentState(ApartmentState.STA);
            worker.Start();
        }

        private void ReadIDCardThreadProc()
        {
            timer = new DispatcherTimer()
            {
                IsEnabled = false,
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += new EventHandler(ReadIDCard);

            formsHost = new WindowsFormsHost();
            idCardControl = new AxSYNCARDOCXLib.AxSynCardOcx();
            idCardControl.BeginInit();
            formsHost.Child = idCardControl;
            idCardControl.EndInit();

            dispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher;
            readyEvent.Set();

            System.Windows.Threading.Dispatcher.Run();
        }

        private void ReadIDCard(object sender, EventArgs e)
        {
            if (listener == null)
                return;

            if (idCardControl.FindReader() <= 0)
            {
                listener.dispatcher.BeginInvoke(new Action(() =>
                {
                    listener.handler(this, new IDCardReadCompletedEventArgs(new IDCardReaderException("无法找到读卡器。"), null));
                }));
                timer.IsEnabled = false;
                return;
            }

            idCardControl.GetSAMID();
            idCardControl.SetPhotoType(1);
            idCardControl.SetPhotoPath(1, "");
            idCardControl.SetPhotoName(2);
            idCardControl.SetReadType(0);
            idCardControl.ReadCardMsg();

            if (File.Exists(idCardControl.CardNo + ".jpeg"))
            {
                timer.IsEnabled = false;

                IDCardInfo idCardInfo = new IDCardInfo();
                idCardInfo.name = idCardControl.NameA;
                idCardInfo.no = idCardControl.CardNo;
                idCardInfo.sex = idCardControl.Sex;
                idCardInfo.mz = idCardControl.Nation;
                idCardInfo.org = idCardControl.Police;
                idCardInfo.birth = idCardControl.Born;
                idCardInfo.valid_begin = idCardControl.UserLifeB;
                idCardInfo.valid_end = idCardControl.UserLifeE;
                idCardInfo.address = idCardControl.Address;

                listener.dispatcher.BeginInvoke(new Action(() => listener.handler(this, new IDCardReadCompletedEventArgs(null, idCardInfo))));
                //listener = null;
            }
        }

        static public IDCardReader GetIDCardReader()
        {
            if (idCardReader == null)
                idCardReader = new IDCardReader();

            return idCardReader;
        }

        public void BeginRead(Dispatcher dispatcher, IDCardReadCompletedHandler handler)
        {
            readyEvent.Wait();

            this.dispatcher.BeginInvoke(new Action(() =>
            {
                listener = new IDCardReadCompletedListener(dispatcher, handler);
                timer.IsEnabled = true;
            }));
        }

        public void EndRead()
        {
            readyEvent.Wait();

            this.dispatcher.BeginInvoke(new Action(() =>
            {
                timer.IsEnabled = false;
                listener = null;
            }));
        }

        public void Exit()
        {
            readyEvent.Wait();
            dispatcher.InvokeShutdown();
        }
    }
}
