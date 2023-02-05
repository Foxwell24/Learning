using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace numeroUno
{
    internal class Program
    {
        private static IWindow window;
        private static GL gl;

        private static BufferObject<float> vertexBufferObject;
        private static BufferObject<uint> elementBufferObject;
        private static VertexArrayObject<float, uint> vertexArrayObject;
        private static Shader shader;

        private static readonly float[] Vertices =
        {
            //X    Y      Z     R  G  B  A
             0.5f,  0.5f, 0.0f, 1, 0, 0, 1,
             0.5f, -0.5f, 0.0f, 0, 0, 0, 1,
            -0.5f, -0.5f, 0.0f, 0, 0, 1, 1,
            -0.5f,  0.5f, 0.5f, 0, 0, 0, 1
        };
        private static readonly uint[] Indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private static void Main(string[] args)
        {
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            options.Title = "Title xD";
            window = Window.Create(options);

            window.Load += Window_Load;
            window.Render += Window_Render;
            window.Closing += Window_Closing;

            window.Run();
        }

        private static void Window_Load()
        {
            IInputContext input = window.CreateInput();
            for (int i = 0; i < input.Keyboards.Count; i++) input.Keyboards[i].KeyDown += KeyDown;

            gl = GL.GetApi(window);

            elementBufferObject = new BufferObject<uint>(gl, Indices, BufferTargetARB.ElementArrayBuffer);
            vertexBufferObject = new BufferObject<float>(gl, Vertices, BufferTargetARB.ArrayBuffer);
            vertexArrayObject = new VertexArrayObject<float, uint>(gl, vertexBufferObject, elementBufferObject);

            vertexArrayObject.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 7, 0);
            vertexArrayObject.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 7, 3);

            shader = new Shader(gl, "shader.vert", "shader.frag");
        }

        private static unsafe void Window_Render(double obj)
        {
            gl.Clear((uint)ClearBufferMask.ColorBufferBit);

            vertexArrayObject.Bind();
            shader.Use();

            shader.SetUniform("uBlue", (float)Math.Sin(DateTime.Now.Millisecond / 1000f * Math.PI));
            gl.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null);
        }

        private static void Window_Closing()
        {
            vertexBufferObject.Dispose();
            elementBufferObject.Dispose();
            vertexArrayObject.Dispose();
            shader.Dispose();
        }

        private static void KeyDown(IKeyboard arg1, Key arg2, int arg3)
        {
           if (arg2 == Key.Escape) window.Close();
        }
    }
}