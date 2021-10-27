using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Sprite
    {
        private Texture2D _texture;

        /// <summary>
        /// Width of the Texture
        /// </summary>
        public int Width
        {
            get { return _texture.width; }
            private set { _texture.width = value; }
        }

        /// <summary>
        /// Height of the Texture
        /// </summary>
        public int Height
        {
            get { return _texture.height; }
            private set { _texture.height = value; }
        }

        /// <param name="path">The file path of the image to use as the texture</param>
        public Sprite(string path)
        {
            _texture = Raylib.LoadTexture(path);
        }


        /// <summary>
        /// Draws the sprite using the rotation, translation and the scale of the given transform
        /// </summary>
        /// <param name="transform">The transform the sprite will use</param>
        public void Draw(Matrix3 transform)
        {
            //Finds the scale of the sprite
            Width = (int)Math.Round(new Vector2(transform.M00, transform.M10).Magnitude);
            Height = (int)Math.Round(new Vector2(transform.M01, transform.M11).Magnitude);

            System.Numerics.Vector2 position = new System.Numerics.Vector2(transform.M02, transform.M12);
        }
    }
}
