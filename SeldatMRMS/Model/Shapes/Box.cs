using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SeldatMRMS
{
    public class Box:GraphicElement
    {

        public Box(Canvas canvas) : base(canvas)
        {
        }
        public override void Draw()
        {
            canvas.Children.Add(DisplayRectangle);
        }

    }
}
