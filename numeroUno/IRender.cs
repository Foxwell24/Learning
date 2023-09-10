using Silk.NET.Maths;

namespace numeroUno
{
    internal interface IRender
    {
        Vector2D<float> position { get; set; }
        Vector4D<float> color { get; set; }
    }
}