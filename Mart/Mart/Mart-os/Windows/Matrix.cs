using Cosmos.System;
using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Mart.Controls;

namespace Mart.Windows
{
    internal class Matrix
    {
		public static double[][] Multiply(double[][] a, double[][] b)
		{
			double[][] result = new double[4][];

			for (int i = 0; i < 4; i++)
			{
				result[i] = new double[4];
				for (int j = 0; j < 4; j++)
				{
					result[i][j] = a[i][0] * b[0][j] + a[i][1] * b[1][j] + a[i][2] * b[2][j] + a[i][3] * b[3][j];
				}
			}

			return result;
		}

		public static float[,] Multiply(float[,] a, float[,] b)
        {
            float[,] m = new float[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m[i, j] = a[i, 0] * b[0, j] + a[i, 1] * b[1, j] + a[i, 2] * b[2, j] + a[i, 3] * b[3, j];
                }
            }

            return m;
        }

        public static double[,] Multiply(double[,] a, double[,] b)
        {
            double[,] m = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m[i, j] = a[i, 0] * b[0, j] + a[i, 1] * b[1, j] + a[i, 2] * b[2, j] + a[i, 3] * b[3, j];
                }
            }

            return m;
        }

        public static double[,] Multiply(int[,] a, double[,] b)
        {
            double[,] m = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    m[i, j] = a[i, 0] * b[0, j] + a[i, 1] * b[1, j] + a[i, 2] * b[2, j] + a[i, 3] * b[3, j];
                }
            }

            return m;
        }

		public static Vector3 MultiplyVector(double[][] m, Vector3 v)
		{
			return new Vector3
				(
					m[0][0] * v.x + m[0][1] * v.y + m[0][2] * v.z + m[0][3] * v.w,
					m[1][0] * v.x + m[1][1] * v.y + m[1][2] * v.z + m[1][3] * v.w,
					m[2][0] * v.x + m[2][1] * v.y + m[2][2] * v.z + m[2][3] * v.w,
					m[3][0] * v.x + m[3][1] * v.y + m[3][2] * v.z + m[3][3] * v.w
				);
		}

		public static Vector3 MultiplyVector(float[,] m, Vector3 v)
        {
            return new Vector3
                (
                    m[0, 0] * v.x + m[0, 1] * v.y + m[0, 2] * v.z + m[0, 3] * v.w,
                    m[1, 0] * v.x + m[1, 1] * v.y + m[1, 2] * v.z + m[1, 3] * v.w,
                    m[2, 0] * v.x + m[2, 1] * v.y + m[2, 2] * v.z + m[2, 3] * v.w,
                    m[3, 0] * v.x + m[3, 1] * v.y + m[3, 2] * v.z + m[3, 3] * v.w
                );
        }

        public static Vector3 MultiplyVector(double[,] m, Vector3 v)
        {
            return new Vector3
                (
                    m[0, 0] * v.x + m[0, 1] * v.y + m[0, 2] * v.z + m[0, 3] * v.w,
                    m[1, 0] * v.x + m[1, 1] * v.y + m[1, 2] * v.z + m[1, 3] * v.w,
                    m[2, 0] * v.x + m[2, 1] * v.y + m[2, 2] * v.z + m[2, 3] * v.w,
                    m[3, 0] * v.x + m[3, 1] * v.y + m[3, 2] * v.z + m[3, 3] * v.w
                );
        }

		public static double[][] GetTranslationMatrix(int dx, int dy, int dz)
		{
			double[][] matrix = new double[4][];
			matrix[0] = new double[] { 1, 0, 0, dx };
			matrix[1] = new double[] { 0, 1, 0, dy };
			matrix[2] = new double[] { 0, 0, 1, dz };
			matrix[3] = new double[] { 0, 0, 0, 1 };

			return matrix;
		}
		public static double[][] GetScale(int sx, int sy, int sz)
		{
			double[][] matrix = new double[4][];
			matrix[0] = new double[] { sx, 0, 0, 0 };
			matrix[1] = new double[] { 0, sy, 0, 0 };
			matrix[2] = new double[] { 0, 0, sz, 0 };
			matrix[3] = new double[] { 0, 0, 0, 1 };

			return matrix;
		}
		public static double[][] GetRotationX(int angle)
		{
			double rad = Math.PI / 180 * angle;

			double[][] matrix = new double[4][];
			matrix[0] = new double[] { 1, 0, 0, 0 };
			matrix[1] = new double[] { 0, Math.Cos(rad), -Math.Sin(rad), 0 };
			matrix[2] = new double[] { 0, Math.Sin(rad), Math.Cos(rad), 0 };
			matrix[3] = new double[] { 0, 0, 0, 1 };

			return matrix;
		}
		public static double[][] GetRotationY(int angle)
		{
			double rad = Math.PI / 180 * angle;

			double[][] matrix = new double[4][];
			matrix[0] = new double[] { Math.Cos(rad), 0, Math.Sin(rad), 0 };
			matrix[1] = new double[] { 0, 1, 0, 0 };
			matrix[2] = new double[] { -Math.Sin(rad), 0, Math.Cos(rad), 0 };
			matrix[3] = new double[] { 0, 0, 0, 1 };

			return matrix;
		}
		public static double[][] GetRotationZ(int angle)
		{
			double rad = Math.PI / 180 * angle;

			double[][] matrix = new double[4][];
			matrix[0] = new double[] { Math.Cos(rad), -Math.Sin(rad), 0, 0 };
			matrix[1] = new double[] { Math.Sin(rad), Math.Cos(rad), 0, 0 };
			matrix[2] = new double[] { 0, 0, 1, 0 };
			matrix[3] = new double[] { 0, 0, 0, 1 };

			return matrix;
		}
	}
}
