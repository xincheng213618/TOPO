using System.Windows.Forms;

namespace BaseUtil
{
    public class XFile
    {          
        public static string OpenFileDialog(string Filter = "所有文件(*.*)|*.*")
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,//该值确定是否可以选择多个文件
                Title = "请选择文件",
                Filter = Filter,
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;      
            }
            return null;
        }

        public static string FolderBrowserDialog()
        {
            string choice;
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show("文件夹路径不能为空", "提示");
                    return null;
                }
                choice = dialog.SelectedPath + "\\";
                return choice;
            }
            return null;
        }

        public static string SaveFileDialog(string DefaultFileName = null, string Filter = "", int FilterIndex = 1)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = Filter,
                FileName = DefaultFileName,
                FilterIndex = FilterIndex,
                RestoreDirectory = true,
                AddExtension = true
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.FileName))
                {
                    MessageBox.Show("文件名称不能为空", "提示");
                    return null;
                }
                return dialog.FileName;
            }
            return null;
        }

    }
}
