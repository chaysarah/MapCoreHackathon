using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MCTester.General_Forms
{
    public delegate void MouseMoveEventArgs(object sender, Point mouseLocation, MouseButtons mouseClickedButton, int mouseWheelDelta);
    public delegate void MouseWheelButtonEventArgs(object sender, Point mouseLocation, int mouseWheelDelta);
    public delegate void MouseClickEventArgs(object sender, Point mouseLocation);
    public delegate void MouseUpEventArgs(object sender, Point mouseLocation, int mouseWheelDelta);
    public delegate void MouseDownEventArgs(object sender, Point mouseLocation, MouseButtons mouseClickedButton, int mouseWheelDelta);
    public delegate void MouseDoubleClickEventArgs(object sender, Point mouseLocation, int mouseWheelDelta);
    public delegate void PreKeyDownEventArgs(object sender, string keyString);
    public delegate void KeyDownEventArgs(object sender, string keyString, bool IsShiftPressed, bool IsControlPressed);
    public delegate void KeyUpEventArgs(object sender, string keyString);
    public delegate void MapControlResizeEventArgs(object sender, EventArgs e);

    public interface IMapObject
    {
        event MouseMoveEventArgs MouseMoveEvent;
        event MouseWheelButtonEventArgs MouseWheelEvent;
        event MouseClickEventArgs MouseClickEvent;
        event MouseUpEventArgs MouseUpEvent;
        event MouseDownEventArgs MouseDownEvent;
        event MouseDoubleClickEventArgs MouseDoubleClickEvent;
        event PreKeyDownEventArgs PreviewKeyDownEvent;
        event KeyDownEventArgs KeyDownEvent;
        event KeyUpEventArgs KeyUpEvent;
        //event MapControlResizeEventArgs MapControlResizeEvent;
    }


}
