
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SeldatMRMS
{
    public class GraphicElement
    {
        public string Name { get; set; }
        public string Text { get; set; }
        //public System.Drawing.Font TextFont { get; set; }
       // public Color TextColor { get; set; }
        //public ContentAlignment TextAlign { get; set; }
        public BorderShape DisplayRectangle { get; set; }
        // TODO: Text location - left, top, right, middle, bottom
        public virtual bool Selected { get; protected set; }
        protected bool HasCornerAnchors { get; set; }
        protected bool HasCenterAnchors { get; set; }
        protected bool HasLeftRightAnchors { get; set; }
        protected bool HasTopBottomAnchors { get; set; }

        protected bool HasCornerConnections { get; set; }
        protected bool HasCenterConnections { get; set; }
        protected bool HasLeftRightConnections { get; set; }
        protected bool HasTopBottomConnections { get; set; }

        public virtual void SetConnection(GripType gt, GraphicElement shape) { }
        public virtual void DisconnectShapeFromConnector(GripType gt) { }
        public virtual void DetachAll() { }
        public virtual void UpdateProperties() { }
        public virtual void RemoveConnection(GripType gt) { }
        public bool isSelected { get; set; }
        public GraphicElement Parent { get; set; }
        public List<ShapeAnchor> AnchorsList;
        public List<Connection> Connections = new List<Connection>();
        protected SolidColorBrush selectionPen;
        public bool ShowAnchors { get; set; }
        public bool ShowConnectionPoints { get; set; }
        public virtual bool IsConnector { get { return false; } }
        /* protected Bitmap background;
         protected System.Windows.Shapes.Rectangle backgroundRectangle;
         protected Pen selectionPen;
         protected Pen altSelectionPen;
         protected Pen tagPen;
         protected Pen altTagPen;
         protected Pen anchorPen = new Pen(Color.Black);
         protected Pen connectionPointPen = new Pen(Color.Blue);
         protected SolidBrush anchorBrush = new SolidBrush(Color.Black);*/
        protected int anchorWidthHeight = 6;        // TODO: Make const?
        public Canvas canvas;

        protected bool disposed;
        protected bool removed;
        public virtual void UpdatePath() { }
    //    public virtual void UpdateProperties() { }
        public GraphicElement(Canvas canvas)
        {
            this.canvas = canvas;
            HasCenterAnchors = true;
            HasCornerAnchors = true;
            HasLeftRightAnchors = false;
            HasTopBottomAnchors = false;
            HasCenterConnections = true;
            HasCornerConnections = true;
            HasLeftRightConnections = false;
            HasTopBottomConnections = false;
            selectionPen = new SolidColorBrush(Colors.BlueViolet);
            
        }
        public virtual List<ShapeAnchor> GetAnchors()
        {
            // draw anchors 
            List<ShapeAnchor> anchors = new List<ShapeAnchor>();
            AnchorBox r;
            int anchorSize = 6;
            if (HasCornerAnchors) // tai cac canh
            {
                Point newPoint = new Point();
                newPoint = DisplayRectangle.TopLeft();
                r =new AnchorBox(DisplayRectangle.TopLeft(), anchorSize);
                anchors.Add(new ShapeAnchor(ShapeAnchor.GripType.TopLeft, r, Cursors.SizeNWSE));

                newPoint = DisplayRectangle.TopRight();
                newPoint.Offset(-anchorWidthHeight, 0);
                r = new AnchorBox(newPoint, anchorSize);
                anchors.Add(new ShapeAnchor(ShapeAnchor.GripType.TopRight, r, Cursors.SizeNESW));

                newPoint = DisplayRectangle.BottomLeft();
                newPoint.Offset(0, -anchorWidthHeight);
                r = new AnchorBox(newPoint, anchorSize);
                anchors.Add(new ShapeAnchor(ShapeAnchor.GripType.BottomLeft, r, Cursors.SizeNESW));

                newPoint = DisplayRectangle.BottomRight();
                newPoint.Offset(-anchorWidthHeight, -anchorWidthHeight);
                r = new AnchorBox(newPoint, anchorSize);
                anchors.Add(new ShapeAnchor(ShapeAnchor.GripType.BottomRight, r, Cursors.SizeNWSE));
            }

            if (HasCenterAnchors || HasLeftRightAnchors) // tai trai va phai
            {
                Point newPoint = DisplayRectangle.LeftMiddle();
                newPoint.Offset(0, -anchorWidthHeight / 2);
                r = new AnchorBox(newPoint, anchorSize);
                anchors.Add(new ShapeAnchor(ShapeAnchor.GripType.LeftMiddle, r, Cursors.SizeWE));

                newPoint = DisplayRectangle.RightMiddle();
                newPoint.Offset(-anchorWidthHeight, -anchorWidthHeight / 2);
                r = new AnchorBox(newPoint, anchorSize);
                anchors.Add(new ShapeAnchor(ShapeAnchor.GripType.RightMiddle, r, Cursors.SizeWE));
            }

            if (HasCenterAnchors || HasTopBottomAnchors) // tai vi tri tren va duoi
            {
                Point newPoint = new Point();
                newPoint = DisplayRectangle.TopMiddle();
                newPoint.Offset(-anchorWidthHeight / 2, 0);
                r = new AnchorBox(newPoint, anchorSize);
                anchors.Add(new ShapeAnchor(ShapeAnchor.GripType.TopMiddle, r, Cursors.SizeNS));
                newPoint = DisplayRectangle.BottomMiddle();
                newPoint.Offset(-anchorWidthHeight / 2, -anchorWidthHeight);
                r = new AnchorBox(newPoint, anchorSize);
                anchors.Add(new ShapeAnchor(ShapeAnchor.GripType.BottomMiddle, r, Cursors.SizeNS));
            }

            return anchors;
        }
        public virtual void Select()
        {
            Selected = true;

        }
        public virtual List<ConnectionPoint> GetConnectionPoints()
        {
            List<ConnectionPoint> connectionPoints = new List<ConnectionPoint>();

            if (HasCornerConnections)
            {
                connectionPoints.Add(new ConnectionPoint(GripType.TopLeft, DisplayRectangle.TopLeft()));
                connectionPoints.Add(new ConnectionPoint(GripType.TopRight, DisplayRectangle.TopRight()));
                connectionPoints.Add(new ConnectionPoint(GripType.BottomLeft, DisplayRectangle.BottomLeft()));
                connectionPoints.Add(new ConnectionPoint(GripType.BottomRight, DisplayRectangle.BottomRight()));
            }

            if (HasCenterConnections)
            {
                connectionPoints.Add(new ConnectionPoint(GripType.LeftMiddle, DisplayRectangle.LeftMiddle()));
                connectionPoints.Add(new ConnectionPoint(GripType.RightMiddle, DisplayRectangle.RightMiddle()));
                connectionPoints.Add(new ConnectionPoint(GripType.TopMiddle, DisplayRectangle.TopMiddle()));
                connectionPoints.Add(new ConnectionPoint(GripType.BottomMiddle, DisplayRectangle.BottomMiddle()));
            }

            if (HasLeftRightConnections)
            {
                connectionPoints.Add(new ConnectionPoint(GripType.Start, DisplayRectangle.LeftMiddle()));
                connectionPoints.Add(new ConnectionPoint(GripType.End, DisplayRectangle.RightMiddle()));
            }

            if (HasTopBottomConnections)
            {
                connectionPoints.Add(new ConnectionPoint(GripType.Start, DisplayRectangle.TopMiddle()));
                connectionPoints.Add(new ConnectionPoint(GripType.End, DisplayRectangle.BottomMiddle()));
            }

            return connectionPoints;
        }
        public virtual void Deselect()
        {
            Selected = false;
        }
        public virtual bool IsSelectable(Point p)
        {
            return DisplayRectangle.Contain(p);
        }
        public virtual BorderShape DefaultRectangle()
        {
            return new BorderShape(20, 20, 60, 60);
        }
        public virtual void DrawAnchors()
        {
            
            AnchorsList.ForEach((a =>
            {
                a.anchorBox.Move();
                Draw(a.anchorBox.rectangle);
            }));
        }
        public virtual void RemoveAnchors()
        {
            AnchorsList.ForEach((a =>
            {
                Remove(a.anchorBox.rectangle);
            }));
        }
        protected virtual void Remove(UIElement element)
        {
          
            this.canvas.Children.Remove(element);
        }
        protected virtual void Draw(UIElement element)
        {
            this.canvas.Children.Add(element);
        }
        public virtual void Draw()
        {
             DrawSelection();
            
        }


        public virtual void Move(Point delta)
        {
            DisplayRectangle.Move(delta);
        }
        public virtual void DrawSelection()
        {
            if (!Selected)
            {
                DisplayRectangle.BorderBrush = new SolidColorBrush(Colors.Black);
            }
            else
            {
                DisplayRectangle.BorderBrush = selectionPen;
            }
        }
        public virtual System.Windows.Point GetLocation()
        {
            return new System.Windows.Point(Canvas.GetLeft(DisplayRectangle),Canvas.GetTop(DisplayRectangle));
        }

    }
}
