using Silk.NET.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace numeroUno
{
    internal struct Square : IRender
    {
        public Vector2D<float> position { get; set; }
        public Vector4D<float> color { get; set; }

        public Vector2D<float> size;

        public Square(Vector2D<float> position, Vector2D<float> size, Vector4D<float> color)
        {
            this.position = position;
            this.size = size;
            this.color = color;
        }

        public float[] GetSquareVertices()
        {
            return new float[]
            {
                position.X + size.X / 2, position.Y + size.Y / 2, 0,
                color.X, color.Y, color.Z, color.W,
                position.X + size.X / 2, position.Y - size.Y / 2, 0,
                color.X, color.Y, color.Z, color.W,
                position.X - size.X / 2, position.Y - size.Y / 2, 0,
                color.X, color.Y, color.Z, color.W,
                position.X - size.X / 2, position.Y + size.Y / 2, 0,
                color.X, color.Y, color.Z, color.W
            };
        }

        public void Move(Vector2D<float> direction, float amount)
        {
            position += direction * amount;
        }
    }

    internal class SquareList
    {
        private List<Square> squares = new List<Square>();

        public (float[] Vertices, uint[] Indices) GetAllSquares()
        {
            float[] verts = new float[0];
            List<uint> uints = new List<uint>();
            for (uint i = 0, x = 0; i < squares.Count; i++, x+=4)
            {
                Square square = squares[(int)i];
                verts = verts.Concat(square.GetSquareVertices()).ToArray();

                uints.Add(x + 0);
                uints.Add(x + 1);
                uints.Add(x + 3);

                uints.Add(x + 1);
                uints.Add(x + 2);
                uints.Add(x + 3);
            }

            return (verts, uints.ToArray());
        }

        public void AddSquare(Square square) => squares.Add(square);
    }
}
