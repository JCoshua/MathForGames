using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class AABBCollider : Collider
    {
        private float _width;
        private float _height;

        /// <summary>
        /// The size of the collider on the x axis
        /// </summary>
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// The size of the collider on the y axis
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// The furthest Left X value of this collider
        /// </summary>
        public float Left
        {
            get
            {
                return Owner.Position.x - (Width / 2);
            }
        }

        /// <summary>
        /// The furthest right X value of this collider
        /// </summary>
        public float Right
        {
            get
            {
                return Owner.Position.x + (Width / 2);
            }
        }

        /// <summary>
        /// The Highest Y Value of this collider
        /// </summary>
        public float Top
        {
            get
            {
                return Owner.Position.y - (Height / 2);
            }
        }

        /// <summary>
        /// The Lowest Y Value of this collider
        /// </summary>
        public float Bottom
        {
            get
            {
                return Owner.Position.y + (Height / 2);
            }
        }

        public AABBCollider(float width, float height, Actor owner) : base(owner, ColliderType.AABB)
        {
            _width = width;
            _height = height;
        }

        public override bool CheckCollisionCircle(CircleCollider other)
        {
            return other.CheckCollisionAABB(this);
        }

        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //Checks if the other Collider and this collider belong to the same owner
            if (other.Owner == Owner)
                return false;

            //Returns True if there is an overlap betweens the two colliders
            return other.Left <= Right &&
                   other.Top <= Bottom &&
                   Left <= other.Right &&
                   Top <= other.Bottom;
        }

        public override void Draw()
        {
                Raylib.DrawRectangleLines((int)Left, (int)Top, (int)Width, (int)Height, Color.RED);
        }
    }
}
