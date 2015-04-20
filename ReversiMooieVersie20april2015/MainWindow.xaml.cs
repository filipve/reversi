using ReversiMooieVersie20april2015.Model;
using ReversiMooieVersie20april2015.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

namespace ReversiMooieVersie20april2015
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameViewModel game;
        public MainWindow()
        {
            InitializeComponent();
            // GameViewModel game = new GameViewModel();
            // this.DataContext = game;

            game = (this.DataContext as GameViewModel);


            MakeGrid();
            game.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(game_PropertyChanged);
            game.Queue.OnTurnSkipped += (o, e) => MessageBox.Show("Turn skipped!");
            game.Game.GameOver += (o, e) => MessageBox.Show("Game over!");
            UpdateGrid();
            UpdateGrid();
        }

        void game_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Game")
            {
                UpdateGrid();
            }
        }

        private void UpdateGrid()
        {
            var board = game.Game;
            var black = game.Black;
            var white = game.White;

            foreach (UIElement item in g.Children)
            {
                int x = Grid.GetColumn(item);
                int y = Grid.GetRow(item);

                if (item is Ellipse)
                {
                    var el = item as Ellipse;

                    if (board[x, y] == null)
                    {
                        if (game.Game.CanPlayThere(board.CurrentPlayer, x, y))
                        {
                            el.Fill = board.CurrentPlayer == black ? Brushes.Black : Brushes.White;
                            el.Opacity = 0.4;
                        }
                        else
                        {
                            el.Fill = null;
                            el.Opacity = 0;
                        }
                    }
                    else
                    {
                        el.Opacity = 1;
                        if (board[x, y] == black)
                        {
                            el.Fill = Brushes.Black;
                        }
                        else if (board[x, y] == white)
                        {
                            el.Fill = Brushes.White;
                        }
                    }
                }

            }
        }

        private void MakeGrid()
        {
            var creator = new GridCreator();

            //grid tekenen lukt al, hier blijven we dus af
            creator.SetupGrid(g, 8, 8);

            creator.FillWithEllipseWithEvent(g, t_MouseLeftButtonUp, Ellipse_IsMouseDirectlyOverChanged);
            // creator.FillWithEllipseWithEvent(g);

        }

        void Ellipse_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var tok = sender as Ellipse;
            if (tok.IsMouseDirectlyOver)
            {
                if (tok.Opacity < 1)
                {
                    tok.Opacity = 0.7;
                    this.Cursor = Cursors.Hand;
                }
            }
            else
            {
                if (tok.Opacity < 1)
                {
                    tok.Opacity = 0.4;
                }
                this.Cursor = null;
            }
        }

        void t_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //dit wordt geactiveerd als je met de linker muis knop op een mogelijke token drukt op het speelveld
            var token = sender as Ellipse;

            //game.Play(token);
            game.PlayCommand.Execute(token);

        }

        public bool? ShowDialog<T>(INotifyPropertyChanged viewmodel) where T : Window
        {
            var window = Activator.CreateInstance<T>();
            window.Owner = this;
            window.DataContext = viewmodel;

            return window.ShowDialog();
        }


    }
}
