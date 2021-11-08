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
        private Vector3 _velocity;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Player(float x, float y, float z, float speed, string name = "Actor", Shape shape = Shape.SPHERE)
            : base(x, y, z, name, shape)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            //Get the player input direction
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = 0;
            if (IsActorGrounded)
                yDirection = Convert.ToInt32(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE));
            int zDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            //Creates a vector that stores the move input
            Vector3 moveDirection = new Vector3(xDirection, yDirection, zDirection);
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if (Velocity.Magnitude > 0)
                Forward = Velocity.Normalized;

            Translate(Velocity.x, Velocity.y, Velocity.z);
            base.Update(deltaTime);
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void OnCollision(Actor actor)
        {
        }
    }
}
