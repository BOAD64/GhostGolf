using System;
using System.Collections.Generic;
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

namespace ClientWPF
{   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Brush costomCollor;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("HI");
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Rectangle)
            {
                Rectangle r = (Rectangle)e.OriginalSource;
                canvasName.Children.Remove(r);
            }
            else
            {
                costomCollor = new SolidColorBrush(Color.FromRgb((byte)100, (byte)150, (byte)50));

                Rectangle rectangle = new Rectangle
                {
                    Width = 20,
                    Height = 20,
                    Fill = costomCollor,
                    StrokeThickness = 3,
                    Stroke = Brushes.Black
                };

                Canvas.SetLeft(rectangle, Mouse.GetPosition(canvasName).X);
                Canvas.SetTop(rectangle, Mouse.GetPosition(canvasName).Y);

                canvasName.Children.Add(rectangle);
            }
        }
    }
}
