using System;

namespace MathLibrary
{
    public struct Vector2
    {
        public float x;
        public float y;

        /// <summary>
        /// Adds the x and y values of two vectors together.
        /// </summary>
        /// <param name="lhs">The First Vector</param>
        /// <param name="rhs">The Second Vector</param>
        /// <returns>The result of the addition</returns>
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { x = lhs.x + rhs.x, y = lhs.y + rhs.y };
        }

        /// <summary>
        /// Subtracts the x and y values of the second vector from the values of the first vector.
        /// </summary>
        /// <param name="lhs">The First Vector</param>
        /// <param name="rhs">The Second Vector</param>
        /// <returns>The result of the subtraction</returns>
        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { x = lhs.x - rhs.x, y = lhs.y - rhs.y };
        }

        /// <summary>
        /// Multiplies the x and y values of two vectors.
        /// </summary>
        /// <param name="lhs">The first vector</param>
        /// <param name="rhs">The Second Vector</param>
        /// <returns>The result of the multiplication</returns>
        public static Vector2 operator *(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { x = lhs.x * rhs.x, y = lhs.y * rhs.y };
        }

        /// <summary>
        /// Divides the x and y values of the second vector from the value of the first vector.
        /// </summary>
        /// <param name="lhs">The first vector</param>
        /// <param name="rhs">The Second Vector</param>
        /// <returns>The result of the multiplication</returns>
        public static Vector2 operator /(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { x = lhs.x / rhs.x, y = lhs.y / rhs.y };
        }

        /// <summary>
        /// Multiplies the x and y values of the vector by the scaler
        /// </summary>
        /// <param name="vector">The vector being scaled</param>
        /// <param name="scaler">The scaler of the vector</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector2 operator *(Vector2 vector, float scaler)
        {
            return new Vector2 { x = vector.x * scaler, y = vector.y * scaler };
        }
        /// <summary>
        /// Divides the x and y values of the vector by the scaler
        /// </summary>
        /// <param name="vector">The vector being scaled</param>
        /// <param name="scaler">The scaler of the vector</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector2 operator /(Vector2 vector, float scaler)
        {
            return new Vector2 { x = vector.x / scaler, y = vector.y / scaler };
        }

        /// <summary>
        /// Compares the values of two vectors
        /// </summary>
        /// <param name="lhs">The first Vector</param>
        /// <param name="rhs">The Second Vector</param>
        /// <returns>True if the values are equal</returns>
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }

        /// <summary>
        /// Compares the values of two vectors
        /// </summary>
        /// <param name="lhs">The first Vector</param>
        /// <param name="rhs">The Second Vector</param>
        /// <returns>True if the values are not equal</returns>
        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return lhs.x != rhs.x || lhs.y != rhs.y;
        }
    }
}
