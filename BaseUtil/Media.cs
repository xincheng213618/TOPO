using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BaseUtil
{
    public static class Media
    {
        public static string[] MediaList = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Base\\Media\\");

        public static MediaPlayer mediaPlayer = new MediaPlayer();

        public static bool Player(int listnum = 0)
        {
            try
            {
                if (MediaList.Length > listnum)
                {
                    string path = MediaList[listnum];
                    mediaPlayer.Open(new Uri(path, UriKind.RelativeOrAbsolute));
                    mediaPlayer.Position = TimeSpan.Zero;
                    mediaPlayer.Play();
                }
                return MediaList.Length > listnum;
            }
            catch
            {
                return false;
            }
        }

        public static void Play(string path)
        {
            try
            {
                mediaPlayer.Open(new Uri(path, UriKind.RelativeOrAbsolute));
                mediaPlayer.Position = TimeSpan.Zero;
                mediaPlayer.Play();
            }
            catch
            {
                
            }

        }

    }
}
