using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace RPGame.Scipts.Components
{
    internal class Tile
    {
        const int PATH = 1, GRASS = 2, TREES = 3, WATER = 4;

        Texture2D tileSet;
        Rectangle sourceRectangle;
        bool passable;

        public Rectangle Rectangle { get; set; }

        public Vector2 Position { get; set; }

        public int Material { get; private set; }

        public Tile(Texture2D tileSet, int material, Rectangle rectangle)
        {
            Rectangle = rectangle;
            this.tileSet = tileSet;
            Position = new Vector2(rectangle.X, rectangle.Y);
            Material = material;
            SetProperties(material);
        }

        private void SetProperties(int material)
        {
            switch (material)
            {
                case PATH:
                    sourceRectangle = new Rectangle(48, 16, 16, 16);
                    passable = true;
                    break;

                case GRASS:
                    sourceRectangle = new Rectangle(128, 0, 16, 16);
                    passable = true;
                    break;

                case TREES:
                    sourceRectangle = new Rectangle(96, 0, 16, 16);
                    passable = false;
                    break;

                case WATER:
                    sourceRectangle = new Rectangle(0, 0, 16, 16);
                    passable = false;
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tileSet, Rectangle, sourceRectangle, Color.White);
        }

        public bool GetPassability()
        {
            return passable;
        }
    }
}
