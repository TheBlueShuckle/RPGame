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
        Rectangle hitbox;
        Texture2D texture;

        public override float HP { get; set; }

        public override float Damage { get; set; }

        public Slime(Vector2 pos, Texture2D texture)
        {
            this.pos = pos;
            this.texture = texture;
            hitbox = new Rectangle(pos.ToPoint(), new Point(30, 30));
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

            hitbox = new Rectangle(pos.ToPoint(), new Point(30, 30));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, Color.LightBlue);
        }

        private Vector2 GetCenter()
        {
            return new Vector2(hitbox.X + (hitbox.Width / 2), hitbox.Y + (hitbox.Height/ 2));
        }
    }
}
