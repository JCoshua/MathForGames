
using System;

namespace MathLibrary
{
    public struct Matrix3
    {
        public float M00, M01, M02, M10, M11, M12, M20, M21, M22;

        public Matrix3(float m00, float m01, float m02,
                        float m10, float m11, float m12,
                        float m20, float m21, float m22)
        {
            M00 = m00; M01 = m01; M02 = m02;
            M10 = m10; M11 = m11; M12 = m12;
            M20 = m20; M21 = m21; M22 = m22;
        }

        public static Matrix3 Identity
        {
            get
            {
                return new Matrix3(1, 0, 0,
                                   0, 1, 0,
                                   0, 0, 1);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public Matrix3 CreateRotation(float radians)
        {

        }

        /// <summary>
        /// Translates the Matrix by the values given
        /// </summary>
        /// <param name="x">The translation of the x axis</param>
        /// <param name="y">The translation of the y axis</param>
        /// <returns></returns>
        public Matrix3 CreateTranslation(float x, float y)
        {
            M02 += x;
            M12 += y;
            return this;
        }

        /// <summary>
        /// Creates a new Matrix that is scaled by the given value
        /// </summary>
        /// <param name="scale">The vector to use to scale the matrix</param>
        /// <returns>The</returns>
        public Matrix3 CreateScale(float x, float y)
        {
            M00 = x;
            M11 = y;
            return this;
        }

        public static Matrix3 operator +(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3(lhs.M00 + rhs.M00, lhs.M01 + rhs.M01, lhs.M02 + rhs.M02,
                               lhs.M10 + rhs.M10, lhs.M11 + rhs.M11, lhs.M12 + rhs.M12,
                               lhs.M20 + rhs.M20, lhs.M21 + rhs.M21, lhs.M22 + rhs.M22);
        }

        public static Matrix3 operator -(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3(lhs.M00 - rhs.M00, lhs.M01 - rhs.M01, lhs.M02 - rhs.M02,
                               lhs.M10 - rhs.M10, lhs.M11 - rhs.M11, lhs.M12 - rhs.M12,
                               lhs.M20 - rhs.M20, lhs.M21 - rhs.M21, lhs.M22 - rhs.M22);
        }

        public static Matrix3 operator *(Matrix3 lhs, Matrix3 rhs)
        {
            return new Matrix3(lhs.M00 * rhs.M00 + lhs.M01 * rhs.M10 + lhs.M02 + rhs.M20, lhs.M00 * rhs.M01 + lhs.M01 * rhs.M11 + lhs.M02 + rhs.M21, lhs.M00 * rhs.M02 + lhs.M01 * rhs.M12 + lhs.M02 + rhs.M22,
                               lhs.M10 * rhs.M00 + lhs.M11 * rhs.M10 + lhs.M12 + rhs.M20, lhs.M10 * rhs.M01 + lhs.M11 * rhs.M11 + lhs.M12 + rhs.M21, lhs.M10 * rhs.M02 + lhs.M11 * rhs.M12 + lhs.M12 + rhs.M22,
                               lhs.M20 * rhs.M00 + lhs.M21 * rhs.M10 + lhs.M22 + rhs.M20, lhs.M20 * rhs.M01 + lhs.M21 * rhs.M11 + lhs.M22 + rhs.M21, lhs.M20 * rhs.M02 + lhs.M21 * rhs.M12 + lhs.M22 + rhs.M22);
        }
    }
}


