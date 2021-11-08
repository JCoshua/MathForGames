using System;

namespace MathLibrary
{
    public struct Vector4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Vector4(float X, float Y, float Z, float W)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        /// <summary>
        /// Gets the length of the Vector
        /// </summary>
        public float Magnitude
        {
            get { return (float)Math.Sqrt(x * x + y * y + z * z); }
        }

        /// <summary>
        /// Gets the normalized version of this vector without changing it
        /// </summary>
        public Vector4 Normalized
        {
            get
            {
                Vector4 value = this;
                return value.Normalize();
            }
        }
        /// <summary>
        /// Changes this vector to have a magnitude of one
        /// </summary>
        /// <returns>The result of the normalization, or an empty vector if magnitude is zero</returns>
        public Vector4 Normalize()
        {
            if (Magnitude == 0)
                return new Vector4();

            return this /= Magnitude;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lhs">The left hand side of the operation</param>
        /// <param name="rhs">The right hand side of the operation</param>
        /// <returns>The Dot Product of the first vector onto the second</returns>
        public static float DotProduct(Vector4 lhs, Vector4 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z + rhs.z;
        }

        public static Vector4 CrossProduct(Vector3 lhs, Vector3 rhs)
        {
            return new Vector4(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x, 0);
        }

        /// <summary>
        /// Gets the Angle of a Dot Product in Radian form
        /// </summary>
        /// <param name="lhs">The left hand side of the operation</param>
        /// <param name="rhs">The right hand side of the operation</param>
        /// <returns>The Radian of the Angle</returns>
        public static double GetRadian(Vector4 lhs, Vector4 rhs)
        {
            float dotProduct = DotProduct(lhs, rhs);
            if (dotProduct > 1)
                dotProduct = 1;
            if (dotProduct < -1)
                dotProduct = -1;
            return Math.Acos(dotProduct);
        }

        /// <summary>
        /// Gets the Angle of a Dot Product in Radian form
        /// </summary>
        /// <param name="dotProduct">The Dot Product</param>
        /// <returns>The Radian of the Angle</returns>
        public static double GetRadian(float dotProduct)
        {
            if (dotProduct > 1)
                dotProduct = 1;
            if (dotProduct < -1)
                dotProduct = -1;
            return Math.Acos(dotProduct);
        }

        /// <summary>
        /// Gets the Angle of a Dot Product in Degree form
        /// </summary>
        /// <param name="lhs">The left hand side of the operation</param>
        /// <param name="rhs">The right hand side of the operation</param>
        /// <returns>The Degree of the Angle</returns>
        public static double GetDegree(Vector4 lhs, Vector4 rhs)
        {
            float dotProduct = DotProduct(lhs, rhs);
            if (dotProduct > 1)
                dotProduct = 1;
            if (dotProduct < -1)
                dotProduct = -1;
            return Math.Acos(dotProduct) * (180 / Math.PI);
        }

        /// <summary>
        /// Gets the Angle of a Dot Product in Degree form
        /// </summary>
        /// <param name="dotProduct">The Dot Product</param>
        /// <returns>The Degree of the Angle</returns>
        public static double GetDegree(float dotProduct)
        {
            if (dotProduct > 1)
                dotProduct = 1;
            if (dotProduct < -1)
                dotProduct = -1;
            return Math.Acos(dotProduct) * (180 / Math.PI);
        }

        /// <summary>
        /// Finds the distatnce from the first vector to the second
        /// </summary>
        /// <param name="lhs">The Starting point</param>
        /// <param name="rhs">The Ending Point</param>
        /// <returns></returns>
        public static float Distance(Vector4 lhs, Vector4 rhs)
        {
            return (rhs - lhs).Magnitude;
        }


        /// <summary>
        /// Adds the x and y values of two vectors together.
        /// </summary>
        /// <param name="lhs">The First Vector</param>
        /// <param name="rhs">The Second Vector</param>
        /// <returns>The result of the addition</returns>
        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4 { x = lhs.x + rhs.x, y = lhs.y + rhs.y, z = lhs.z + rhs.z, w = lhs.w + rhs.w };
        }

        /// <summary>
        /// Subtracts the x and y values of the second vector from the values of the first vector.
        /// </summary>
        /// <param name="lhs">The First Vector</param>
        /// <param name="rhs">The Second Vector</param>
        /// <returns>The result of the subtraction</returns>
        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4 { x = lhs.x - rhs.x, y = lhs.y - rhs.y, z = lhs.z - rhs.z, w = lhs.w - rhs.w };
        }

        /// <summary>
        /// Multiplies the x and y values of two vectors.
        /// </summary>
        /// <param name="lhs">The first vector</param>
        /// <param name="rhs">The Second Vector</param>
        /// <returns>The result of the multiplication</returns>
        public static Vector4 operator *(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4 { x = lhs.x * rhs.x, y = lhs.y * rhs.y, z = lhs.z * rhs.z, w = lhs.w * rhs.w };
        }

        /// <summary>
        /// Divides the x and y values of the second vector from the value of the first vector.
        /// </summary>
        /// <param name="lhs">The first vector</param>
        /// <param name="rhs">The Second Vector</param>
        /// <returns>The result of the multiplication</returns>
        public static Vector4 operator /(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4 { x = lhs.x / rhs.x, y = lhs.y / rhs.y, z = lhs.z / rhs.z, w = lhs.w / rhs.w };
        }

        /// <summary>
        /// Multiplies the x and y values of the vector by the scaler
        /// </summary>
        /// <param name="vector">The vector being scaled</param>
        /// <param name="scaler">The scaler of the vector</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector4 operator *(Vector4 vector, float scaler)
        {
            return new Vector4 { x = vector.x * scaler, y = vector.y * scaler, z = vector.z * scaler, w = vector.w };
        }
        /// <summary>
        /// Divides the x and y values of the vector by the scaler
        /// </summary>
        /// <param name="vector">The vector being scaled</param>
        /// <param name="scaler">The scaler of the vector</param>
        /// <returns>The result of the vector scaling</returns>
        public static Vector4 operator /(Vector4 vector, float scaler)
        {
            return new Vector4 { x = vector.x / scaler, y = vector.y / scaler, z = vector.z / scaler, w = vector.w };
        }

        /// <summary>
        /// Compares the values of two vectors
        /// </summary>
        /// <param name="lhs">The first Vector</param>
        /// <param name="rhs">The Second Vector</param>
        /// <returns>True if the values are equal</returns>
        public static bool operator ==(Vector4 lhs, Vector4 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w;
        }

        /// <summary>
        /// Compares the values of two vectors
        /// </summary>
        /// <param name="lhs">The first Vector</param>
        /// <param name="rhs">The Second Vector</param>
        /// <returns>True if the values are not equal</returns>
        public static bool operator !=(Vector4 lhs, Vector4 rhs)
        {
            return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z || lhs.z != rhs.w;
        }
    }
}
