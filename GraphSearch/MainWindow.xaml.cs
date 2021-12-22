using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System.Windows.Shapes;
using UniStorage;
using System.Windows.Controls.Primitives;

namespace GraphSearch
{
    public partial class MainWindow : Window
    {
        public List<Shape> shapes;
        public MainWindow()
        {
            InitializeComponent();
            shapes = new List<Shape>();  
        }


        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach(var s in shapes)
            {
                if (s.HitTest(e.GetPosition(canvas).X, e.GetPosition(canvas).Y)){
                  
                    if(!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                    {
                        foreach (var c in shapes)
                            c.Unselect();
                    }

                    foreach (ComboBoxItem i in ColorBox.Items)
                    {
                        if (s.GetColor().Equals( ((((i.Content as StackPanel).Children[0] as System.Windows.Shapes.Rectangle).Fill as SolidColorBrush).Color)))
                        ColorBox.SelectedItem = i;
                    }
                    SizeSlider.Value = s.GetSize();
                    s.Select();
                    e.Handled = true;
                    return;
                }
            }
            System.Windows.Point pt = e.GetPosition(canvas);
            foreach (var c in shapes)
                c.Unselect();

            Shape shape= null;
            if (CircleButton.IsChecked == true)
                shape = new Circle(canvas,e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
            if (SquareButton.IsChecked == true)
                shape = new Sqare(canvas,e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
            if(EllipseButton.IsChecked == true)
                shape = new Ellipse(canvas,e.GetPosition(canvas).X, e.GetPosition(canvas).Y);

            SizeSlider.Value = shape.GetSize();
            shape.SetColor(((((ColorBox.SelectedItem as ComboBoxItem).Content as StackPanel).Children[0] as System.Windows.Shapes.Rectangle).Fill
                as SolidColorBrush).Color);
            shapes.Add(shape); 
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Escape)
            {
                foreach (var c in shapes)
                {
                    if (c.Selected == true)
                    {
                        c.Unselect();
                    }
                }
            }
            if(e.Key == Key.Delete)
            {
                List<Shape> deleted = new List<Shape>();
                foreach(var c in shapes)
                {
                    if (c.Selected == true)
                    {
                        deleted.Add(c);
                    }
                }
                foreach(var c in deleted)
                {
                    c.Remove();
                    shapes.Remove(c);    
                }
            }
            #region MoveControls
            if (e.Key == Key.Left)
            {
                foreach (var c in shapes)
                {
                    if (c.Selected == true)
                    {
                    c.Move(-1, 0);
                    }
                }
            }
            if (e.Key == Key.Right)
            {
                foreach (var c in shapes)
                {
                    if (c.Selected == true)
                    {
                        c.Move(1, 0);
                    }
                }
            }
            if (e.Key == Key.Up)
            {
                foreach (var c in shapes)
                {
                    if (c.Selected == true)
                    {
                        c.Move(0, -1);
                    }
                }
            }
            if (e.Key == Key.Down)
            {
                foreach (var c in shapes)
                {
                    if (c.Selected == true)
                    {
                        c.Move(0, 1);
                    }
                }
            }
            #endregion
        }
        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(shapes != null)
            foreach (var c in shapes)
            {
                if (c.Selected == true)
                {
                    c.SetColor((((((ColorBox.SelectedItem as ComboBoxItem).Content as StackPanel).Children[0] as System.Windows.Shapes.Rectangle).Fill 
                            as SolidColorBrush).Color));
                }
            }
        }

        private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            foreach (var c in shapes)
            {
                if (c.Selected == true)
                {
                    c.SetSize((int)SizeSlider.Value);
                }
            }
        }

        private void ShapeButtonChecked(object sender, RoutedEventArgs e)
        {
            foreach(var el in UpToolBar.Items)
            {
                if (el is ToggleButton && (el != sender as ToggleButton))
                    (el as ToggleButton).IsChecked = false;
            }
        }
    }
}
