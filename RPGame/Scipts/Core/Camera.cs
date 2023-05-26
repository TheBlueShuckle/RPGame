using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGame.Scipts.Components;
using SharpDX.Direct2D1.Effects;
using System.Security.Permissions;

namespace RPGame.Scipts.Core
{
    internal class Camera
    {
        Matrix transform, newCentre;
        Rectangle border;
        Vector2 centre;
        Viewport viewport;
        float zoom = 1;

        public Matrix Transform { get { return transform; } }

        public float X { get { return centre.X; } set { centre.X = value; } }

        public float Y { get { return centre.Y; } set { centre.Y = value; } }

        public float Zoom { get { return zoom; } set {  zoom = value; if (zoom > 3) { zoom = 3; } else if (zoom < 0.1) { zoom = 0.1f; } } }

        public Camera(Viewport viewport, Rectangle border)
        {
            this.viewport = viewport;
            this.border = border;
        }

        public void Update(Vector2 position)
        {
            centre = position;

            StopCamera(centre);

            transform = newCentre *
                        Matrix.CreateScale(Zoom) *
                        Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }

        public void StopCamera(Vector2 position)
        {
            float x, y;

            x = -centre.X;
            y = -centre.Y;

            if (position.X < border.X + (Main.ScreenWidth / 2) && !Main.EditMode)
            {
                x = -Main.ScreenWidth / 2;
            }

            if (position.X > border.Right - (Main.ScreenWidth / 2) && !Main.EditMode)
            {
                x = -border.Right + Main.ScreenWidth / 2;
            }

            if (position.Y < border.Y + (Main.ScreenHeight / 2) && !Main.EditMode)
            {
                y = -Main.ScreenHeight / 2;
            }

            if (position.Y > border.Bottom - (Main.ScreenHeight / 2) && !Main.EditMode)
            {
                y = -border.Bottom + Main.ScreenHeight / 2;
            }

            newCentre = Matrix.CreateTranslation(x, y, 0);
        }

        public Vector2 ScreenToWorldSpace(Vector2 point)
        {
            Matrix invertedMatrix = Matrix.Invert(Transform);
            return Vector2.Transform(point, invertedMatrix);
        }
    }
}
