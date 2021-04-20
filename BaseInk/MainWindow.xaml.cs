using BaseUtil;
using Microsoft.Ink;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BaseInk
{
    public delegate void Delegate_Ink_Msg(string Msg);

    public static class InkPut
    {
        public static event Delegate_Ink_Msg delegate_Ink_Msg;

        public static void Invoke(string Msg)
        {
            delegate_Ink_Msg?.Invoke(Msg);
        }
    }


    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InkWindows : Window
    {

        public class SelectionsList
        {
            public string Content111 { get; set; }
        }

        private ObservableCollection<SelectionsList> selectionsLists = new ObservableCollection<SelectionsList>();

        public InkWindows()
        {
            Topmost = true;
            InitializeComponent();
        }
        private void Window_MouseDrag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            ListVierew.ItemsSource = selectionsLists;
        }




        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            selectionsLists.Clear();
            inkCanvas.Strokes.Clear();
        }
        


        //修正不直接对页面负责对窗口负责
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            KeyHelper.OnKeyPress(KeyHelper.KeyCode.BACK);
        }

        Timer timer = null;
        private void InkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
            timer = new Timer(_ => Dispatcher.BeginInvoke(new Action(() => HandWriting_Recognize())), null, TimeSpan.FromMilliseconds(100), Timeout.InfiniteTimeSpan);
        }

        private int lastcout = 0;
        private void HandWriting_Recognize()
        {
            timer.Dispose();
            timer = null;
            using (MemoryStream stream = new MemoryStream())
            {
                inkCanvas.Strokes.Save(stream);
                InkCollector inkCollector = new InkCollector();
                Ink ink = new Ink();
                ink.Load(stream.ToArray());
                new Thread(_ =>
                {
                    using (RecognizerContext context = new RecognizerContext())
                    {
                        context.Factoid = Factoid.ChineseSimpleCommon;
                        if (ink.Strokes.Count - lastcout > 10)
                        {
                            lastcout = ink.Strokes.Count;
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                selectionsLists.Clear();
                                inkCanvas.Strokes.Clear();
                            }
                            ));
                        }
                        else
                        {
                            lastcout = ink.Strokes.Count;
                        }
                        if (ink.Strokes.Count > 0 && ink.Strokes.Count < 40)
                        {
                            context.Strokes = ink.Strokes;
                            RecognitionStatus status;
                            var result = context.Recognize(out status);
                            if (status == RecognitionStatus.NoError)
                            {
                                RecognitionAlternates selections = result.GetAlternatesFromSelection();

                                Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    selectionsLists.Clear();
                                    foreach (var item in selections)
                                    {
                                        selectionsLists.Add(new SelectionsList() { Content111 = item.ToString() });
                                    };
                                }));
                            }
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(new Action(() =>
                            {
                                selectionsLists.Clear();
                                inkCanvas.Strokes.Clear();
                            }
                            ));
                        }
                    }
                }).Start();
            }
        }
        private void Fun_Click(object sender, RoutedEventArgs e)
        {
            KeyHelper.HotKey(new List<byte>() { KeyHelper.KeyCode.WinL, KeyHelper.KeyCode.SPACE });
        }


        private void Exit(object sender, RoutedEventArgs e)
        {
            selectionsLists.Clear();
            inkCanvas.Strokes.Clear(); 
            Hide();
        }


        private void ListVierew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView.SelectedIndex > -1)
            {
                InkPut.Invoke(selectionsLists[listView.SelectedIndex].Content111);
                selectionsLists.Clear();
                inkCanvas.Strokes.Clear();
            }
        }


    }
}
