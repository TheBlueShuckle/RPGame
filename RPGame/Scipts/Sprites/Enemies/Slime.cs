using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGame.Scipts.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame.Scipts.Sprites.Enemies
{
    internal class Slime : Enemy
    {
        Texture2D texture;
        
        EnemyMovementHandler enemyMovementHandler;

        public override float HP { get; set; }

        public override float Damage { get; set; }

        public override Vector2 Position { get; set; }

        public override Rectangle Rectangle { get; set; }

        public Slime(int tileSize, Vector2 pos, Texture2D texture)
        {
            Position = pos;
            this.texture = texture;

            enemyMovementHandler = new EnemyMovementHandler(tileSize * 2, pos, new Vector2(Main.ScreenWidth / 128, Main.ScreenWidth / 96));

            Rectangle = new Rectangle(pos.ToPoint(), new Point(30, 30));
        }

        public override void Update(GameTime gameTime)
        {
            enemyMovementHandler.Update(gameTime, GetCenter());

            Position = enemyMovementHandler.Position;
            Rectangle = enemyMovementHandler.Rectangle;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Rectangle, Color.LightBlue);
        }

        private Point GetCenter()
        {
            return Rectangle.Center;
        }
    }
}
