using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SilkNet.Shapes
{
    internal class Shape_Triangle : ITriangleShape
    {
        private float[] verts;
        private uint[] ints;
        private Vector2 position;

        public Shape_Triangle(Vector2 corner1, Vector2 corner2, Vector2 corner3, Vector2 position)
        {
            this.position = position;

            corner1 += position;
            corner2 += position;
            corner3 += position;

            verts = new float[]
            {
                corner1.X,  corner1.Y,  0.0f,
                corner2.X,  corner2.Y,  0.0f,
                corner3.X,  corner3.Y,  0.0f
            };

            ints = new uint[]
            {
                0u, 1u, 3u
            };
        }

        public void CombineMesh(List<float> vertices, List<uint> indices)
        {
            uint v1 = (uint)vertices.Count / 3;

            vertices.AddRange(verts);
            foreach (var v in ints) indices.Add(v + v1);
        }

        public void ChangePosition(Vector2 position)
        {
            this.position = position;

            verts = new float[]
            {
                verts[0] + position.X,  verts[1] + position.Y,  verts[2],
                verts[3] + position.X,  verts[4] + position.Y,  verts[5],
                verts[6] + position.X,  verts[7] + position.Y,  verts[8]
            };
        }
    }
}
