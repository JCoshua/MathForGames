using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MathLibrary;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];
        private static Icon[,] _buffer;

        /// <summary>
        /// Called to begin the application
        /// </summary>
        public void Run()
        {
            //Call Start for the entire application
            Start();

            //Loop until the application is told to close
            while(!_applicationShouldClose)
            {
                Update();
                Draw();
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
                Thread.Sleep(60);
            }

            //Calls end for the entire application
            End();
        }

        /// <summary>
        /// Called when the application starts
        /// </summary>
        private void Start()
        {
            _scenes[_currentSceneIndex].Start();
        }

        /// <summary>
        /// Called everytime the game loops
        /// </summary>
        private void Update()
        {
            _scenes[_currentSceneIndex].Update();
        }

        /// <summary>
        /// Called every time the game loops to update visuals
        /// </summary>
        private void Draw()
        {
            Console.CursorVisible = false;
            //Clears the stuff that was on the screen in the last frame
            _buffer = new Icon[Console.WindowWidth - 1, Console.WindowHeight - 1];

            //Resets the cursor poistion to the top to draw over the screen
            Console.SetCursorPosition(0, 0);

            //Adds all actor icons to buffer
            _scenes[_currentSceneIndex].Draw();

            //Iterates through buffer
            for (int y = 0; y < _buffer.GetLength(1); y++)
            {
                for (int x = 0; x < _buffer.GetLength(0); x++)
                {
                    if (_buffer[x, y].Symbol == '\0')
                        _buffer[x, y].Symbol = ' ';
                    Console.ForegroundColor = _buffer[x, y].Color;
                    Console.Write(_buffer[x, y].Symbol);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Called when the appliication Exits
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
        }

        /// <summary>
        /// Adds a scene to the engine's scene array
        /// </summary>
        /// <param name="scene">That scene to be added</param>
        /// <returns>The Index of the added scene</returns>
        public int AddScene(Scene scene)
        {
            //Creates a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy akk values frin old array into the new array
            for (int i = 0; i < _scenes.Length; i++)
            {
                tempArray[i] = _scenes[i]; 
            }

            //Sets the last index to bee the new scene
            tempArray[_scenes.Length] = scene;

            //Sets the original array to be the new array
            _scenes = tempArray;

            //Retrun the last index
            return _scenes.Length - 1;
        }

        /// <summary>
        /// Gets the next key pressed in the input stream
        /// </summary>
        /// <returns>The key that was pressed (if any)</returns>
        public static ConsoleKey GetNextKey()
        {
            //Returns the current key being pressed
            if(Console.KeyAvailable)
            return Console.ReadKey(true).Key;

            //If there is no key being pressed
            return 0;
        }

        /// <summary>
        /// Adds the icon to the buffer to print to the screen in the next draw call.
        /// Prints the icon at the given postion in the buffer.
        /// </summary>
        /// <param name="icon">The icon to be drawn</param>
        /// <param name="postion">The position of the icon in the buffer</param>
        /// <returns>False if the postition is outsides the bounds of the buffer</returns>
        public static bool Render(Icon icon, Vector2 position)
        {
            //Checks if the position is out of bounds
            if (position.x < 0 || position.x >= _buffer.GetLength(0) || position.y < 0 || position.y >= _buffer.GetLength(1))
                return false;

            //Set the buffer at the index of the given position to be the icon
            _buffer[(int)position.x, (int)position.y] = icon;
            return true;
        }

        /// <summary>
        /// Ends the application
        /// </summary>
        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }
}
