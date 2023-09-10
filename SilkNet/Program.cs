﻿using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using System.Numerics;

namespace SilkNet
{
    internal class Program
    {
        private static uint _program;
        private static IWindow _window;
        private static GL _gl;

        private static uint _vao;
        private static uint _vbo;
        private static uint _ebo;

        static void Main(string[] args)
        {
            WindowOptions options = WindowOptions.Default with
            {
                Size = new Vector2D<int>(800, 600),
                Title = "LEarning"
            };

            _window = Window.Create(options);
            _window.Load += Load;
            _window.Update += Update;
            _window.Render += Render;
            _window.Run();
        }

        private static unsafe void Load()
        {
            #region GLSL

            const string vertexCode = @"
#version 330 core

layout (location = 0) in vec3 aPosition;

void main()
{
    gl_Position = vec4(aPosition, 1.0);
}
";

            const string fragmentCode = @"
#version 330 core

out vec4 out_color;

void main()
{
    out_color = vec4(1.0, 0.5, 0.2, 1.0);
}
";

            #endregion

            #region Setup
            _gl = _window.CreateOpenGL();
            IInputContext input = _window.CreateInput();

            // apply keypressed even to all connected keyboards
            for (int i = 0; i < input.Keyboards.Count; i++)
            {
                input.Keyboards[i].KeyDown += KeyDown;
            }

            // set color of cleared window
            _gl.ClearColor(Color.CornflowerBlue);

            // Generate then bind the "Vertex Array Object"
            _vao = _gl.GenVertexArray();
            _gl.BindVertexArray(_vao);

            // Generate then bind "Vertex Buffer Object"
            _vbo = _gl.GenBuffer();
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

            // Generate then bind "Element Buffer Object"
            _ebo = _gl.GenBuffer();
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);

            #region shaders

            _program = _gl.CreateProgram();

            // Create shader object of type "vertex shader"
            uint vertexShader = _gl.CreateShader(ShaderType.VertexShader);
            _gl.ShaderSource(vertexShader, vertexCode);

            // compile vertex shader we just made
            _gl.CompileShader(vertexShader);

            // check to make sure shader compiled correctly
            _gl.GetShader(vertexShader, ShaderParameterName.CompileStatus, out int vStatus);
            if (vStatus != (int)GLEnum.True)
                throw new Exception("Vertex shader failed to compile: " + _gl.GetShaderInfoLog(vertexShader));

            // Create shader object of type "fragment shader"
            uint fragmentShader = _gl.CreateShader(ShaderType.FragmentShader);
            _gl.ShaderSource(fragmentShader, fragmentCode);

            // compile fragment shader we just made
            _gl.CompileShader(fragmentShader);

            // check to make sure shader compiled correctly
            _gl.GetShader(fragmentShader, ShaderParameterName.CompileStatus, out int fStatus);
            if (fStatus != (int)GLEnum.True)
                throw new Exception("Fragment shader failed to compile: " + _gl.GetShaderInfoLog(fragmentShader));

            _gl.AttachShader(_program, vertexShader);
            _gl.AttachShader(_program, fragmentShader);

            _gl.LinkProgram(_program);

            _gl.GetProgram(_program, ProgramPropertyARB.LinkStatus, out int lStatus);
            if (lStatus != (int)GLEnum.True)
                throw new Exception("Program failed to link: " + _gl.GetProgramInfoLog(_program));

            _gl.DetachShader(_program, vertexShader);
            _gl.DetachShader(_program, fragmentShader);
            _gl.DeleteShader(vertexShader);
            _gl.DeleteShader(fragmentShader);

            const uint positionLoc = 0;
            _gl.EnableVertexAttribArray(positionLoc);
            _gl.VertexAttribPointer(positionLoc, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), (void*)0);

            // unbinding buffers, MUST UNBIND VERTEX FIRST
            _gl.BindVertexArray(0);
            _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
            _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
            #endregion

            #endregion

            ShapeManager.Instance.AddTriangleShape(new Shape_Rectangle(1, 1, Vector2.Zero));

            List<float> _vertices = new();
            List<uint> _indices = new();

            ShapeManager.Instance.GetCombinedMesh(_vertices, _indices);

            float[] vertices = _vertices.ToArray();
            uint[] indices = _indices.ToArray();

            fixed (float* buf = vertices)
            {
                _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Length * sizeof(float)), buf, BufferUsageARB.StaticDraw);
            }

        }

        private static void Update(double deltaTime)
        {
        }

        private static unsafe void Render(double deltaTime)
        {
            _gl.Clear(ClearBufferMask.ColorBufferBit);

            _gl.BindVertexArray(_vao);
            _gl.UseProgram(_program);
            _gl.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, (void*)0);
        }

        private static void KeyDown(IKeyboard keyboard, Key key, int keyCode)
        {
            // exit application on Escape Pressed
            if (key == Key.Escape)
                _window.Close();
        }
    }
}