using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiMooieVersie20april2015.Media.Classes
{
    public class SoundManager
    {
        private System.Windows.Media.MediaPlayer _mediaPlayer = new System.Windows.Media.MediaPlayer();

        public Playlist Brain { get; private set; }


        public SoundManager()
        {
            this.Brain = new Playlist(this._mediaPlayer);
            //AddSongs();
            //   LoadSongs();
            //this.Brain.Play();

        }

        public void AddSongs()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "MP3's (*.mp3)|*.mp3|All files (*.*)|*.*";
            dialog.Multiselect = true;


            if (dialog.ShowDialog() == true)
            {
                foreach (string filename in dialog.FileNames)
                {
                    if (System.IO.File.Exists(filename))
                    {
                        this.Brain.AddSong(filename);
                    }
                }
            }
        }

        //leest automatisch alle mp3 songs in de folder Sounds in en speelt ze af
        public void LoadSongs()
        {
            string sound_path = "f:\\documenten\\visual studio 2013\\Projects\\ReversiMooieVersie20april2015\\ReversiMooieVersie20april2015\\Media\\Sounds\\";
            // string[] filePaths = Directory.GetFiles(sound_path, "*.mp3|*.m4a|*.wav",SearchOption.AllDirectories);
            //        string[] filePaths = Directory.GetFiles(sound_path, "*.*", SearchOption.AllDirectories);

            foreach (string filename in Directory.EnumerateFiles(sound_path, "*.mp3", SearchOption.AllDirectories))
            {
                this.Brain.AddSong(filename);
            }
            // returns:
            // "c:\MyDir\my-car.BMP"


        }


        public ObservableCollection<Song> Songs
        {
            get { return this.Brain.Songs; }
        }

        public System.Windows.Media.MediaPlayer Player
        {
            get { return _mediaPlayer; }
            set { _mediaPlayer = value; }
        }

        public void PlaySound()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                _mediaPlayer.Open(new Uri(openFileDialog.FileName));
                _mediaPlayer.Play();


            }



        }

        public void PlaySoundFolder()
        {
            /*
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
            */
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            //  string path = openFileDialog.FileName;
            string path2 = 
                "C:\\Users\\FILIP\\Music\\iTunes\\iTunes Media\\Music\\Compilations\\The Ultimate Best of Queen\\05 Another One Bites the Dust.mp3";
            string pathwav =
                "C:\\Users\\FILIP\\documents\\visual studio 2013\\Projects\\OthelloPropereCode\\OthelloPropereCode\\Sounds\\WeAreTheChampions.wav";
            player.SoundLocation = pathwav;
            player.Load();
            player.Play();

            // _mediaPlayer.Open(_mediaPlayer.Source);
            //_mediaPlayer.Open(new Uri("Another_One_Bites_the_Dust.mp3", UriKind.Relative));
            //   _mediaPlayer.Play();


            //  }

            _mediaPlayer.Open(new Uri(path2));
            _mediaPlayer.Play();



        }

        public void PlaySoundFolder2()
        {

            string path2 =
                "C:\\Users\\FILIP\\Music\\iTunes\\iTunes Media\\Music\\Compilations\\The Ultimate Best of Queen\\05 Another One Bites the Dust.mp3";

            _mediaPlayer.Open(new Uri(path2));
            _mediaPlayer.Play();

        }



        public void PlaySoundIfWon()
        {
            //soundplayer kan alleen maar .wav files afspelen

            // voor mp3 moet je mediaplayer gebruiken

            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            string pathwav =
                "C:\\Users\\FILIP\\documents\\visual studio 2013\\Projects\\OthelloPropereCode\\OthelloPropereCode\\Sounds\\VictorySong\\WeAreTheChampions.wav";
            player.SoundLocation = pathwav;
            player.Load();
            player.Play();

        }

        ///inspiratie https://msdn.microsoft.com/en-us/library/bb383890(v=vs.90).aspx
        public void PlaySoundIfWonmp3()
        {
            //soundplayer kan alleen maar .wav files afspelen

            // voor mp3 moet je mediaplayer gebruiken



            string pathmp3 =
                "C:\\Users\\FILIP\\documents\\visual studio 2013\\Projects\\OthelloPropereCode\\OthelloPropereCode\\Sounds\\VictorySong\\WeAreTheChampions.mp3";
            _mediaPlayer.Open(new Uri(pathmp3));
            _mediaPlayer.Play();
        }

        public void PlaySoundOnStartUp()
        {

            // string path ="C:\\Users\\FILIP\\documents\\visual studio 2013\\Projects\\OthelloPropereCode\\OthelloPropereCode\\Sounds\\VictorySong\\RequiemForADreamOriginalSong.m4a";
            string path =
               "C:\\Users\\FILIP\\documents\\visual studio 2013\\Projects\\OthelloPropereCode\\OthelloPropereCode\\Sounds\\Songs\\AnotherOneBitestheDust.mp3";

            _mediaPlayer.Open(new Uri(path));

            // playList.Enqueue(new Uri("C:\\Users\\FILIP\\documents\\visual studio 2013\\Projects\\OthelloPropereCode\\OthelloPropereCode\\Sounds\\Songs\\AnotherOneBitestheDust.mp3"));
            //playList.Enqueue(new Uri("C:\\Users\\FILIP\\documents\\visual studio 2013\\Projects\\OthelloPropereCode\\OthelloPropereCode\\Sounds\\Songs\\WeAreTheChampions.mp3"));



        }


        public void StopMediaPlayer()
        {
            _mediaPlayer.Stop();
        }




    }
}