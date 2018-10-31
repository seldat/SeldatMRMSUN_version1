
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
        public ToolBoxController(Canvas canvas)
        {
            canvas.MouseDown += canvas_MouseMove;
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
        }
       /* protected GraphicElement GetSelectedElement(Point p)
        {
            GraphicElement el = elements.FirstOrDefault(e => e.DisplayRectangle.Contains(p));
            return el;
        }*/

    }
}
