using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace VL.Avalonia.Custom.Controls.Collections
{
    public partial class ListBoxReorderable :  ListBox
    {
        protected override Type StyleKeyOverride { get { return typeof(ListBox); } }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            DragDrop.SetAllowDrop(this, true);
            AddHandler(PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel);
            AddHandler(PointerReleasedEvent, OnPointerReleased, RoutingStrategies.Tunnel);
            AddHandler(DragDrop.DragOverEvent, DragOver);
        }

        private object? _draggedItem;
        private object? _originalSelectedItem;

        private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            if (_draggedItem == null)
                return;

            var point = e.GetPosition(this);
            var targetItem = GetItemUnderPointer(point);

            if (targetItem == null || targetItem == _draggedItem)
                return;


            var pouet = IsAbove(e);

            var collection = this.ItemsSource?.Cast<object>().ToList();
            if (collection != null)
            {
                int oldIndex = collection.IndexOf(_draggedItem);
                int newIndex = collection.IndexOf(targetItem);

                if (oldIndex >= 0 && newIndex >= 0 && oldIndex != newIndex)
                {
                    collection.RemoveAt(oldIndex);

                    // If item was before the drop position, the index shifts by one
                    if (oldIndex < newIndex) newIndex--;

                    collection.Insert(newIndex, _draggedItem);
                    this.SelectedItem = _originalSelectedItem;

                    this.ItemsSource = collection;
                }
            }

            _draggedItem = null;
        }


        private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            var point = e.GetPosition(this);
            var item = GetItemUnderPointer(point);

            if (item != null)
            {
                _originalSelectedItem = item;
                var sourceElement = e.Source as Control;

                _draggedItem = sourceElement?.DataContext;
            }
        }

        private void DragOver(object? sender, DragEventArgs e)
        {
            if (e.Data.Contains(DataFormats.Text))
            {
                e.DragEffects = DragDropEffects.Move;
                e.Handled = true;
            }
        }

        private bool IsAbove(PointerReleasedEventArgs e)
        {
            var point = e.GetPosition(this);
            var item = GetItemUnderPointer(point) as Visual;

            var pointOnItem = e.GetPosition(item);
            var height = item.Bounds.Height;

            if(pointOnItem.Y < height)
                return true;

            return false;
        }

        private object? GetItemUnderPointer(Point point)
        {
            var visual = this.InputHitTest(point) as Visual;

            while (visual != null && visual is not ListBoxItem)
                visual = visual.GetVisualParent();

            return (visual as ListBoxItem)?.DataContext;
        }
    }
}
