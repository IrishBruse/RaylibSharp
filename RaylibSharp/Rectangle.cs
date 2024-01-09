namespace RaylibSharp;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Numerics;

#pragma warning disable CA1051

/// <summary> Rectangle structure </summary>
public struct Rectangle : IEquatable<Rectangle>
{
    /// <summary> X position of the rectangle </summary>
    public float X;
    /// <summary> Y position of the rectangle </summary>
    public float Y;
    /// <summary> Width of the rectangle </summary>
    public float Width;
    /// <summary> Height of the rectangle </summary>
    public float Height;

    /// <summary> Empty Rectangle </summary>
    public static readonly Rectangle Empty;

    /// <summary> Is Empty </summary>
    [Browsable(false)]
    public readonly bool IsEmpty => Height == 0 && Width == 0 && X == 0 && Y == 0;

    /// <summary> Bottom of Rectangle </summary>
    [Browsable(false)]
    public readonly float Bottom => Y + Height;

    /// <summary> Top of Rectangle </summary>
    [Browsable(false)]
    public readonly float Top => Y;

    /// <summary> Left of Rectangle </summary>
    [Browsable(false)]
    public readonly float Left => X;

    /// <summary> Right of Rectangle </summary>
    [Browsable(false)]
    public readonly float Right => X + Width;

    /// <summary> Rectangle Constructor with Vector4 </summary>
    public Rectangle(Vector4 vector)
    {
        X = vector.X;
        Y = vector.Y;
        Width = vector.Z;
        Height = vector.W;
    }

    /// <summary> Rectangle Constructor </summary>
    public Rectangle(float x, float y, float width, float height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary> Contains Vector2 </summary>
    [Pure]
    public readonly bool Contains(Vector2 pt)
    {
        return Contains(pt.X, pt.Y);
    }

    /// <summary> Contains Rectangle </summary>
    public readonly bool Contains(Rectangle rect)
    {
        return (X <= rect.X) && ((rect.X + rect.Width) <= (X + Width)) && (Y <= rect.Y) && ((rect.Y + rect.Height) <= (Y + Height));
    }

    /// <summary> Contains x,y </summary>
    public readonly bool Contains(float x, float y)
    {
        return X <= x && x < X + Width && Y <= y && y < Y + Height;
    }

    /// <summary> Inflate Rectangle </summary>
    public void Inflate(Vector2 size)
    {
        Inflate(size.X, size.Y);
    }

    /// <summary> Inflate Rectangle </summary>
    public void Inflate(float width, float height)
    {
        X -= width;
        Y -= height;
        Width += 2 * width;
        Height += 2 * height;
    }

    /// <summary> Intersect Rectangle </summary>
    public void Intersect(Rectangle rect)
    {
        Rectangle result = Intersect(rect, this);

        X = result.X;
        Y = result.Y;
        Width = result.Width;
        Height = result.Height;
    }

    /// <summary> Intersect Rectangle </summary>
    public static Rectangle Intersect(Rectangle a, Rectangle b)
    {
        float x1 = Math.Max(a.X, b.X);
        float x2 = Math.Min(a.X + a.Width, b.X + b.Width);
        float y1 = Math.Max(a.Y, b.Y);
        float y2 = Math.Min(a.Y + a.Height, b.Y + b.Height);

        if (x2 >= x1 && y2 >= y1)
        {
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }
        return Empty;
    }

    /// <summary> Intersects with Rectangle </summary>
    public readonly bool IntersectsWith(Rectangle rect)
    {
        return (rect.X < X + Width) && (X < (rect.X + rect.Width)) && (rect.Y < Y + Height) && (Y < rect.Y + rect.Height);
    }

    /// <summary> Offset Rectangle </summary>
    public void Offset(Vector2 pos)
    {
        Offset(pos.X, pos.Y);
    }

    /// <summary> Offset Rectangle </summary>
    public void Offset(float x, float y)
    {
        X += x;
        Y += y;
    }

    /// <summary> Operator Overloading </summary>
    public static explicit operator Vector4(Rectangle rectangle)
    {
        return new Vector4(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
    }

    /// <summary> Operator Overloading </summary>
    public static explicit operator Rectangle(Vector4 vector)
    {
        return new Rectangle(vector);
    }

    /// <summary> Operator Overloading </summary>
    public static bool operator ==(Rectangle left, Rectangle right)
    {
        return left.Equals(right);
    }

    /// <summary> Operator Overloading </summary>
    public static implicit operator Rectangle(System.Drawing.RectangleF r)
    {
        return new Rectangle(r.X, r.Y, r.Width, r.Height);
    }

    /// <summary> Operator Overloading </summary>
    public static bool operator !=(Rectangle left, Rectangle right)
    {
        return !(left == right);
    }

    /// <summary> Stringify struct </summary>
    public override readonly string ToString()
    {
        return $"{{X={X}, Y={Y}, Width={Width}, Height={Height}}}";
    }

    /// <summary> To Vector4 </summary>
    public readonly Vector4 ToVector4()
    {
        return new Vector4(X, Y, Width, Height);
    }

    /// <summary> Union </summary>
    public static Rectangle Union(Rectangle a, Rectangle b)
    {
        float x1 = Math.Min(a.X, b.X);
        float x2 = Math.Max(a.X + a.Width, b.X + b.Width);
        float y1 = Math.Min(a.Y, b.Y);
        float y2 = Math.Max(a.Y + a.Height, b.Y + b.Height);

        return new Rectangle(x1, y1, x2 - x1, y2 - y1);
    }

    /// <summary> GetHashCode </summary>
    public override readonly int GetHashCode()
    {
        return (int)((uint)X ^ (((uint)Y << 13) | ((uint)Y >> 19)) ^ (((uint)Width << 26) | ((uint)Width >> 6)) ^ (((uint)Height << 7) | ((uint)Height >> 25)));
    }

    /// <summary> Equals </summary>
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        return Equals(obj);
    }

    /// <summary> Equals </summary>
    public readonly bool Equals(Rectangle other)
    {
        return (other.X == X) && (other.Y == Y) && (other.Width == Width) && (other.Height == Height);
    }
}

#pragma warning restore
