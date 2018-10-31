using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeldatMRMS
{
    public static class Constants
    {
        public static String ANCHOR_NONE = "M0,0 L0,-0 L0,0 z";
        public static String ANCHOR_ARROW = "M0,0 L2,-2 L2,2 z";
        public static String ANCHOR_SQUARE= "M0,0 L2,-2 L2,2 z";

        public const int MIN_WIDTH = 20;
        public const int MIN_HEIGHT = 20;
    }
}
