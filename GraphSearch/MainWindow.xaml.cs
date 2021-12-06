using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphSearch
{
    public partial class MainWindow : Window
    {
        public List<CCircle> circles;
        public MainWindow()
        {
            InitializeComponent();
            circles = new List<CCircle>();
            
        }

        //protected override void OnRender(DrawingContext drawingContext)
        //{
        //    base.OnRender(drawingContext);
        //    foreach (var c in circles)
        //    {
        //        //drawingContext.DrawEllipse(Brushes.White, new Pen(Brushes.Black, 3), new Point(c.X, c.Y), 50, 50);
        //        c.Paint(canvas);

        //    }
        //}
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
            
            foreach(var circle in circles)
            {
                if (circle.HitTest(e.GetPosition(this).X, e.GetPosition(this).Y)){
                    if(!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                    {
                        UncheckAll();
                    }
                    circle.Select();
                    return;
                }
            }

            
            Point pt = e.GetPosition(this);
            UncheckAll();
            var Vertex = new CCircle(e.GetPosition(this).X, e.GetPosition(this).Y);
            circles.Add(Vertex);
            Vertex.Paint(canvas);
            
        }

       
        void UncheckAll()
        {
            foreach(var c in circles)
            {
                c.Unselect();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                foreach(var c in circles)
                {
                    if (c.Selected == true)
                    {
                        circles.Remove(c);
                    }
                }
            }
        }

    }
}
