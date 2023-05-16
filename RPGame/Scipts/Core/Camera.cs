using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGame.Scipts.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Scipts.Core
{
    internal class Camera
    {
        Matrix pos;

        public Matrix Transform { get; private set; }

        public void Follow(Player target)
        {
            pos = Matrix.CreateTranslation(
                -target.Pos.X - target.Rectangle.Width / 2,
                -target.Pos.Y - target.Rectangle.Height / 2,
                0);

             StopCamera(target, new Rectangle(new Point(0, 0), new Point(5120, 2880)));

            Matrix offset = Matrix.CreateTranslation(
                    Main.ScreenWidth / 2,
                    Main.ScreenHeight / 2,
                    0);

            Transform = pos * offset;
        }

        public void StopCamera(Player target, Rectangle border)
        {
            float x, y;

            x = -target.Pos.X - target.Rectangle.Width / 2;
            y = -target.Pos.Y - target.Rectangle.Height / 2;

            if (target.Pos.X < border.X + (Main.ScreenWidth / 2))
            {
                x = -Main.ScreenWidth / 2;
            }

            if (target.Pos.X > border.Right - (Main.ScreenWidth / 2))
            {
                x = -border.Right + Main.ScreenWidth / 2;
            }

            if (target.Pos.Y < border.Y + (Main.ScreenHeight / 2))
            {
                y = -Main.ScreenHeight / 2;
            }

            if (target.Pos.Y > border.Bottom - (Main.ScreenHeight / 2))
            {
                y = -border.Bottom + Main.ScreenHeight / 2;
            }

            pos = Matrix.CreateTranslation(x, y, 0);
        }

        //if target.pos < border.X + screenWidth / 2 OR target.pos > border.X - screenWidth / 2:
        //  pos = Matrix.CreateTranslation(1, same, 0)
        //if target.pos < border.Y + ScreenHeight / 2 OR target.pos > border.Y - screenHeight / 2:
        //  pos = Matrix.CreateTranslation(same, 1, 0)
    }
}
