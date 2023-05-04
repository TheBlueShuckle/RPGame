using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace RPGame.Scipts
{
    internal class Map : Main
    {
        static int tileLength { get; set; }

        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        Texture2D texture;

        List<Tile> tiles = new List<Tile>();

        public Map(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
            this.texture = texture;

            tileLength = Window.ClientBounds.Width / 20;

            tiles.Add(new Tile(spriteBatch, graphics, texture, tileLength, Color.Green, new Vector2(0, 0), true));
            tiles.Add(new Tile(spriteBatch, graphics, texture, tileLength, Color.Brown, new Vector2(tileLength, 0), true));
            tiles.Add(new Tile(spriteBatch, graphics, texture, tileLength, Color.Blue, new Vector2(2*tileLength, 0), false));
        }

        public void Draw()
        {
            foreach(Tile tile in tiles)
            {
                tile.Draw();
            }
        }

        public List<Tile> GetImpassableTiles()
        {
            List<Tile> impassableTiles = new List<Tile>();

            foreach (Tile tile in tiles)
            {
                if (tile.GetPassability())
                {
                    impassableTiles.Add(tile);
                }
            }

            return impassableTiles;
        }
    }
}
