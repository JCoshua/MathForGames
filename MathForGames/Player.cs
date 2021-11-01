using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _velocity;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Player(float x, float y, float speed, string name = "Actor", string path = "")
            : base(x, y, name, path)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            //Get the player input direction
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            //Creates a vector that stores the move input
            Vector2 moveDirection = new Vector2(xDirection, yDirection);

            if (xDirection != 0 || yDirection != 0)
            {
                Velocity = moveDirection.Normalized * Speed * deltaTime;

                if (Velocity.Magnitude > 0)
                    Forwards = Velocity.Normalized;

                Translate(Velocity.x, Velocity.y);
            }
            base.Update(deltaTime);
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void OnCollision(Actor actor)
        {
            if (actor.Name == "HorizontalWall")
            {
                Translate(0, -Forwards.y);
                Console.WriteLine("Vertical Collision");
            }
            else if (actor.Name == "VerticalWall")
            {
                Translate(-Forwards.x, 0);
                Console.WriteLine("Horizontal Collision");
            }
        }
    }
}
