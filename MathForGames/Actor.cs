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
        private bool _started;
        private Vector2 _forward = new Vector2(1, 0);
        private Collider _collider;
        private Matrix3 _globalTransform = Matrix3.Identity;
        private Matrix3 _localTransform = Matrix3.Identity;
        private Matrix3 _translation = Matrix3.Identity;
        private Matrix3 _rotation = Matrix3.Identity;
        private Matrix3 _scale = Matrix3.Identity;
        private Actor _parent;
        private Actor[] _children = new Actor[0];
        private Sprite _sprite;

        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// True if the start function has been called for this actor
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        /// <summary>
        /// The forwards facing rotation of the actor
        /// </summary>
        public Vector2 Forwards
        {
            get { return new Vector2(_rotation.M00, _rotation.M10); }
            set
            {
                Vector2 point = value.Normalized + WorldPosition;
                LookAt(point);
            }
        }

        /// <summary>
        /// The Collider attached to the Actor
        /// </summary>
        public Collider Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        public Matrix3 GlobalTransform
        {
            get { return _globalTransform; }
            private set { _globalTransform = value; }
        }

        public Matrix3 LocalTransform
        {
            get { return _localTransform; }
            private set { _localTransform = value; }
        }

        /// <summary>
        /// The parent of the current actor (if any)
        /// </summary>
        public Actor Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// The children of this actor (if any)
        /// </summary>
        public Actor[] Children
        {
            get { return _children; }
        }

        /// <summary>
        /// The actor's sprite
        /// </summary>
        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        /// <summary>
        /// The position of the actor relative to the parent
        /// </summary>
        public Vector2 LocalPosition
        {
            get { return new Vector2(_localTransform.M02 + WorldPosition.x, _localTransform.M12 + WorldPosition.y); }
            set { SetTranslation(value.x + WorldPosition.x, value.y + WorldPosition.y); }
        }

        /// <summary>
        /// The absolute position of the actor, regardless of parent
        /// </summary>
        public Vector2 WorldPosition
        {
            //Return the Global Transforms T Column
            get { return new Vector2(_translation.M02, _translation.M12); }
            set 
            {
                //If the actor has a Parent
                if (Parent != null)
                {
                    //Offset the values by the Parents and tranlate the actor
                    float xScale = (value.x - Parent.WorldPosition.x) / new Vector2(_globalTransform.M00, _globalTransform.M10).Magnitude;
                    float yScale = (value.y - Parent.WorldPosition.y) / new Vector2(_globalTransform.M10, _globalTransform.M11).Magnitude;
                    SetTranslation(xScale, yScale);
                }
                //Else change the Local Position to the given values
                else
                    LocalPosition = value;
            }
        }

        /// <summary>
        /// The Size of the actor
        /// </summary>
        public Vector2 Size
        {
            get 
            {
                float xScale = new Vector2(_scale.M00, _scale.M10).Magnitude;
                float yScale = new Vector2(_scale.M01, _scale.M11).Magnitude;
                return new Vector2(xScale, yScale); 
            }
            set { SetScale(value.x, value.y); }
        }

        /// <summary>
        /// A Empty Actor Constructor
        /// </summary>
        public Actor() { }

        /// <summary>
        /// The base Actor Constructor
        /// </summary>
        /// <param name="position">The position of the actor</param>
        /// <param name="name">The actor's name</param>
        /// <param name="path">The path of the actor's sprite</param>
        public Actor(Vector2 position, string name = "Actor", string path = "")
        {
            LocalPosition = position;
            _name = name;

            if (path != "")
                _sprite = new Sprite(path);
        }

        public Actor(float x, float y, string name = "Actor", string path = "") :
            this(new Vector2 { x = x, y = y }, name, path)
        { }

        /// <summary>
        /// Updates the Position, rotation, and size of the Actor
        /// </summary>
        public void UpdateTransforms()
        {
            LocalTransform = _translation * _rotation * _scale;
            if (Parent != null)
                GlobalTransform = Parent.GlobalTransform * LocalTransform;
            else 
                GlobalTransform = LocalTransform;
        }

        /// <summary>
        /// Adds an actor to the scenes list of actors
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(Actor child)
        {
            //Creates a temp array larger than the original
            Actor[] tempArray = new Actor[_children.Length + 1];

            //Copies all values from the orginal array into the temp array
            for (int i = 0; i < _children.Length; i++)
                tempArray[i] = _children[i];
            //Adds the new actor to the end of the new array
            tempArray[_children.Length] = child;

            //Merges the arrays
            _children = tempArray;

            //Link the child to the actor
            child.Parent = this;
        }

        /// <summary>
        /// Removes the child from the Actor
        /// </summary>
        /// <param name="child">The child to remove</param>
        /// <returns>If the removal was successful</returns>
        public bool RemoveChild(Actor child)
        {
            //Creates a variable to store if the removal was successful
            bool actorRemoved = false;

            //Creates a new rray that is smaller than the original
            Actor[] tempArray = new Actor[_children.Length - 1];

            //Copies all values from the orginal array into the temp array unless it is the removed actor
            int j = 0;
            for (int i = 0; i < _children.Length; i++)
            {
                if (_children[i] != child)
                {
                    tempArray[j] = _children[i];
                    j++;
                }
                else
                    actorRemoved = true;
            }

            //Merges the arrays
            if (actorRemoved)
            {
                _children = tempArray;
                //Removes the child from the actor
                child.Parent = null;
            }
            
            return actorRemoved;
        }


        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(float deltaTime)
        {
            if (Name != "Space" && Name != "UFO")
                Rotate(0.001f);
            if (Name == "Uranus")
                Rotate(-0.002f);
            if (Name == "Saturn's Rings")
                Rotate(-0.005f);


            UpdateTransforms();
        }

        public virtual void Draw()
        {
            if (_sprite != null)
                _sprite.Draw(GlobalTransform);
        }

        public void End()
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

        /// <summary>
        /// Sets the position of the actor
        /// </summary>
        /// <param name="translationX">The new x position</param>
        /// <param name="translationY">The new y position</param>
        public void SetTranslation(float translationX, float translationY)
        {
            _translation = Matrix3.CreateTranslation(translationX, translationY);
        }

        /// <summary>
        /// Applies the given values to the current translation
        /// </summary>
        /// <param name="translationX">The amount to move on the x</param>
        /// <param name="translationY">The amount to move on the yparam>
        public void Translate(float translationX, float translationY)
        {
            _translation *= Matrix3.CreateTranslation(translationX, translationY);
        }

        /// <summary>
        /// Set the rotation of the actor.
        /// </summary>
        /// <param name="radians">The angle of the new rotation in radians.</param>
        public void SetRotation(float radians)
        {
            _rotation = Matrix3.CreateRotation(radians);
        }

        /// <summary>
        /// Adds a roation to the current transform's rotation.
        /// </summary>
        /// <param name="radians">The angle in radians to turn.</param>
        public void Rotate(float radians)
        {
            _rotation *= Matrix3.CreateRotation(radians);
        }

        /// <summary>
        /// Sets the scale of the actor.
        /// </summary>
        /// <param name="x">The value to scale on the x axis.</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(x, y);
        }

        /// <summary>
        /// Scales the actor by the given amount.
        /// </summary>
        /// <param name="x">The value to scale on the x axis.</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void Scale(float x, float y)
        {
            _scale *= Matrix3.CreateScale(x, y);
        }

        /// <summary>
        /// Rotates the actor to face the given position
        /// </summary>
        /// <param name="position">The position the actor should be looking towards</param>
        public void LookAt(Vector2 position)
        {
            //Find the direction the the actor should look in
            Vector2 direction = (position - WorldPosition).Normalized;

            //Use the dot product to find the angle the actor needs to rotate
            float dotProd = Vector2.DotProduct(direction, Forwards);

            if (dotProd > 1)
                dotProd = 1;

            float angle = (float)Math.Acos(dotProd);


            //Find a perpindicular vector to the direction
            Vector2 perpDirection = new Vector2(direction.y, -direction.x);

            //Find the dot product of the perpindicular vector and the current forward
            float perpDot = Vector2.DotProduct(perpDirection, Forwards);

            //If the result isn't 0, use it to change the sign of the angle to be either positive or negative
            if (perpDot != 0)
                angle *= -perpDot / Math.Abs(perpDot);

            Rotate(angle);
        }
    }
}
