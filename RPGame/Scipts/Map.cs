using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        Vector2[,] tileGrid = new Vector2[40, 30];

        public Map(SpriteBatch spriteBatch, GraphicsDeviceManager graphics, Texture2D texture)
        {
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
            this.texture = texture;

            tileLength = Window.ClientBounds.Width / 20;

            for (int y = 0; y < 30; y++)
            {
                for (int x = 0; x < 40; x++)
                {
                    tileGrid[x, y] = new Vector2(x * tileLength, y * tileLength);
                }
            }

            tiles.Add(new Tile(spriteBatch, graphics, texture, tileLength, Color.Green, new Vector2(0, 0), true));
            tiles.Add(new Tile(spriteBatch, graphics, texture, tileLength, Color.SandyBrown, new Vector2(tileLength, 0), true));
            tiles.Add(new Tile(spriteBatch, graphics, texture, tileLength, Color.Blue, new Vector2(2 * tileLength, 0), false));
            tiles.Add(new Tile(spriteBatch, graphics, texture, tileLength, Color.Green, new Vector2(0, tileLength), true));
            tiles.Add(new Tile(spriteBatch, graphics, texture, tileLength, Color.SandyBrown, new Vector2(0, 2 * tileLength), true));
        }

        public void Draw()
        {
            foreach (Vector2 tilePos in tileGrid)
            {
                spriteBatch.Draw(texture, new Rectangle(tilePos.ToPoint(), new Point(tileLength, tileLength)), Color.Black);
                spriteBatch.Draw(texture, new Rectangle(new Point((int)tilePos.X + 1, (int)tilePos.Y + 1), new Point(tileLength - 2, tileLength - 2)), Color.White);
            }

            foreach (Tile tile in tiles)
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
