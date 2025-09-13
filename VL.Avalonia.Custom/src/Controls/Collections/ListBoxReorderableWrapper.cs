using Avalonia.Controls;
using VL.Avalonia.Attributes;
using VL.Avalonia.Controls;
using VL.Core;
using VL.Core.Import;

namespace VL.Avalonia.Custom.Controls.Collections
{
    /// <summary>
    /// Listbox with reorderable items
    /// </summary>
    [ProcessNode(Name = "ListBoxReorderable")]
    public partial class ListBoxReorderableWrapper<T> : SelectingItemsControlWrapperBase<ListBoxReorderable, T>
    {
        [ImplementProperty("ListBoxReorderable.SelectionModeProperty", PinVisibility = Model.PinVisibility.Optional)]
        protected Optional<SelectionMode> _selectionMode;
    }
}
