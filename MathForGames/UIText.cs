using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace MathForGames
{
    class UIText : Actor
    {
        private string _text;
        private int _width;
        private int _height;

        /// <summary>
        /// The Text that is within the Text Box
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// The width of the text box. Wraps text if the cursor is outside the max width.
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// The height of the text box. Wraps text if the cursor is outside the max height.
        /// </summary>
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

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
        public UIText(float x, float y, string name, Color color, int width, int height, string text = "") 
            : base('\0', x, y, color, name)
        {
            Text = text;
            Width = width;
            Height = height;
        }

        public override void Update(float DeltaTime)
        {

        }

        public override void Draw()
        {
            int cursorPosX = (int)Position.x;
            int cursorPosY = (int)Position.y;

            Icon currentLetter = new Icon { Color = Icon.Color };

            char[] textChar = Text.ToCharArray();

            for (int i = 0; i < textChar.Length; i++)
            {
                currentLetter.Symbol = textChar[i];

                if (currentLetter.Symbol == '\0')
                {
                    cursorPosX = (int)Position.x;
                    cursorPosY++;
                    continue;
                }

                cursorPosX++;

                if(cursorPosX- (int)Position.x > Width)
                {
                    cursorPosX = (int)Position.x;
                    cursorPosY++;
                }

                if (cursorPosY - (int)Position.y > Height)
                {
                    break;
                }
            }
        }
    }
}
