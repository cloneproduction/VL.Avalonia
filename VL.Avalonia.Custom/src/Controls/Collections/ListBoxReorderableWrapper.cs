using Avalonia.Controls;
using System.Collections;
using VL.Avalonia.Attributes;
using VL.Avalonia.Controls;
using VL.Avalonia.Helpers;
using VL.Core;
using VL.Core.Import;
using VL.Lib.Collections;
using VL.Lib.Reactive;

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


        //protected ChannelTwoWayBinding<Spread<T>> _itemsList;
        /// <param name="itemsList">
        /// Gets or sets the current value
        /// </param>
        [Fragment(Order = PinOrder.Main)]
        public void SetItemsList(IChannel<Spread<T>> itemsList) =>
            _itemsList.SetChannel(itemsList);


        protected ChannelTwoWayBinding<Spread<T>, IEnumerable> _itemsList;
        public ListBoxReorderableWrapper()
        {
            //_itemsList = new ChannelTwoWayBinding<Spread<T>>(_output, ListBox.ItemsSourceProperty);
            _itemsList = new ChannelTwoWayBinding<Spread<T>, IEnumerable>(_output, ListBox.ItemsSourceProperty, (x) => x as IEnumerable, (x) => CastToGeneric<T>(x).ToSpread<T>());
        }

        public static IEnumerable<T> CastToGeneric<T>(IEnumerable source)
        {
            return source.Cast<T>();
        }
    }
}
