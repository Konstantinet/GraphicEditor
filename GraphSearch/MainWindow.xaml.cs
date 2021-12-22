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
                        UncheckAll();
                    }

                    foreach (ComboBoxItem i in ColorBox.Items)
                    {
                        if (s.Color.Equals( ((((i.Content as StackPanel).Children[0] as System.Windows.Shapes.Rectangle).Fill as SolidColorBrush).Color)))
                        ColorBox.SelectedItem = i;
                    }
                    SizeSlider.Value = s.GetSize();
                    SelectShape(s);
                    e.Handled = true;
                    return;
                }
            }
            System.Windows.Point pt = e.GetPosition(canvas);
            UncheckAll();

            Shape shape= null;
            if (CircleButton.IsChecked == true)
                shape = new Circle(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
            if (SquareButton.IsChecked == true)
                shape = new Sqare(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);
            if(EllipseButton.IsChecked == true)
                shape = new Ellipse(e.GetPosition(canvas).X, e.GetPosition(canvas).Y);

            SizeSlider.Value = shape.GetSize();
            shape.Color = ((((ColorBox.SelectedItem as ComboBoxItem).Content as StackPanel).Children[0] as System.Windows.Shapes.Rectangle).Fill
                as SolidColorBrush).Color;
            shape.Paint(canvas);
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
                        UnselectShape(c);
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
                    c.Remove(canvas);
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
                    c.Move(canvas,-1, 0);
                    //c.Paint(canvas);
                    }
                }
            }
            if (e.Key == Key.Right)
            {
                foreach (var c in shapes)
                {
                    if (c.Selected == true)
                    {
                        c.Move(canvas,1, 0);
                        //c.Paint(canvas);
                    }
                }
            }
            if (e.Key == Key.Up)
            {
                foreach (var c in shapes)
                {
                    if (c.Selected == true)
                    {
                        c.Move(canvas,0, -1);
                        //c.Paint(canvas);
                    }
                }
            }
            if (e.Key == Key.Down)
            {
                foreach (var c in shapes)
                {
                    if (c.Selected == true)
                    {
                        c.Move(canvas,0, 1);
                        //c.Paint(canvas);
                    }
                }
            }
            #endregion
        }
        void UncheckAll()
        {
            foreach (var c in shapes)
            {
                UnselectShape(c);
            }
        }
        void SelectShape(Shape shape)
        {
            shape.Select();
            shape.Paint(canvas);
        }
        void UnselectShape(Shape shape)
        {
            shape.Unselect();
            shape.Paint(canvas);
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(shapes != null)
            foreach (var c in shapes)
            {
                if (c.Selected == true)
                {
                    c.Color = (((((ColorBox.SelectedItem as ComboBoxItem).Content as StackPanel).Children[0] as System.Windows.Shapes.Rectangle).Fill 
                            as SolidColorBrush).Color);
                    c.Paint(canvas);
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
                    c.Paint(canvas);
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
