﻿using System;
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
    public static class ExtensionMethod
    {
        public static Point Delta(this Point p, Point p2)
        {
            return new Point(p.X - p2.X, p.Y - p2.Y);
        }
        public static bool IsInPolygon(Point[] poly, Point p)
        {
            Point p1, p2;
            bool inside = false;

            if (poly.Length < 3)
            {
                return inside;
            }

            var oldPoint = new Point(
                poly[poly.Length - 1].X, poly[poly.Length - 1].Y);

            for (int i = 0; i < poly.Length; i++)
            {
                var newPoint = new Point(poly[i].X, poly[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                    && (p.Y - (long)p1.Y) * (p2.X - p1.X)
                    < (p2.Y - (long)p1.Y) * (p.X - p1.X))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
        }
        public static int Sign(this int n)
        {
            return Math.Sign(n);
        }
        public static int Abs(this int n)
        {
            return Math.Abs(n);
        }
        public static bool IsNear(this Point p1, Point p2, int range)
        {
            return Math.Abs(p1.X - p2.X) <= range && Math.Abs(p1.Y - p2.Y) <= range;
        }
    }
}
