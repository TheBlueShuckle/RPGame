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
        public Matrix Transform { get; private set; }

        public void Follow(Player target)
        {
            Matrix pos = Matrix.CreateTranslation(
                -target.Pos.X - target.Rectangle.Width / 2,
                -target.Pos.Y - target.Rectangle.Height / 2,
                0);

            Matrix offset = Matrix.CreateTranslation(
                    Main.ScreenWidth / 2,
                    Main.ScreenHeight / 2,
                    0);

            Transform = pos * offset;
        }

        //if target.pos < border.X + screenWidth / 2 OR target.pos > border.X - screenWidth / 2:
        //  pos = Matrix.CreateTranslation(Main.ScreenWidth / 2, same, 0)
        //if target.pos < border.Y + ScreenHeight / 2 OR target.pos > border.Y - screenHeight / 2:
        //  pos = Matrix.CreateTranslation(same, Matrix.ScreenWidth / 2, 0)
    }
}
