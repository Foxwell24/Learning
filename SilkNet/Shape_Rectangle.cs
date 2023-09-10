using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SilkNet
{
    internal class Shape_Rectangle : ITriangleShape
    {
        private float[] verts;
        private uint[] ints;
        private Vector2 position;

        public Shape_Rectangle(float width, float height, Vector2 position)
        {
            this.position = position;

            verts = new float[]
            {
                (0.5f * width) + position.X,  (0.5f * height) + position.Y,  0.0f,
                (0.5f * width) + position.X,  (-0.5f * height) + position.Y, 0.0f,
                (-0.5f * width) + position.X, (-0.5f * height) + position.Y, 0.0f,
                (-0.5f * width) + position.X, (0.5f * height) + position.Y,  0.0f
            };

            ints = new uint[]
            {
                0u, 1u, 3u,
                1u, 2u, 3u
            };
        }

        public void CombineMesh(List<float> vertices, List<uint> indices)
        {
            uint v1 = (uint)vertices.Count / 3;

            vertices.AddRange(verts);
            foreach (var v in ints) indices.Add(v + v1);
        }
    }
}
