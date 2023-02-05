using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace numeroUno
{
    //Our buffer object abstraction.
    public class BufferObject<TDataType> : IDisposable
        where TDataType : unmanaged
    {
        private uint handle;
        private BufferTargetARB bufferType;
        private GL gl;

        public unsafe BufferObject(GL gl, Span<TDataType> data, BufferTargetARB bufferType)
        {
            //Setting the gl instance and storing our buffer type.
            this.gl = gl;
            this.bufferType = bufferType;

            //Getting the handle, and then uploading the data to said handle.
            handle = this.gl.GenBuffer();
            Bind();
            fixed (void* d = data) // fixed stops garbage collector from reallocating this memory, makes method unsafe
            {
                gl.BufferData(bufferType, (nuint)(data.Length * sizeof(TDataType)), d, BufferUsageARB.StaticDraw);
            }
        }

        public void Bind()
        {
            //Binding the buffer object, with the correct buffer type.
            gl.BindBuffer(bufferType, handle);
        }

        public void Dispose()
        {
            //Remember to delete our buffer.
            gl.DeleteBuffer(handle);
        }
    }
}
