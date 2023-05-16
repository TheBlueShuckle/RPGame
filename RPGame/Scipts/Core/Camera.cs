﻿using Microsoft.Xna.Framework;
using RPGame.Scipts.Components;

namespace RPGame.Scipts.Core
{
    internal class Camera
    {
        Matrix pos;
        Rectangle border;

        public Matrix Transform { get; private set; }

        public Camera(Rectangle border)
        {
            this.border = border;
        }

        public void Follow(Player target)
        {
            pos = Matrix.CreateTranslation(
                -target.Pos.X - target.Rectangle.Width / 2,
                -target.Pos.Y - target.Rectangle.Height / 2,
                0);

            if (!Main.EditMode)
            {
                StopCamera(target);
            }

            Matrix offset = Matrix.CreateTranslation(
                    Main.ScreenWidth / 2,
                    Main.ScreenHeight / 2,
                    0);

            Transform = pos * offset;
        }

        public void StopCamera(Player target)
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
    }
}
