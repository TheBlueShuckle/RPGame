using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace RPGame.Scipts.Components
{
    internal class Tile
    {
        const int PATH = 1, GRASS = 2, TREES = 3, WATER = 4;

        bool passable;
        Random random = new Random();
        Vector2 position;

        public Rectangle Rectangle { get; set; }

        public Rectangle SourceRectangle { get; set; }

        public int Material { get; private set; }

        public Tile(int material, Rectangle rectangle)
        {
            Rectangle = rectangle;
            position = new Vector2(rectangle.X * Main.Pixel, rectangle.Y * Main.Pixel);
            Material = material;

            SetProperties(material);
        }

        private void SetProperties(int material)
        {
            switch (material)
            {
                case PATH:
                    if (SourceRectangle == Rectangle.Empty)
                    {
                        SourceRectangle = new Rectangle(48, 16, 16, 16);
                    }

                    passable = true;
                    break;

                case GRASS:
                    if (SourceRectangle == Rectangle.Empty)
                    {
                        SourceRectangle = GenerateSourceRectangle();
                    }
                    passable = true;
                    break;

                case TREES:
                    if (SourceRectangle == Rectangle.Empty)
                    {
                        SourceRectangle = new Rectangle(96, 0, 16, 16);
                    }
                    passable = false;
                    break;

                case WATER:
                    if (SourceRectangle == Rectangle.Empty)
                    {
                        SourceRectangle = new Rectangle(0, 0, 16, 16);
                    }
                    passable = false;
                    break;
            }
        }

        private Rectangle GenerateSourceRectangle()
        {
            int randomNumber = random.Next(1, 4);
            Rectangle sourceRectangle = new Rectangle(0, 96, 16, 16);

            if (randomNumber == 3)
            {
                sourceRectangle = new Rectangle(128 + 16 * random.Next(0, 3), 16 * random.Next(0, 5), 16, 16);
            }

            return sourceRectangle;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D tileSet)
        {
            spriteBatch.Draw(tileSet, ScaledRectangle(), SourceRectangle, Color.White);
        }

        public bool GetPassability()
        {
            return passable;
        }

        public Rectangle ScaledRectangle()
        {
            return new Rectangle((int)(Rectangle.X * Main.Pixel), (int)(Rectangle.Y * Main.Pixel), (int)(Rectangle.Width * Main.Pixel), (int)(Rectangle.Height * Main.Pixel));
        }

        public Vector2 Position()
        {
            return position;
        }
    }
}
