using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Actor
    {
        private string _name;
        private Matrix3 _transform = Matrix3.Identity;
        private Matrix3 _translation = Matrix3.Identity;
        private Matrix3 _rotation = Matrix3.Identity;
        private Matrix3 _scale = Matrix3.Identity;
        private bool _started;
        private Vector2 _forwards = new Vector2(1, 0);
        private Collider _collider;
        private bool _toBeRemoved;
        private Sprite _sprite;

        public bool Started
        {
            get { return _started; }
        }

        public Vector2 Position
        {
            get { return new Vector2(_transform.M02, _transform.M12); }
            set
            {
                _transform.M02 = value.x;
                _transform.M12 = value.y;
            }
        }

        public String Name
        {
            get { return _name; }
        }

        public Vector2 Forwards
        {
            get { return _forwards; }
            set { _forwards = value; }
        }

        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        /// <summary>
        /// The Collider attached to the Actor
        /// </summary>
        public Collider Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        public bool ToBeRemoved
        {
            get { return _toBeRemoved; }
            set { _toBeRemoved = value; }
        }

        public Actor(float x, float y, string name = "Actor", string path = "") :
            this(new Vector2 { x = x, y = y }, name, path)
        { }

        public Actor(Vector2 position, string name = "Actor", string path = "")
        {
            Position = position;
            _name = name;
            if (path != "")
                _sprite = new Sprite(path);
        }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(float deltaTime)
        {
            _transform = _translation * _rotation * _scale;
        }

        public virtual void Draw()
        {
            if (_sprite != null)
                _sprite.Draw(_transform);
            if (Raylib.IsKeyDown(KeyboardKey.KEY_TAB))
                Collider.Draw();
        }

        public virtual void End()
        {

        }

        public virtual void OnCollision(Actor actor)
        {

        }

        /// <summary>
        /// Checks for actor collision
        /// </summary>
        /// <param name="other">The other actor to check collision against</param>
        /// <returns>True if the distance between the two actors is less than their combined radii</returns>
        public virtual bool CheckCollision(Actor other)
        {
            //Returns false if there is a null collider
            if (Collider == null || other.Collider == null)
                return false;

            return Collider.CheckCollision(other);
        }

        public void SetRotation(float radians)
        {

        }
        public void SetTranslation(float x, float y)
        {

        }

        public void SetScale(float x, float y)
        {
            _tranform.CreateScale(x, y);
        }
    }
}
