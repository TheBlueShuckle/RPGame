﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace RPGame.Scipts.Components
{
    internal class Tile
    {
        const int PATH = 1, GRASS = 2, TREES = 3, WATER = 4;

        bool passable;
        Random random = new Random();

        public Rectangle Rectangle { get; set; }

        public Rectangle SourceRectangle { get; set; }

        public Vector2 Position { get; set; }

        public int Material { get; private set; }

        public Tile(int material, Rectangle rectangle)
        {
            Rectangle = rectangle;
            Position = new Vector2(rectangle.X, rectangle.Y);
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

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D tileSet)
        {
            spriteBatch.Draw(tileSet, Rectangle, SourceRectangle, Color.White);
        }

        public bool GetPassability()
        {
            return passable;
        }
    }
}
