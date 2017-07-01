using Svg2Xaml;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowLogic : Window
    {

        private DrawingImage _selectedShape;
        private string _selectedShapeNameToSpawn;

        public MainWindowLogic()
        {
            InitializeComponent();
            ImageBrush ib = new ImageBrush();
            string dir = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            ib.ImageSource = new BitmapImage(new Uri(dir + @"\images\triangular_grid_large.png"));
            drawingBoard.Background = ib;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void SelectShape(object sender, RoutedEventArgs e)
        {
            int tileType = int.Parse(((Button)sender).Tag.ToString());
            _selectedShape = GetImageToSpawn(tileType);
            _selectedShapeNameToSpawn = GetImageToSpawn2(tileType);
        }

        private void MouseClickOnCanvas(object sender, MouseButtonEventArgs e)
        {

            if (_selectedShapeNameToSpawn == null)
            {
                return;
            }

            Image bodyImage = new Image
            {
                Width = 140,
                Height = 140,
                Name = "svirki",
                Source = new BitmapImage(new Uri("WpfApplication1;component/images/"+ _selectedShapeNameToSpawn, UriKind.Relative))
            };

            Point mousePosition = Mouse.GetPosition(drawingBoard);
          
            Canvas.SetLeft(bodyImage, mousePosition.X - bodyImage.Width/2);
            Canvas.SetTop(bodyImage, mousePosition.Y - bodyImage.Height/2);
            drawingBoard.Children.Add(bodyImage);
        }

        private void CanvasReset(object sender, RoutedEventArgs e)
        {
            drawingBoard.Children.RemoveRange(0, drawingBoard.Children.Count);
        }

        private void CanvasUndo(object sender, RoutedEventArgs e)
        {
            if (drawingBoard.Children.Count > 0)
            {
                drawingBoard.Children.RemoveAt(drawingBoard.Children.Count - 1);
            }
        }

        // Don't mind this for now
        private DrawingImage GetImageToSpawn(int tileType)
        {
            DrawingImage image = null;
            string dir = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

            switch (tileType)
            {
                case 1:
                    image = GetSVGFromResources(dir + @"\images\tile_1.svg");
                    break;

                case 2:
                    image = GetSVGFromResources(dir + @"\images\tile_2.svg");
                    break;

                case 3:
                    image = GetSVGFromResources(dir + @"\images\tile_3.svg");
                    break;

                case 4:
                    image = GetSVGFromResources(dir + @"\images\tile_4.svg");
                    break;

                case 5:
                    image = GetSVGFromResources(dir + @"\images\tile_5.svg");
                    break;

                case 6:
                    image = GetSVGFromResources(dir + @"\images\tile_6.svg");
                    break;
            }

            return image;
        }

        private String GetImageToSpawn2(int tileType)
        {
            string result = null;
            switch (tileType)
            {
                case 1:
                    result = "tile_1.png";
                    break;           
                                     
                case 2:              
                    result = "tile_2.png";
                    break;           
                                     
                case 3:              
                    result = "tile_3.png";
                    break;           
                                              
                case 4:              
                    result = "tile_4.png";
                    break;           
                                     
                case 5:              
                    result = "tile_5.png";
                    break;           
                                     
                case 6:              
                    result = "tile_6.png";
                    break;
            }

            return result;
        }

        private DrawingImage GetSVGFromResources(string fileName)
        {
            DrawingImage image = null;
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                image = SvgReader.Load(stream);

                // SvgReaderOptions options = new SvgReaderOptions(...);
                // DrawingImage image = SvgReader.Load(stream, options);
            }

            return image;
        }
    }
}
