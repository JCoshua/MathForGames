using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace MathForGames
{
    class UIText : Actor
    {
        public string Text;
        public int Width;
        public int Height;
        public int FontSize;
        public Font Font;
        public Color FontColor;

        /// <summary>
        /// The UIText Constructor
        /// </summary>
        /// <param name="x">The X Position for the Text Box</param>
        /// <param name="y">The Y Position for the Text Box</param>
        /// <param name="name">The Name of the box</param>
        /// <param name="color">The Text Color</param>
        /// <param name="width">The length of the text box</param>
        /// <param name="height">the width</param>
        /// <param name="text">the text within the box</param>
        public UIText(float x, float y, float z, string name, Color color, int width, int height, int fontSize, string text = "", Shape shape = Shape.SPHERE)
            : base(x, y, z, name, shape)
        {
            Text = text;
            Width = width;
            Height = height;
            Font = Raylib.LoadFont("resources/fonts/alagard.png");
            FontSize = fontSize;
            FontColor = color;
        }

        public override void Update(float DeltaTime)
        {

        }

        public override void Draw()
        {
            Rectangle textBox = new Rectangle(LocalPosition.x, LocalPosition.y, Width, Height);
            Raylib.DrawTextRec(Font, Text, textBox, FontSize, 1, true, FontColor);
        }
    }
}
