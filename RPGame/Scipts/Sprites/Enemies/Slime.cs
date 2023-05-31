using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGame.Scipts.Handlers;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Scipts.Sprites.Enemies
{
    internal class Slime : Enemy
    {
        Texture2D texture;
        Vector2 size = new Vector2(10 * Main.Pixel, 10 * Main.Pixel);

        EnemyMovementHandler enemyMovementHandler;

        public override float HP { get; set; }

        public override float Damage { get; set; }

        public override Vector2 Position { get; set; }

        public override Rectangle Hitbox { get; set; }

        public Slime(float tileSize, Vector2 pos, Texture2D texture)
        {
            Position = pos;
            this.texture = texture;

            enemyMovementHandler = new EnemyMovementHandler(tileSize * 2, pos, size);

            Hitbox = new Rectangle(pos.ToPoint(), new Point(10, 10));
        }

        public override void Update(GameTime gameTime)
        {
            enemyMovementHandler.Update(gameTime, GetCenter());

            Position = enemyMovementHandler.Position;
            Hitbox = enemyMovementHandler.Rectangle;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Hitbox, Color.LightBlue);
        }

        private Point GetCenter()
        {
            return Hitbox.Center;
        }
    }
}
