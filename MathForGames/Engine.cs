using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MathLibrary;
using System.Diagnostics;
using Raylib_cs;

namespace MathForGames
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];
        private Stopwatch _stopwatch = new Stopwatch();

        /// <summary>
        /// Called to begin the application
        /// </summary>
        public void Run()
        {
            //Call Start for the entire application
            Start();

            float currentTime = 0;
            float lastTime = 0;
            float deltaTime = 0;
            //Loop until the application is told to close
            while(!_applicationShouldClose && !Raylib.WindowShouldClose())
            {
                //Gets how mych time has passed since application started
                currentTime = _stopwatch.ElapsedMilliseconds / 1000.0f;

                //Set delta time to be the difference in time from the last time recorded to the current time.
                deltaTime = currentTime - lastTime;

                //Updates the application
                Update(deltaTime);
                //Draw all items
                Draw();

                //Sets the last time recorded to be the current time
                lastTime = currentTime;
            }

            //Calls end for the entire application
            End();
        }

        /// <summary>
        /// Called when the application starts
        /// </summary>
        private void Start()
        {
            //Begins the stopwatch
            _stopwatch.Start();

            //Create a window using RayLib
            Raylib.InitWindow(800, 450, "Math For Games");
            Raylib.SetTargetFPS(0);

            Scene scene = new Scene();
            AddScene(scene);
            Actor sun = new Actor(390, 225, "Sun", "Images/sun.png");
            Actor mercury = new Actor(0.4f, 0.225f, "Mercury", "Images/mercury.png");
            sun.Collider = new CircleCollider(50, sun);
            sun.SetScale(150, 140);
            mercury.SetScale(0.2f, 0.16f);
            scene.AddActor(sun);
            scene.AddActor(mercury);
            sun.AddChild(mercury);
            _scenes[_currentSceneIndex].Start();

        }

        /// <summary>
        /// Called everytime the game loops
        /// </summary>
        private void Update(float deltaTime)
        {
            _scenes[_currentSceneIndex].Update(deltaTime);
        }

        /// <summary>
        /// Called every time the game loops to update visuals
        /// </summary>
        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);

            //Adds all actor icons to buffer
            _scenes[_currentSceneIndex].Draw();

            Raylib.EndDrawing();
        }

        /// <summary>
        /// Called when the appliication Exits
        /// </summary>
        private void End()
        {
            _scenes[_currentSceneIndex].End();
            Raylib.CloseWindow(); 
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

        public static void CloseApplication()
        {
            _applicationShouldClose = true;
        }
    }
}
