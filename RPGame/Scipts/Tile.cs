using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RPGame.Scipts
{
    internal class Tile
    {
        const int PATH = 1, GRASS = 2, TREES = 3, WATER = 4;

        Rectangle rectangle;
        Color color;
        bool passable;

        public Tile(int material, Rectangle rectangle)
        {
            this.rectangle = rectangle;
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

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }

        public bool GetPassability()
        {
            return passable;
        }
    }
}
