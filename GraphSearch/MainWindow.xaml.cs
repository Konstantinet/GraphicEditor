using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

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
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach(var circle in circles)
            {
                if (circle.HitTest(e.GetPosition(this).X, e.GetPosition(this).Y)){
                  
                    if(!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                    {
                        UncheckAll();
                    }
                    SelectCircle(circle);
                    e.Handled = true;
                    return;
                }
            }
            Point pt = e.GetPosition(this);
            UncheckAll();
            var Vertex = new CCircle(e.GetPosition(this).X, e.GetPosition(this).Y);
            Vertex.Paint(canvas);
            circles.Add(Vertex); 
        }

       
        void UncheckAll()
        {
            foreach(var c in circles)
            {
                UnselectCircle(c);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                List<CCircle> deleted = new List<CCircle>();
                foreach(var c in circles)
                {
                    if (c.Selected == true)
                    {
                        deleted.Add(c);
                    }
                }
                foreach(var c in deleted)
                {
                    c.Remove(canvas);
                    circles.Remove(c);
                    
                }
            }
        }
        void SelectCircle(CCircle circle)
        {
            circle.Select();
            circle.Paint(canvas);
        }
        void UnselectCircle(CCircle circle)
        {
            circle.Unselect();
            circle.Paint(canvas);
        }
    }
}
