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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowLogic : Window
    {
        public MainWindowLogic()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void SpawnTile(object sender, RoutedEventArgs e)
        {
            int tileType = int.Parse(((Button)sender).Tag.ToString());
            DrawingImage imageToSpawn = GetImageToSpawn(tileType);
        }

        // Don't mind this for now
        private DrawingImage GetImageToSpawn(int tileType)
        {
            DrawingImage image = null;

            switch (tileType)
            {
                case 1:
                //D:\Projects\Polyiamonths\Program\WpfApplication1\WpfApplication1\images\tile_1.svg
                    break;

                case 2:
                    break;

                case 3:
                    break;

                case 4:
                    break;

                case 5:
                    break;

                case 6:
                    break;
            }

            return image;
        }
    }
}
