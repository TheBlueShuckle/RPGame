using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Runtime.CompilerServices;

namespace RPGame.Scipts
{
    static class RectangleHelper
    {
        public static bool TouchTop(this Rectangle rectangle1, Rectangle rectangle2)
        {
            return (rectangle1.Top <= rectangle2.Bottom &&
                    rectangle1.Top > rectangle2.Top &&
                    rectangle1.Right > rectangle2.Left &&
                    rectangle1.Left < rectangle2.Right);
        }

        public static bool TouchBottom(this Rectangle rectangle1, Rectangle rectangle2)
        {
            return (rectangle1.Bottom >= rectangle2.Top &&
                    rectangle1.Bottom < rectangle2.Bottom &&
                    rectangle1.Left < rectangle2.Right &&
                    rectangle1.Right > rectangle2.Left);
        }

        public static bool TouchToRight(this Rectangle rectangle1, Rectangle rectangle2)
        {
            return (rectangle1.Right >= rectangle2.Left &&
                    rectangle1.Right < rectangle2.Right &&
                    rectangle1.Bottom > rectangle2.Top &&
                    rectangle1.Top < rectangle2.Bottom);
        }

        public static bool TouchToLeft(this Rectangle rectangle1, Rectangle rectangle2)
        {
            return (rectangle1.Left <= rectangle2.Right &&
                    rectangle1.Left > rectangle2.Left &&
                    rectangle1.Bottom > rectangle2.Top &&
                    rectangle1.Top < rectangle2.Bottom);
        }
    }
}
