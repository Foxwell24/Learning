using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace numeroUno
{
    internal class Shader
    {
        private uint handle;
        private GL gl;

        public Shader(GL gl, string vertexPath, string fragmentPath)
        {
            this.gl = gl;

            // load the individual shaders.
            uint vertex = LoadShader(ShaderType.VertexShader, vertexPath);
            uint fragment = LoadShader(ShaderType.FragmentShader, fragmentPath);

            // create shader program
            handle = gl.CreateProgram();

            // attach individual haders
            gl.AttachShader(handle, vertex);
            gl.AttachShader(handle, fragment);
            gl.LinkProgram(handle);

            // check for errors in link
            gl.GetProgram(handle, GLEnum.LinkStatus, out var status);
            if (status == 0)
            {
                throw new Exception($"Program failed to link with error: {gl.GetProgramInfoLog(handle)}");
            }

            // detatch and delete the shaders
            gl.DetachShader(handle, vertex);
            gl.DetachShader(handle, fragment);
            gl.DeleteShader(vertex);
            gl.DeleteShader(fragment);
        }

        public void Use()
        {
            gl.UseProgram(handle);
        }

        /// <summary>
        /// Uniforms are properties that applies to the entire geometry
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetUniform(string name, int value)
        {
            // setting uniform on a shader using a name
            int location = gl.GetUniformLocation(handle, name);

            if (location == -1) // uniform is not found
                throw new Exception($"{name} uniform not found on shader.");

            gl.Uniform1(location, value);
        }

        /// <summary>
        /// Uniforms are properties that applies to the entire geometry
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetUniform(string name, float value)
        {
            // setting uniform on a shader using a name
            int location = gl.GetUniformLocation(handle, name);

            if (location == -1) // uniform is not found
                throw new Exception($"{name} uniform not found on shader.");

            gl.Uniform1(location, value);
        }

        private uint LoadShader(ShaderType type, string path)
        {
            // load shader from file
            string src = File.ReadAllText(path);
            uint handle = gl.CreateShader(type);

            // give openGL the shader
            gl.ShaderSource(handle, src);

            // compile shader
            gl.CompileShader(handle);

            // check for errors
            string infoLog = gl.GetShaderInfoLog(handle);
            if (infoLog.Length > 0)
            {
                throw new Exception($"Error compiling shader of type {type}, failed with error {infoLog}");
            }

            // return shader handle
            return handle;
        }

        public void Dispose()
        {
            //Remember to delete the program when we are done.
            gl.DeleteProgram(handle);
        }
    }
}
