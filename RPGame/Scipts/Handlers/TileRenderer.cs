using Microsoft.Xna.Framework;
using RPGame.Scipts.Components;
using RPGame.Scipts.Core;


namespace RPGame.Scipts.Handlers
{
    internal class TileRenderer
    {
        float tileSize;
        Rectangle mapSize;

        public TileRenderer(float tileSize, Rectangle mapSize)
        {
            this.tileSize = tileSize;
            this.mapSize = mapSize;
        }

        public bool IsTileOnScreen(Tile tile, Vector2 playerPosition, Point playerCenter, float zoom)
        {
            bool isOnScreen = false;
            if (
                tile.ScaledRectangle().X + tileSize >= playerCenter.X - (Main.ScreenWidth / 2 / zoom) &&
                tile.ScaledRectangle().Right - tileSize <= playerCenter.X + (Main.ScreenWidth / 2 / zoom) &&
                tile.ScaledRectangle().Y + tileSize >= playerCenter.Y - (Main.ScreenHeight / 2 / zoom) &&
                tile.ScaledRectangle().Bottom - tileSize <= playerCenter.Y + (Main.ScreenHeight / 2 / zoom))
            {
                isOnScreen = true;
            }
            if (
                // if component.X is in range 
                playerPosition.X < Main.ScreenWidth / 2 / zoom &&
                tile.ScaledRectangle().Right - tileSize < Main.ScreenWidth / zoom &&
                  // if camera sees top of screen and component.Y is in range
                ((playerPosition.Y < Main.ScreenHeight / 2 / zoom &&
                  tile.Position().Y - tileSize < Main.ScreenHeight / zoom) ||
                  // else if camera sees bottom of screen and component.Y is in range
                 (playerPosition.Y > mapSize.Bottom - (Main.ScreenHeight / 2 / zoom) &&
                  tile.Position().Y + tileSize > (mapSize.Bottom - Main.ScreenHeight) / zoom) ||
                  // else if camera sees neither the bottom or the top and component.Y is in range
                 (playerPosition.Y > Main.ScreenHeight / 2 / zoom &&
                  playerPosition.Y < mapSize.Bottom - (Main.ScreenHeight / 2 / zoom) &&
                  tile.Position().Y - tileSize < playerPosition.Y + (Main.ScreenHeight / 2 / zoom) &&
                  tile.Position().Y + tileSize > playerPosition.Y - (Main.ScreenHeight / 2 / zoom))))
            {
                isOnScreen = true;
            }
            if (
                playerPosition.X > (mapSize.Right - (Main.ScreenWidth / 2) / zoom) &&
                tile.ScaledRectangle().X + tileSize > mapSize.Right - (Main.ScreenWidth / zoom) &&
                  // if camera sees top of screen and component.Y is in range
                ((playerPosition.Y < Main.ScreenHeight / 2 / zoom &&
                  tile.Position().Y - tileSize < Main.ScreenHeight / zoom) ||
                  // else if camera sees bottom of screen and component.Y is in range
                 (playerPosition.Y > mapSize.Bottom - (Main.ScreenHeight / 2 / zoom) &&
                  tile.Position().Y + tileSize > (mapSize.Bottom - (Main.ScreenHeight)) / zoom) ||
                  // else if camera sees neither the bottom or the top and component.Y is in range
                 (playerPosition.Y > Main.ScreenHeight / 2 / zoom &&
                  playerPosition.Y < mapSize.Bottom - (Main.ScreenHeight / 2 / zoom) &&
                  tile.Position().Y - tileSize < playerPosition.Y + (Main.ScreenHeight / 2 / zoom) &&
                  tile.Position().Y + tileSize > playerPosition.Y - (Main.ScreenHeight / 2 / zoom))))
            {
                isOnScreen = true;
            }
            if (
                playerPosition.Y < mapSize.Y + (Main.ScreenHeight / 2) / zoom &&
                tile.ScaledRectangle().Bottom - tileSize < Main.ScreenHeight / zoom &&
                  // if camera sees left of screen and component.X is in range
                ((playerPosition.X < Main.ScreenWidth / 2 / zoom &&
                  tile.Position().X - tileSize < Main.ScreenWidth / zoom) ||
                  // else if camera sees right of screen and component.X is in range
                 (playerPosition.X > mapSize.Right - (Main.ScreenWidth / 2 / zoom) &&
                  tile.Position().X + tileSize > (mapSize.Right - (Main.ScreenWidth)) / zoom) ||
                  // else if camera sees neither the bottom or the top and component.X is in range
                 (playerPosition.X > Main.ScreenWidth / 2 / zoom &&
                  playerPosition.X < mapSize.Right - (Main.ScreenWidth / 2 / zoom) &&
                  tile.Position().X - tileSize < playerPosition.X + (Main.ScreenWidth / 2 / zoom) &&
                  tile.Position().X + tileSize > playerPosition.X - (Main.ScreenWidth / 2 / zoom))))
            {
                isOnScreen = true;
            }
            if (
                playerPosition.Y > mapSize.Bottom - (Main.ScreenHeight / 2) / zoom &&
                tile.ScaledRectangle().Y + tileSize > mapSize.Bottom - (Main.ScreenHeight / zoom) &&
                  // if camera sees left of screen and component.X is in range
                ((playerPosition.X < Main.ScreenWidth / 2 / zoom &&
                 tile.Position().X - tileSize < Main.ScreenWidth / zoom) ||
                 // else if camera sees right of screen and component.X is in range
                 (playerPosition.X > mapSize.Right - (Main.ScreenWidth / 2 / zoom) &&
                  tile.Position().X + tileSize > (mapSize.Right - (Main.ScreenWidth) / zoom)) ||
                  // else if camera sees neither the bottom or the top and component.X is in range
                 (playerPosition.X > Main.ScreenWidth / 2 / zoom &&
                  playerPosition.X < mapSize.Right - (Main.ScreenWidth / 2 / zoom) &&
                  tile.Position().X - tileSize < playerPosition.X + (Main.ScreenWidth / 2 / zoom) &&
                  tile.Position().X + tileSize > playerPosition.X - (Main.ScreenWidth / 2 / zoom))))
            {
                isOnScreen = true;
            }

            return isOnScreen;
        }
    }
}
