using GhostGolfClient;
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

        private Brush costomCollor;
        private ConnectionToData connection;
        private Level level;
        private Ball player;
        private Hole hole;
        private Ball[] otherPlayers;
        private Ellipse canvasBall;
        private Ellipse canvasHole;
        private Ellipse[] canvasOtherPlayers;

        public MainWindow()
        {
            InitializeComponent();
            this.connection = new ConnectionToData();
            this.level = connection.GetLevel();
            this.player = this.level.ball;
            this.hole = this.level.hole;
            fillCanvas();
        }

        private void fillCanvas ()
        {
            //create area
            int[] bounds = level.getBounds();
            Rectangle levelArea = new Rectangle
            {
                Width = bounds[2] - bounds[0],
                Height = bounds[3] - bounds[1],
                Fill = new SolidColorBrush(Color.FromRgb((byte)100, (byte)150, (byte)50)),
                StrokeThickness = 3,
                Stroke = Brushes.Black
            };
            Canvas.SetLeft(levelArea, bounds[0]);
            Canvas.SetTop(levelArea, bounds[1]);
            levelCanvas.Children.Add(levelArea);

            //create ball
            canvasBall = new Ellipse
            {
                Width = player.getRadius(),
                Height = player.getRadius(),
                Fill = new SolidColorBrush(Color.FromRgb((byte)200, (byte)200, (byte)200)),
                StrokeThickness = 3,
                Stroke = Brushes.Black
            };
            Canvas.SetLeft(canvasBall, player.getPos()[0] - (player.getRadius() / 2));
            Canvas.SetTop(canvasBall, player.getPos()[1] - (player.getRadius() / 2));
            levelCanvas.Children.Add(canvasBall);

            //create hole
            canvasHole = new Ellipse
            {
                Width = hole.getRadius(),
                Height = hole.getRadius(),
                Fill = new SolidColorBrush(Color.FromRgb((byte)10, (byte)10, (byte)10)),
                StrokeThickness = 3,
                Stroke = Brushes.Black
            };
            Canvas.SetLeft(canvasHole, hole.getPos()[0] - (hole.getRadius() / 2));
            Canvas.SetTop(canvasHole, hole.getPos()[1] - (hole.getRadius() / 2));
            levelCanvas.Children.Add(canvasHole);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //do the shoot
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
