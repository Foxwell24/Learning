namespace SilkNet
{
    internal interface ITriangleShape
    {
        void CombineMesh(List<float> vertices, List<uint> indices);
    }
}