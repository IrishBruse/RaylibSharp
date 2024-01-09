namespace RaylibSharp;

using System.Drawing;

#pragma warning disable IDE0290

public partial struct NPatchInfo
{
    /// <summary> NPatchInfo Constructor </summary>
    public NPatchInfo(RectangleF source, int left, int top, int right, int bottom, NPatchLayout layout)
    {
        Source = source;
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
        Layout = (int)layout;
    }

    /// <summary> NPatchInfo Constructor </summary>
    public NPatchInfo(float x, float y, float width, float height, int left, int top, int right, int bottom, NPatchLayout layout)
    {
        Source = new(x, y, width, height);
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
        Layout = (int)layout;
    }
}

#pragma warning restore IDE0290
