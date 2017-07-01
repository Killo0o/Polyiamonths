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

        public MainWindowLogic()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void SelectShape(object sender, RoutedEventArgs e)
        {
            int tileType = int.Parse(((Button)sender).Tag.ToString());
            _selectedShape = GetImageToSpawn(tileType);
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
