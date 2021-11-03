﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class CircleCollider : Collider
    {
        private float _collisionRadius;

        public float CollisionRadius
        {
            get { return _collisionRadius; }
            set { _collisionRadius = value; }
        }

        public CircleCollider(float collisionRadius, Actor owner) : base(owner, ColliderType.CIRCLE)
        {
            CollisionRadius = collisionRadius;
        }

        public override bool CheckCollisionCircle(CircleCollider other)
        {
            //Checks if the other Collider and this collider belong to the same owner
            if (other.Owner == Owner)
                return false;

            //Finds the distatnce between the two actors
            float distance = Vector3.Distance(other.Owner.LocalPosition, Owner.LocalPosition);

            //Find the length of teh raddi combined
            float combinedRadii = other.CollisionRadius + CollisionRadius;

            return distance <= combinedRadii;
        }

        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //Checks if the other Collider and this collider belong to the same owner
            if (other.Owner == Owner)
                return false;

            //Gets the direction from the collider to the AABB
            Vector3 direction = Owner.LocalPosition - other.Owner.LocalPosition;

            //Clamps the direction to be within the AABB Collider
            direction.x = Math.Clamp(direction.x, -other.Width/2, other.Width/2);
            direction.y = Math.Clamp(direction.y, -other.Height/2, other.Height/2);

            //Finds the closest point by adding the direction vector to the AABB Center
            Vector3 closestPoint = other.Owner.LocalPosition + direction;

            //Finds the distance from the circle's center to the closest point
            float distanceFromClosestPoint = Vector3.Distance(Owner.LocalPosition, closestPoint);

            //Returns if the distance from the closest point is less than the Collision Radius
            return distanceFromClosestPoint <= CollisionRadius;
        }

        public override void Draw()
        {
            base.Draw();
            Raylib.DrawCircleLines((int)Owner.LocalPosition.x, (int)Owner.LocalPosition.y, CollisionRadius, Color.RED);
        }
    }
}
