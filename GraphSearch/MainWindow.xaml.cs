using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using UniStorage;
using System.Windows.Controls.Primitives;
using GraphSearch.Model;
using System.IO;
using Microsoft.Win32;
using System;

namespace GraphSearch
{
    public partial class MainWindow : Window
    {
        public SerializableStorage<IShape> shapes;
        public MainWindow()
        {
            InitializeComponent();
            shapes = new SerializableStorage<IShape>();  
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

                    if (s is Shape)
                    {
                        foreach (ComboBoxItem i in ColorBox.Items)
                        {
                            if ((s as Shape).GetColor().Equals(((((i.Content as StackPanel).Children[0] as System.Windows.Shapes.Rectangle).Fill as SolidColorBrush).Color)))
                                ColorBox.SelectedItem = i;
                        }
                        SizeSlider.Value = (s as Shape).GetSize();
                    }
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
            shapes.AddElement(shape); 
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
                List<IShape> deleted = new List<IShape>();
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
                    shapes.RemoveElement(c);    
                }
            }
            if(e.Key == Key.G) 
            {
                var group = new ShapeGroup();
                List<IShape> deleted = new List<IShape>();
                foreach (var c in shapes)
                {
                    if (c.Selected == true)
                    {
                            
                            group.Add(c);
                            deleted.Add(c);
                            
                    }
                }
                foreach (var d in deleted)
                {
                    shapes.RemoveElement(d);
                }
                shapes.AddElement(group);
            }
            if(e.Key == Key.U)
            {
                foreach (var c in shapes)
                {
                    if (c.Selected == true)
                    {
                        if (c is ShapeGroup)
                        {
                            foreach (var m in (c as ShapeGroup).shapes)
                            {
                                m.Unselect();
                                shapes.AddElement(m);
                            }
                            shapes.RemoveElement(c);
                            
                        }
                    }
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
                if (c.Selected == true && c is Shape)
                {
                    (c as Shape).SetColor((((((ColorBox.SelectedItem as ComboBoxItem).Content as StackPanel).Children[0] as System.Windows.Shapes.Rectangle).Fill 
                            as SolidColorBrush).Color));
                }
            }
        }
        private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            foreach (var c in shapes)
            {
                if (c.Selected == true && c is Shape)
                {
                    (c as Shape).SetSize((int)SizeSlider.Value);
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
        private void FileSave(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save the file?",
                    "Save file",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var sfd = new SaveFileDialog();
                if (sfd.ShowDialog() == true)
                    using (StreamWriter sw = new StreamWriter(new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write)))
                    {
                        sw.WriteLine(shapes.GetCount());
                        foreach (var shape in shapes)
                        {
                            shape.Save(sw);
                        }
                    }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fd = new OpenFileDialog();
            if (fd.ShowDialog() == true) {
                var factory = new ShapeFactory(canvas);
                shapes.LoadComponents(shapes,new StreamReader(fd.FileName), factory);
            }
        }
    }
}
