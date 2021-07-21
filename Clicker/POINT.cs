using System.Windows;

namespace Clicker
{
    /// <summary>
    /// struct POINT for importet method GetCursorPos with int fields
    /// </summary>
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Point(POINT point)
        {
            return new Point(point.X, point.Y);
        }
    }

}
