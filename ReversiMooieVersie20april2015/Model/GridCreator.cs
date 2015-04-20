
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ReversiMooieVersie20april2015.Model
{
    class GridCreator
    {

        public Brush LineBrush { get; set; }
        public int LineThickness { get; set; }
        public double EllipsePaddingPercentage { get; set; }

        //deze functie werkt, afblijven vanaf nu dus
        public GridCreator()
        {
            LineBrush = Brushes.Black;
            LineThickness = 1;
            EllipsePaddingPercentage = 0.075;
        }

        //deze functie werkt, afblijven vanaf nu dus
        public void SetupGrid(Grid grid, int cols, int rows)
        {
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            grid.Children.Clear();

            for (int i = 0; i < cols; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                if (i > 0)
                {
                    var l = CreateLine(cols, rows);
                    l.Y1 = i * grid.Width / 8;
                    l.X1 = 0;
                    l.Y2 = i * grid.Width / 8;
                    l.X2 = grid.Height;
                    grid.Children.Add(l);
                }
            }

            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());

                if (i > 0)
                {
                    var l = CreateLine(cols, rows);
                    l.X1 = i * grid.Height / 8;
                    l.Y1 = 0;
                    l.X2 = i * grid.Height / 8;
                    l.Y2 = grid.Width;
                    grid.Children.Add(l);
                }
            }
        }

        private Line CreateLine(int colspan, int rowspan)
        {
            var l = new Line()
            {
                Stroke = LineBrush,
                StrokeThickness = LineThickness
            };
            Grid.SetRowSpan(l, rowspan);
            Grid.SetColumnSpan(l, colspan);

            return l;
        }


        public void FillWithEllipseWithEvent(Grid g, Action<object, MouseButtonEventArgs> mouseLeftButtonUp, Action<object, DependencyPropertyChangedEventArgs> isMouseDirectlyOverChanged)
        {
            int cols = g.ColumnDefinitions.Count();
            int rows = g.RowDefinitions.Count();

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Ellipse el = new Ellipse();
                    el.MouseLeftButtonUp += new MouseButtonEventHandler(mouseLeftButtonUp);
                    el.IsMouseDirectlyOverChanged += new DependencyPropertyChangedEventHandler(isMouseDirectlyOverChanged);
                    Grid.SetColumn(el, i);
                    Grid.SetRow(el, j);

                    el.Margin = new Thickness(((double)g.Width / cols) * EllipsePaddingPercentage);
                    g.Children.Add(el);
                }
            }
        }


    }
}
