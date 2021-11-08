using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    public enum Shape
    {
        CUBE,
        SPHERE
    }

    class Actor
    {
        private string _name;
        private bool _started;
        private Vector3 _forwards = new Vector3(0, 0, 1);
        private Collider _collider;
        private Matrix4 _globalTransform = Matrix4.Identity;
        private Matrix4 _localTransform = Matrix4.Identity;
        private Matrix4 _translation = Matrix4.Identity;
        private Matrix4 _rotation = Matrix4.Identity;
        private Matrix4 _scale = Matrix4.Identity;
        private Actor _parent;
        private Actor[] _children = new Actor[0];
        private Shape _shape;
        private Color _color;
        public bool IsActorGrounded = true;

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
        public Vector3 Forward
        {
            get { return new Vector3(_rotation.M02, _rotation.M12, _rotation.M22); }
            set
            {
                Vector3 point = value.Normalized + WorldPosition;
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

        public Matrix4 GlobalTransform
        {
            get { return _globalTransform; }
            private set { _globalTransform = value; }
        }

        public Matrix4 LocalTransform
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
        /// The position of the actor relative to the parent
        /// </summary>
        public Vector3 LocalPosition
        {
            get { return new Vector3(_localTransform.M03 + WorldPosition.x, _localTransform.M13 + WorldPosition.y, _localTransform.M23 + WorldPosition.z); }
            set { SetTranslation(value.x + WorldPosition.x, value.y + WorldPosition.y, value.z + WorldPosition.z); }
        }

        /// <summary>
        /// The absolute position of the actor, regardless of parent
        /// </summary>
        public Vector3 WorldPosition
        {
            //Return the Global Transforms T Column
            get { return new Vector3 (_translation.M03, _translation.M13, _translation.M23); }
            set 
            {
                //If the actor has a Parent
                if (Parent != null)
                {
                    //Offset the values by the Parents and tranlate the actor
                    float xScale = (value.x - Parent.WorldPosition.x) / new Vector3(_globalTransform.M00, _globalTransform.M10, _globalTransform.M20).Magnitude;
                    float yScale = (value.y - Parent.WorldPosition.y) / new Vector3(_globalTransform.M01, _globalTransform.M11, _globalTransform.M21).Magnitude;
                    float zScale = (value.z - Parent.WorldPosition.z) / new Vector3(_globalTransform.M02, _globalTransform.M12, _globalTransform.M22).Magnitude;
                    SetTranslation(xScale, yScale, zScale);
                }
                //Else change the Local Position to the given values
                else
                    LocalPosition = value;
            }
        }

        /// <summary>
        /// The Color of the Actor
        /// </summary>
        public Color ShapeColor
        {
            get { return _color; }
        }

        /// <summary>
        /// The Size of the actor
        /// </summary>
        public Vector3 Size
        {
            get 
            {
                float xScale = new Vector3(_scale.M00, _scale.M10, _scale.M20).Magnitude;
                float yScale = new Vector3(_scale.M01, _scale.M11, _scale.M21).Magnitude;
                float zScale = new Vector3(_scale.M02, _scale.M12, _scale.M22).Magnitude;
                return new Vector3(xScale, yScale, zScale); 
            }
            set { SetScale(value.x, value.y, value.z); }
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
        public Actor(Vector3 position, string name = "Actor", Shape shape = Shape.SPHERE)
        {
            LocalPosition = position;
            _name = name;
            _shape = shape;

            if (_shape == Shape.SPHERE)
                Collider = new SphereCollider(this);
            if (_shape == Shape.CUBE)
                Collider = new AABBCollider(this.Size.x, this.Size.y, this);

        }

        public Actor(float x, float y, float z, string name = "Actor", Shape shape = Shape.SPHERE) :
            this(new Vector3 { x = x, y = y, z = z}, name, shape)
        { }

        /// <summary>
        /// Updates the Position, rotation, and size of the Actor
        /// </summary>
        public void UpdateTransforms()
        {
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
            LocalTransform = _translation * _rotation * _scale;
            UpdateTransforms();
        }

        public virtual void Draw()
        {
            System.Numerics.Vector3 position = new System.Numerics.Vector3(WorldPosition.x, WorldPosition.y, WorldPosition.z);

            switch (_shape)
            {
                case Shape.CUBE:
                    Raylib.DrawCube(position, Size.x, Size.y, Size.z, ShapeColor);
                    break;
                case Shape.SPHERE:
                    Raylib.DrawSphere(position, Size.x, ShapeColor);
                    break;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_TAB))
            {
                System.Numerics.Vector3 endPos = new System.Numerics.Vector3(WorldPosition.x + Forward.x * 10, WorldPosition.y + Forward.y * 10, WorldPosition.z + Forward.z * 10);
                Raylib.DrawLine3D(position, endPos, Color.RED);
                Collider.Draw();
            }
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
        /// <param name="translationZ">The new z position</param>
        public void SetTranslation(float translationX, float translationY, float translationZ)
        {
            _translation = Matrix4.CreateTranslation(translationX, translationY, translationZ);
        }

        /// <summary>
        /// Applies the given values to the current translation
        /// </summary>
        /// <param name="translationX">The amount to move on the x axis</param>
        /// <param name="translationY">The amount to move on the y axis</param>
        /// <param name="translationY">The amount to move on the z axis</param>
        public void Translate(float translationX, float translationY, float translationZ)
        {
            _translation *= Matrix4.CreateTranslation(translationX, translationY, translationZ);
        }

        /// <summary>
        /// Set the rotation of the actor.
        /// </summary>
        /// <param name="radians">The angle of the new rotation in radians.</param>
        public void SetRotation(float radiansX, float radiansY, float radiansZ)
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(radiansX);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansY);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(radiansZ);
            _rotation = rotationX * rotationY * rotationZ;
            
        }

        /// <summary>
        /// Adds a roation to the current transform's rotation.
        /// </summary>
        /// <param name="radians">The angle in radians to turn.</param>
        public void Rotate(float radiansX, float radiansY, float radiansZ)
        {
            Matrix4 rotationX = Matrix4.CreateRotationX(radiansX);
            Matrix4 rotationY = Matrix4.CreateRotationY(radiansY);
            Matrix4 rotationZ = Matrix4.CreateRotationZ(radiansZ);
            _rotation *= rotationX * rotationY * rotationZ;
        }

        /// <summary>
        /// Sets the scale of the actor.
        /// </summary>
        /// <param name="x">The value to scale on the x axis.</param>
        /// <param name="y">The value to scale on the y axis.</param>
        /// <param name="z">The value to scale on the z axis.</param>
        public void SetScale(float x, float y, float z)
        {
            _scale = Matrix4.CreateScale(x, y, z);
        }

        /// <summary>
        /// Scales the actor by the given amount.
        /// </summary>
        /// <param name="x">The value to scale on the x axis.</param>
        /// <param name="y">The value to scale on the y axis</param>
        /// <param name="z">The value to scale on the z axis</param>
        public void Scale(float x, float y, float z)
        {
            _scale *= Matrix4.CreateScale(x, y, z);
        }

        /// <summary>
        /// Rotates the actor to face the given position
        /// </summary>
        /// <param name="position">The position the actor should be looking towards</param>
        public void LookAt(Vector3 position)
        {
            //Find the direction the the actor should look in
            Vector3 direction = (position - WorldPosition).Normalized;

            //If the direction's length is 0
            if (direction.Magnitude == 0)
                //Set the direction to forwards
                direction = new Vector3(0, 0, 1);

            //Create a Vector that points up
            Vector3 alignAxis = new Vector3(0, 1, 0);

            //Creates Vectors that will be the new x and y axis
            Vector3 newYAxis = new Vector3(0, 1, 0);
            Vector3 newXAxis = new Vector3(1, 0, 0);

            //If the direction vector is facing directly up
            if (Math.Abs(direction.y) > 0 && direction.x == 0 && direction.z == 0)
            {
                //Set the Align Axis vector to face right
                alignAxis = new Vector3(1, 0, 0);

                //Get the Cross Product of the direction and the newly aligned axis to get the Y Axis
                newYAxis = Vector3.CrossProduct(direction, alignAxis).Normalize();
                //Get the Cross Product of the new Y Axis and the direction to get the X Axis
                newXAxis = Vector3.CrossProduct(newYAxis, direction).Normalize();
            }
            //If the direction vector is not facing directly up
            else
            {
                //Get the Cross Product of the aligned Axis and the direction to get the new X Axis
                newXAxis = Vector3.CrossProduct(alignAxis, direction).Normalize();
                //Get the Cross Product of the direction and the new X Axis to get the new X Axis
                newYAxis = Vector3.CrossProduct(direction, newXAxis).Normalize();
            }

            //Change the rotation with the new axis
            _rotation = new Matrix4(newXAxis.x, newYAxis.x, direction.x, 0,
                                    newXAxis.y, newYAxis.y, direction.y, 0,
                                    newXAxis.z, newYAxis.z, direction.z, 0,
                                    0, 0, 0, 1);
        }

        /// <summary>
        /// Sets a RayLib Color
        /// </summary>
        /// <param name="color">The color set</param>
        public void SetColor(Color color)
        {
            _color = color;
        }

        /// <summary>
        /// Creates the Color of an object using a Vector4
        /// </summary>
        /// <param name="colorValue">The Vector4 that holds the Color Values</param>
        public void SetColor(Vector4 colorValue)
        {
            _color = new Color((int)colorValue.x, (int)colorValue.x, (int)colorValue.z, (int)colorValue.w);
        }

        /// <summary>
        /// Creates a Color for an actor
        /// </summary>
        /// <param name="r">The Red Value</param>
        /// <param name="g">The Green Value</param>
        /// <param name="b">The Blue Value</param>
        /// <param name="a">Transparent</param>
        public void SetColor(int r, int g, int b, int a)
        {
            _color = new Color(r, g, b, a);
        }
    }
}
