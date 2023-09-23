using System.Numerics;

namespace SilkNet.Shapes
{
    internal interface ITriangleShape
    {
        void CombineMesh(List<float> vertices, List<uint> indices);
        void ChangePosition(Vector2 position);
    }
}