﻿using DevExpress.Mvvm.Native;
using SeldatMRMS.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SeldatMRMS
{
    public class MouseAction
    {
        public MouseController.MouseEvent MouseEvent { get; }
        public Point MousePosition { get; }
       // public MouseButtons Buttons { get; }
        public MouseEventArgs MouseEventArgs { get; }

        public MouseAction(MouseController.MouseEvent mouseEvent, MouseEventArgs args)
        {
            MouseEvent = mouseEvent;
            MouseEventArgs = args;
           // MousePosition = args.GetPosition
            // Buttons = buttons;
        }
    }
    public class MouseController
    {

        public enum RouteName
        {
            FireMouseClickEvent,
            CanvasFocus,
            StartDragSurface,
            EndDragSurface,
            EndDragSurfaceWithDeselect,
            DragSurface,
            StartDragSelectionBox,
            EndDragSelectionBox,
            DragSelectionBox,
            StartShapeDrag,
            EndShapeDrag,
            StartAnchorDrag,
            EndAnchorDrag,
            DragShapes,
            DragAnchor,
            HoverOverShape,
            ShowAnchors,
            ShowAnchorCursor,
            ClearAnchorCursor,
            HideAnchors,
            SelectSingleShapeMouseUp,
            SelectSingleShapeMouseDown,
            SelectSingleGroupedShape,
            AddSelectedShape,
            RemoveSelectedShape,
            EditShapeText,
        }
        public enum MouseEvent
        {
            MouseDown,
            MouseUp,
            MouseMove,
            MouseDoubleClick,
        }
        public class MouseRouter
        {
            public MouseController.RouteName RouteName { get; set; }
            public MouseController.MouseEvent MouseEvent { get; set; }
            public Func<bool> Condition { get; set; }
            public Action<MouseEventArgs> Action { get; set; }
            public Action Else { get; set; }
            public Action Debug { get; set; }
        }
        CanvasController canvascontroller;
        Canvas canvas;
        protected Point LastMousePosition { get; set; }
        protected Point CurrentMousePosition { get; set; }
        protected bool DraggingSurface { get; set; }
        protected bool DraggingShapes { get; set; }
        protected bool DraggingAnchor { get; set; }
        protected bool DraggingOccurred { get; set; }
        protected bool DraggingSurfaceOccurred { get; set; }
        protected GraphicElement HoverShape { get; set; }
        protected List<MouseRouter> router;
        protected Box bp;
        public MouseController(Canvas canvas)
        {
            this.canvas = canvas;
            Point p = Mouse.GetPosition(canvas);
            bp = new Box(canvas) { DisplayRectangle = new BorderShape(new Point(20, 20)) };

            bp.Name = "testinga";
            router = new List<MouseRouter>();
            canvascontroller = IServiceManager.canvasController;
            canvas.MouseDown += (sndr, args) => HandleEvent(new MouseAction(MouseEvent.MouseDown, args));
            canvas.MouseUp += (sndr, args) => HandleEvent(new MouseAction(MouseEvent.MouseUp, args));
            canvas.MouseMove += (sndr, args) => HandleEvent(new MouseAction(MouseEvent.MouseMove, args));
            canvas.MouseLeftButtonDown += (sndr, args) => HandleEvent(new MouseAction(MouseEvent.MouseDoubleClick, args));
            bp.Draw();
            InitializeBehavior();
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            
        }
        protected virtual void HandleEvent(MouseAction action)
        {
            //Trace.WriteLine("Route:HandleEvent:" + CurrentButtons.ToString());
            CurrentMousePosition = action.MouseEventArgs.GetPosition(canvas);
           // CurrentButtons = Control.MouseButtons;
            // Issue #39: Mouse Move event fires even for button press when mouse hasn't moved!
            IEnumerable<MouseRouter> routes = router.Where(r => (action.MouseEvent != MouseEvent.MouseMove && r.MouseEvent == action.MouseEvent)
                || ((action.MouseEvent == MouseEvent.MouseMove && r.MouseEvent == action.MouseEvent)));

            routes.ForEach(r =>
            {
                Trace.WriteLine("Route:Executing Route:" + r.RouteName.ToString() + "  "+CurrentMousePosition.ToString());
                r.Debug?.Invoke();

                // Test condition every time after executing a route handler, as the handler may change state for the next condition.
                if (r.Condition())
                {
                    Trace.WriteLine("Route:Executing Route:" + r.RouteName.ToString());
                    r.Action(action.MouseEventArgs);
                }
                else
                {
                    r.Else?.Invoke();
                }
            });

        }
        public virtual void InitializeBehavior()
        {
            router.Add(new MouseRouter()
            {
                RouteName = RouteName.FireMouseClickEvent,
                MouseEvent = MouseEvent.MouseDown,
                Condition = () => true,
                Action = (mouseEventArgs) =>
                {
                    // So Ctrl+V paste works, as keystroke is intercepted only when canvas panel has focus.
                  //  MouseClick.Fire(this, mouseEventArgs);
                }
            });
            router.Add(new MouseRouter()
            {
                RouteName = RouteName.DragShapes,
                MouseEvent = MouseEvent.MouseMove,
                Condition = () =>true,
                Action = (mouseEventArgs) =>
                {
                   // MessageBox.Show("ssss");
                  //  DragShapes();
                    DraggingOccurred = true;
                },
            });
            router.Add(new MouseRouter()
            {
                RouteName = RouteName.SelectSingleShapeMouseDown,
                MouseEvent = MouseEvent.MouseDown,
                Condition = () => canvascontroller.IsRootShapeSelectable(CurrentMousePosition),
                Action = (_) => SelectSingleRootShape()
            });

            router.Add(new MouseRouter()
            {
                RouteName = RouteName.ShowAnchors,
                MouseEvent = MouseEvent.MouseMove,
                Condition = () => canvascontroller.IsRootShapeSelectable(CurrentMousePosition),
                Action = (_) => ShowAnchors()
            });
            router.Add(new MouseRouter()
            {
                RouteName = RouteName.ShowAnchors,
                MouseEvent = MouseEvent.MouseMove,
                Condition = () => !canvascontroller.IsRootShapeSelectable(CurrentMousePosition) && HoverShape!=null,
                Action = (_) => HideAnchors()
            });

            /* router.Add(new MouseRouter()
             {
                 RouteName = RouteName.AddSelectedShape,
                 MouseEvent = MouseEvent.MouseUp,
                 Condition = () => serviceManager.Get<IFlowSharpCanvasService>().ActiveController.IsRootShapeSelectable(CurrentMousePosition) &&
                     serviceManager.Get<IFlowSharpCanvasService>().ActiveController.IsMultiSelect() && !DraggingSelectionBox &&
                     !serviceManager.Get<IFlowSharpCanvasService>().ActiveController.SelectedElements.Contains(serviceManager.Get<IFlowSharpCanvasService>().ActiveController.GetRootShapeAt(CurrentMousePosition)),
                 Action = (_) => AddShapeToSelectionList(),
             });*/

        }
        protected void DragShapes()
        {
          //  controller.Canvas.Cursor = Cursors.SizeAll;
            Point delta = CurrentMousePosition.Delta(LastMousePosition);

          /*  if (controller.SelectedElements.Count == 1 && controller.SelectedElements[0].IsConnector)
            {
                // Check both ends of any connector being moved.
                if (!controller.SnapController.SnapCheck(GripType.Start, delta, (snapDelta) => controller.DragSelectedElements(snapDelta)))
                {
                    if (!controller.SnapController.SnapCheck(GripType.End, delta, (snapDelta) => controller.DragSelectedElements(snapDelta)))
                    {
                        controller.DragSelectedElements(delta);
                        controller.SnapController.UpdateRunningDelta(delta);
                    }
                }
            }*/
            //else
            {
                
                canvascontroller.DragSelectedElements(CurrentMousePosition);
               // controller.SnapController.UpdateRunningDelta(delta);
            }
        }
        protected void SelectSingleRootShape()
        {
            // Preserve for undo:
            List<GraphicElement> selectedShapes = canvascontroller.SelectedElements.ToList();
            GraphicElement el = canvascontroller.GetRootShapeAt(CurrentMousePosition);
            if (!selectedShapes.Contains(el))
            {
                MessageBox.Show(selectedShapes.Count + "");
                canvascontroller.DeselectCurrentSelectedElements();
                canvascontroller.SelectElement(el);
            }
        }

        public void ShowAnchors()
        {
            try
            {
                GraphicElement el = canvascontroller.GetRootShapeAt(CurrentMousePosition);
                Trace.WriteLine("*** ShowAnchors " + el.GetType().Name);
                el.ShowAnchors = true;
                el.DrawAnchors();
                HoverShape = el;
            }
            catch { }
            //canvascontroller.SetAnchorCursor(el);
        }
        protected void HideAnchors()
        {
            
               // GraphicElement el = canvascontroller.GetRootShapeAt(CurrentMousePosition);
               // Trace.WriteLine("*** ShowAnchors " + el.GetType().Name);
                canvascontroller.HideAnchors();
               // HoverShape = el;
         
            //canvascontroller.SetAnchorCursor(el);
        }
    }
}
