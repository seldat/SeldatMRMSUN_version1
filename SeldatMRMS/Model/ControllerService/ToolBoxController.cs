
using SeldatMRMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SeldatMRMS
{
    public class ToolBoxController
    {
        public CanvasController canvasController ;
        public Canvas canvas;
        public ToolBoxController(Canvas canvas)
        {
            this.canvas = canvas;
            canvas.MouseDown += canvas_MouseDown;
        }
        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(canvas);
            Box box = new Box(canvas) {DisplayRectangle=new BorderShape(p) };
            CreateControl(box);
        }
        private void CreateControl(GraphicElement el)
        {
            IServiceManager.canvasController.AddElement(el);
            el.Draw();
        }

    }
}
