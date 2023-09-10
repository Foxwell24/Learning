using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilkNet
{
    internal class ShapeManager
    {
        public static ShapeManager Instance = new ShapeManager();

        private List<ITriangleShape> _shapes = new();

        public uint NumberOfElementsForEBO => (uint)_shapes.Count * 6u;

        public void AddTriangleShape(ITriangleShape shape) => _shapes.Add(shape);

        public void GetCombinedMesh(List<float> vertices, List<uint> indices)
        {
            foreach (var shape in _shapes)
            {
                shape.CombineMesh(vertices, indices);
            }
        }
    }
}