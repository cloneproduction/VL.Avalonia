using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VL.Avalonia.Custom.Controls.Collections
{
    public partial class ListBoxReorderable : ListBox
    {
        protected override Type StyleKeyOverride { get { return typeof(ListBox); } }
    }
}
