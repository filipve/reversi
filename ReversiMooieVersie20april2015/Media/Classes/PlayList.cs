using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiMooieVersie20april2015.Media.Classes
{
    public class Playlist
    {
        private bool shuffle = false;
        private bool repeat = false;
        private bool paused = false;
        private bool isPlaying = false;

        private ObservableCollection<Song> songs;
        private System.Windows.Media.MediaPlayer player;

        public Playlist(System.Windows.Media.MediaPlayer mediaElement)
        {
            this.songs = new ObservableCollection<Song>();
            this.player = mediaElement;
        }

        public int ActiveIndex { get; set; }

        public ObservableCollection<Song> Songs
        {
            get { return this.songs; }
        }

        public void AddSong(string path)
        {
            if (System.IO.File.Exists(path))
            {
                this.songs.Add(new Song(
                    System.IO.Path.GetFileNameWithoutExtension(path),
                    path));
            }
        }

        public void DeleteSong()
        {
            if (this.songs.Count > this.ActiveIndex)
            {
                this.songs.RemoveAt(this.ActiveIndex);
            }
        }

        public void Play()
        {
            if (!this.paused && this.isPlaying)
            {
                this.Pause();
            }
            else
            {
                if (!this.paused && this.Songs.Count > this.ActiveIndex)
                {
                    Song songToPlay = this.Songs[this.ActiveIndex];
                    this.player.Open(new Uri(songToPlay.Path));
                }
                this.player.Play();
                this.paused = false;
                this.isPlaying = true;
            }
        }

        public void Stop()
        {
            this.player.Stop();
            this.player.Position = new TimeSpan(0);
            this.isPlaying = false;
            this.paused = false;
        }

        public void Pause()
        {
            this.paused = true;
            this.isPlaying = false;
            this.player.Pause();
        }

        public void Next()
        {
            this.Stop();
            this.ActiveIndex++;
            if (this.ActiveIndex >= this.songs.Count)
            {
                this.ActiveIndex = 0;
            }

            if (this.isPlaying)
            {
                this.Play();
            }
        }

        public void Prev()
        {
            this.Stop();
            this.ActiveIndex--;
            if (this.ActiveIndex < 0)
            {
                this.ActiveIndex = this.songs.Count - 1;
            }
            if (this.isPlaying)
            {
                this.Play();
            }
        }

        public void ToggleRepeat()
        {
            this.repeat = !this.repeat;
        }

        public void ToggleShuffle()
        {
            this.shuffle = !this.shuffle;
        }
    }
}