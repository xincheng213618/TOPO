using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Windows.Media;

namespace BaseUtil
{
    public static class Media
    {
        public static string[] MediaList = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Base\\Media\\");

        public static MediaPlayer mediaPlayer = new MediaPlayer();

        public static bool Player(int listnum = 0)
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
        public static bool Play(string path)
        {
            try
            {
                MediaPlayer media = new MediaPlayer();
                media.Open(new Uri(path, UriKind.RelativeOrAbsolute));
                media.Position = TimeSpan.Zero;
                media.Play();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
