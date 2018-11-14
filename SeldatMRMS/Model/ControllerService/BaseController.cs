using DevExpress.Mvvm.Native;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
namespace SeldatMRMS
{
    public class BaseController
    {
        //public virtual void SelectElement(GraphicElement el) { }
        public virtual void SelectOnlyElement(GraphicElement el) { }
        public virtual void SetAnchorCursor(GraphicElement el) { }
        public virtual void DragSelectedElements(Point delta) { }
        // public virtual void DeselectCurrentSelectedElements() { }
        //  public virtual void DeselectGroupedElements() { }
        public ReadOnlyCollection<GraphicElement> Elements { get { return elements.AsReadOnly(); } }
      //  public bool IsSnapToBeIgnored { get { return ((Control.ModifierKeys & Keys.Control) == Keys.Control) || UndoRedoIgnoreSnapCheck; } }

        public virtual void DeselectElement(GraphicElement el) { }
        protected List<GraphicElement> elements;
        protected List<GraphicElement> selectedElements;
        public ReadOnlyCollection<GraphicElement> SelectedElements { get { return selectedElements.AsReadOnly(); } }
        protected Canvas canvas;
        public BaseController(Canvas canvas)
        {
            this.canvas = canvas;
            elements = new List<GraphicElement>();
            selectedElements = new List<GraphicElement>();
        }
        public bool IsRootShapeSelectable(Point p)
        {
     
            return elements.Any(e => e.IsSelectable(p) && e.Parent == null);
        }

        public bool IsChildShapeSelectable(Point p)
        {
            return elements.Any(e => e.IsSelectable(p) && e.Parent != null);
        }

        public GraphicElement GetRootShapeAt(Point p)
        {
            return elements.FirstOrDefault(e => e.IsSelectable(p) && e.Parent == null);
        }

        public GraphicElement GetChildShapeAt(Point p)
        {
            return elements.FirstOrDefault(e => e.IsSelectable(p) && e.Parent != null);
        }
        public virtual bool IsMultiSelect()
        {
            return !((System.Windows.Forms.Control.ModifierKeys & (Keys.Control | Keys.Shift)) == 0);
        }
        public void AddElement(GraphicElement el)
        {
            elements.Add(el);
        }
        public void AddElements(List<GraphicElement> els)
        {
            elements.AddRange(els);
        }
        protected bool SelectElement(Point p)
        {
            GraphicElement el = elements.FirstOrDefault(e => e.IsSelectable(p));

            if (el != null)
            {
                SelectElement(el);
            }

            return el != null;
        }
        public void DeselectCurrentSelectedElements()
        {
            List<GraphicElement> elementsToRemove = new List<GraphicElement>();
            selectedElements.Where(el => el.Parent != null).ForEach(el =>
            {
                el.Deselect();
                elementsToRemove.Add(el);
            });
            elementsToRemove.ForEach(el => selectedElements.Remove(el));
        }
        public void SelectElement(GraphicElement el)
        {
              if (!selectedElements.Contains(el))
            {
                selectedElements.Add(el);
                el.Select();
                el.DrawSelection();
            }
        }
        

    }
}
