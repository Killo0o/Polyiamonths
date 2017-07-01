﻿using Svg2Xaml;
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
        private object _movingObject;
        private double _firstXPos, _firstYPos;

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
            if (_selectedShapeNameToSpawn == null || _movingObject != null)
            {
                return;
            }

            Image bodyImage = new Image
            {
                Width = 140,
                Height = 140,
                Source = new BitmapImage(new Uri("WpfApplication1;component/images/"+ _selectedShapeNameToSpawn, UriKind.Relative))
            };

            bodyImage.AllowDrop = true;
            bodyImage.PreviewMouseLeftButtonDown += this.CanvasObjectLeftClick;
            bodyImage.PreviewMouseMove += this.CanvasObjectMouseMove;
            bodyImage.PreviewMouseLeftButtonUp += this.PreviewCanvasObjectLeftButtonUp;
            
            Point mousePosition = Mouse.GetPosition(drawingBoard);
          
            Canvas.SetLeft(bodyImage, mousePosition.X - bodyImage.Width/2);
            Canvas.SetTop(bodyImage, mousePosition.Y - bodyImage.Height/2);

            drawingBoard.Children.Add(bodyImage);
        }

        private void CanvasObjectLeftClick(object sender, MouseButtonEventArgs e)
        {
            // In this event, we get the current mouse position on the control to use it in the MouseMove event.
            Image img = sender as Image;
            Canvas canvas = img.Parent as Canvas;

            _firstXPos = e.GetPosition(img).X;
            _firstYPos = e.GetPosition(img).Y;

            _movingObject = sender;

            // Put the image currently being dragged on top of the others
            int top = Canvas.GetZIndex(img);
            foreach (Image child in canvas.Children)
                if (top < Canvas.GetZIndex(child))
                    top = Canvas.GetZIndex(child);
            Canvas.SetZIndex(img, top + 1);
        }

        private void PreviewCanvasObjectLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            Canvas canvas = img.Parent as Canvas;

            _movingObject = null;

            // Put the image currently being dragged on top of the others
            int top = Canvas.GetZIndex(img);
            foreach (Image child in canvas.Children)
                if (top > Canvas.GetZIndex(child))
                    top = Canvas.GetZIndex(child);
            Canvas.SetZIndex(img, top + 1);
        }

        private void CanvasObjectMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender == _movingObject)
            {
                Image img = sender as Image;
                Canvas canvas = img.Parent as Canvas;

                double newLeft = e.GetPosition(canvas).X - _firstXPos - canvas.Margin.Left;
                // newLeft inside canvas right-border?
                if (newLeft > canvas.Margin.Left + canvas.ActualWidth - img.ActualWidth)
                    newLeft = canvas.Margin.Left + canvas.ActualWidth - img.ActualWidth;
                // newLeft inside canvas left-border?
                else if (newLeft < canvas.Margin.Left)
                    newLeft = canvas.Margin.Left;
                img.SetValue(Canvas.LeftProperty, newLeft);

                double newTop = e.GetPosition(canvas).Y - _firstYPos - canvas.Margin.Top;
                // newTop inside canvas bottom-border?
                if (newTop > canvas.Margin.Top + canvas.ActualHeight - img.ActualHeight)
                    newTop = canvas.Margin.Top + canvas.ActualHeight - img.ActualHeight;
                // newTop inside canvas top-border?
                else if (newTop < canvas.Margin.Top)
                    newTop = canvas.Margin.Top;
                img.SetValue(Canvas.TopProperty, newTop);
            }
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
