﻿using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace numeroUno
{
    public class VertexArrayObject<TVertexType, TIndexType> : IDisposable
        where TVertexType : unmanaged
        where TIndexType : unmanaged
    {
        private uint handle;
        private GL gl;

        public VertexArrayObject(GL gl, BufferObject<TVertexType> vbo, BufferObject<TIndexType> ebo)
        {
            //Saving the GL instance.
            this.gl = gl;

            //Setting out handle and binding the VBO and EBO to this VAO.
            handle = this.gl.GenVertexArray();
            Bind();
            vbo.Bind();
            ebo.Bind();
        }

        public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offSet)
        {
            //Setting up a vertex attribute pointer
            gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint)sizeof(TVertexType), (void*)(offSet * sizeof(TVertexType)));
            gl.EnableVertexAttribArray(index);
        }

        public void Bind()
        {
            //Binding the vertex array.
            gl.BindVertexArray(handle);
        }

        public void Dispose()
        {
            //Remember to dispose this object so the data GPU side is cleared.
            //We dont delete the VBO (VertexBufferObject) and EBO(ElementBufferObject) here, as you can have one VBO stored under multiple VAO's(VertextArrayObject).
            gl.DeleteVertexArray(handle);
        }
    }
}
