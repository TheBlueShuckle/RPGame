using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace RPGame.Scipts.Components
{
    internal class Tile : Component
    {
        const int PATH = 1, GRASS = 2, TREES = 3, WATER = 4;

        Rectangle rectangle;
        Texture2D texture;
        Color color;
        bool passable;

        public int Material { get; private set; }

        public Tile(Texture2D texture, int material, Rectangle rectangle)
        {
            this.rectangle = rectangle;
            this.texture = texture;
            Material = material;
            SetProperties(material);
        }

        private void SetProperties(int material)
        {
            switch (material)
            {
                case PATH:
                    color = Color.SandyBrown;
                    passable = true;
                    break;

                case GRASS:
                    color = Color.Green;
                    passable = true;
                    break;

                case TREES:
                    color = Color.DarkGreen;
                    passable = false;
                    break;

                case WATER:
                    color = Color.Blue;
                    passable = false;
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }

        public bool GetPassability()
        {
            return passable;
        }

        public Rectangle GetRectangle()
        {
            return rectangle;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(rectangle.X, rectangle.Y);
        }

        public void SetColor()
        {
            color = Color.Red;
        }
    }
}
