using System;

namespace MathLibrary
{
    public struct Matrix4
    {
        public float M00, M01, M02, M03 , M10, M11, M12, M13, M20, M21, M22, M23, M30, M31, M32, M33;

        public Matrix4(float m00, float m01, float m02, float m03,
                        float m10, float m11, float m12, float m13,
                        float m20, float m21, float m22, float m23,
                        float m30, float m31, float m32, float m33)
        {
            M00 = m00; M01 = m01; M02 = m02; M03 = m03;
            M10 = m10; M11 = m11; M12 = m12; M13 = m13;
            M20 = m20; M21 = m21; M22 = m22; M23 = m23;
            M30 = m30; M31 = m31; M32 = m32; M33 = m33;
        }

        public static Matrix4 Identity
        {
            get
            {
                return new Matrix4(1, 0, 0, 0,
                                   0, 1, 0, 0,
                                   0, 0, 1, 0,
                                   0, 0, 0, 1);
            }
        }

        /// <summary>
        /// Rotate the Matrix on the x Axis
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static Matrix4 CreateRotationX(float radians)
        {
            return new Matrix4(1, 0, 0, 0,
                               0, (float)Math.Cos(radians), (float)-Math.Sin(radians), 0,
                               0, (float)Math.Sin(radians), (float)Math.Cos(radians), 0,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Rotate the Matrix on the Y Axis
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static Matrix4 CreateRotationY(float radians)
        {
            return new Matrix4((float)Math.Cos(radians), 0, (float)Math.Sin(radians), 0,
                               0, 0, 0, 0,
                               (float)-Math.Sin(radians), 0, (float)Math.Cos(radians), 0,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Rotate the Matrix on the Z Axis
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static Matrix4 CreateRotationZ(float radians)
        {
            return new Matrix4((float)Math.Cos(radians), (float)-Math.Sin(radians), 0, 0,
                               (float)Math.Sin(radians), (float)Math.Cos(radians), 0, 0,
                               0, 0, 0, 0,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a new matrix that has been translated by the given value
        /// </summary>
        /// <param name="x">The x position of the new matrix</param>
        /// <param name="y">The y position of the new matrix</param>
        public static Matrix4 CreateTranslation(float x, float y, float z)
        {
            return new Matrix4(1, 0, 0, x,
                               0, 1, 0, y,
                               0, 0, 1, z,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a new matrix that has been translated by the given value
        /// </summary>
        /// <param name="x">The x position of the new matrix</param>
        /// <param name="y">The y position of the new matrix</param>
        public static Matrix4 CreateTranslation(Vector3 vector)
        {
            return new Matrix4(1, 0, 0, vector.x,
                               0, 1, 0, vector.y,
                               0, 0, 1, vector.z,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a new matrix that has been scaled by the given value
        /// </summary>
        /// <param name="x">The value to use to scale the matrix in the x axis</param>
        /// <param name="y">The value to use to scale the matrix in the y axis</param>
        /// <returns>The result of the scale</returns>
        public static Matrix4 CreateScale(float x, float y, float z)
        {
            return new Matrix4(x, 0, 0, 0,
                               0, y, 0, 0,
                               0, 0, z, 0,
                               0, 0, 0, 1);
        }

        /// <summary>
        /// Creates a new matrix that has been scaled by the given value
        /// </summary>
        /// <param name="x">The value to use to scale the matrix in the x axis</param>
        /// <param name="y">The value to use to scale the matrix in the y axis</param>
        /// <returns>The result of the scale</returns>
        public static Matrix4 CreateScale(Vector3 vector)
        {
            return new Matrix4(vector.x, 0, 0, 0,
                               0, vector.y, 0, 0,
                               0, 0, vector.z, 0,
                               0, 0, 0, 1);
        }

        public static Matrix4 operator +(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4(lhs.M00 + rhs.M00, lhs.M01 + rhs.M01, lhs.M02 + rhs.M02, lhs.M03 + rhs.M03,
                               lhs.M10 + rhs.M10, lhs.M11 + rhs.M11, lhs.M12 + rhs.M12, lhs.M13 + rhs.M13,
                               lhs.M20 + rhs.M20, lhs.M21 + rhs.M21, lhs.M22 + rhs.M22, lhs.M23 + rhs.M23,
                               lhs.M30 + rhs.M30, lhs.M31 + rhs.M31, lhs.M32 + rhs.M32, lhs.M33 + rhs.M33);
        }

        public static Matrix4 operator -(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4(lhs.M00 - rhs.M00, lhs.M01 - rhs.M01, lhs.M02 - rhs.M02, lhs.M03 - rhs.M03,
                               lhs.M10 - rhs.M10, lhs.M11 - rhs.M11, lhs.M12 - rhs.M12, lhs.M13 - rhs.M13,
                               lhs.M20 - rhs.M20, lhs.M21 - rhs.M21, lhs.M22 - rhs.M22, lhs.M23 - rhs.M23,
                               lhs.M30 - rhs.M30, lhs.M31 - rhs.M31, lhs.M32 - rhs.M32, lhs.M33 - rhs.M33);
        }

        public static Matrix4 operator *(Matrix4 lhs, Matrix4 rhs)
        {
            return new Matrix4
                (
                    //Row 1, Column 1
                    lhs.M00 * rhs.M00 + lhs.M01 * rhs.M10 + lhs.M02 * rhs.M20 + lhs.M03 * rhs.M30,
                    //Row 1, Column 2
                    lhs.M00 * rhs.M01 + lhs.M01 * rhs.M11 + lhs.M02 * rhs.M21 + lhs.M03 * rhs.M31,
                    //Row 1, Column 3
                    lhs.M00 * rhs.M02 + lhs.M01 * rhs.M12 + lhs.M02 * rhs.M22 + lhs.M03 * rhs.M32,
                    //Row 1, Column 4
                    lhs.M00 * rhs.M03 + lhs.M01 * rhs.M13 + lhs.M02 * rhs.M23 + lhs.M03 * rhs.M33,
                    //Row 2, Column 1
                    lhs.M10 * rhs.M00 + lhs.M11 * rhs.M10 + lhs.M12 * rhs.M20 + lhs.M13 * rhs.M30,
                    //Row 2, Column 2
                    lhs.M10 * rhs.M01 + lhs.M11 * rhs.M11 + lhs.M12 * rhs.M21 + lhs.M13 * rhs.M31,
                    //Row 2, Column 3
                    lhs.M10 * rhs.M02 + lhs.M11 * rhs.M12 + lhs.M12 * rhs.M22 + lhs.M13 * rhs.M32,
                    //Row 2, Column 4
                    lhs.M10 * rhs.M03 + lhs.M11 * rhs.M13 + lhs.M12 * rhs.M23 + lhs.M13 * rhs.M33,
                    //Row 3, Column 1
                    lhs.M20 * rhs.M00 + lhs.M21 * rhs.M10 + lhs.M22 * rhs.M20 + lhs.M23 * rhs.M30,
                    //Row 3, Column 2
                    lhs.M20 * rhs.M01 + lhs.M21 * rhs.M11 + lhs.M22 * rhs.M21 + lhs.M23 * rhs.M31,
                    //Row 3, Column 3
                    lhs.M20 * rhs.M02 + lhs.M21 * rhs.M12 + lhs.M22 * rhs.M22 + lhs.M23 * rhs.M32,
                    //Row 3, Column 4
                    lhs.M20 * rhs.M03 + lhs.M21 * rhs.M13 + lhs.M22 * rhs.M23 + lhs.M23 * rhs.M33,
                    //Row 4, Column 1
                    lhs.M30 * rhs.M00 + lhs.M31 * rhs.M10 + lhs.M32 * rhs.M20 + lhs.M33 * rhs.M30,
                    //Row 4, Column 2
                    lhs.M30 * rhs.M01 + lhs.M31 * rhs.M11 + lhs.M32 * rhs.M21 + lhs.M33 * rhs.M31,
                    //Row 4, Column 3
                    lhs.M30 * rhs.M02 + lhs.M31 * rhs.M12 + lhs.M32 * rhs.M22 + lhs.M33 * rhs.M32,
                    //Row 4, Column 4
                    lhs.M30 * rhs.M03 + lhs.M31 * rhs.M13 + lhs.M32 * rhs.M23 + lhs.M33 * rhs.M33
                );
        }

        public static Vector4 operator *(Matrix4 lhs, Vector4 rhs)
        {
            return new Vector4
                (
                    lhs.M00 * rhs.x + lhs.M01 * rhs.x + lhs.M02 * rhs.x + lhs.M03 * rhs.x,
                    lhs.M10 * rhs.y + lhs.M11 * rhs.y + lhs.M12 * rhs.y + lhs.M13 * rhs.y,
                    lhs.M20 * rhs.z + lhs.M21 * rhs.z + lhs.M22 * rhs.z + lhs.M23 * rhs.z,
                    lhs.M30 * rhs.w + lhs.M31 * rhs.w + lhs.M32 * rhs.w + lhs.M33 * rhs.w
                );
        }
    }
}


