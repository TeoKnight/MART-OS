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
    internal class Vector3
    {
        public double x, y, z, w;

        public Vector3(float x, float y, float z, float w = 1)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector3(double x, double y, double z, double w = 1)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector3(int x, int y, int z, int w = 1)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Vector3 Add(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }

        public float GetLength()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        public Vector3 Normalize()
        {
            float length = GetLength();

            x /= length;
            y /= length;
            z /= length;

            return this;
        }

        public Vector3 MultiplyByScalar(float scalar)
        {
            x *= scalar;
            y *= scalar;
            z *= scalar;

            return this;
        }
    }
}
