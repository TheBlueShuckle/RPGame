using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Scipts.Sprites.Enemies
{
    internal class Slime : Enemy
    {
        Vector2 pos, velocity = Vector2.Zero;
        Texture2D texture;

        public override float HP { get; set; }

        public override float Damage { get; set; }

        public override Vector2 Position { get; set; }

        public override Rectangle Rectangle { get; set; }

        public Slime(Vector2 pos, Texture2D texture)
        {
            this.pos = pos;
            this.texture = texture;
            Rectangle = new Rectangle(pos.ToPoint(), new Point(30, 30));
        }

        public override void Update(GameTime gameTime)
        {
            if (GetCenter().X > 0)
            {
                velocity.X = 3;
            }

            if (GetCenter().X > 100)
            {
                velocity.X = -3;
            }

            pos += velocity;

            Rectangle = new Rectangle(pos.ToPoint(), new Point(30, 30));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.LightBlue);
        }

        private Vector2 GetCenter()
        {
            return new Vector2(Rectangle.X + (Rectangle.Width / 2), Rectangle.Y + (Rectangle.Height/ 2));
        }
    }
}
