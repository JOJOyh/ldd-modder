﻿using LDDModder.BrickEditor.Rendering.Shaders;
using ObjectTK.Buffers;
using ObjectTK.Shaders.Variables;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDDModder.BrickEditor.Rendering
{
    public class IndexedVertexBuffer<T> : IDisposable, IVertexBuffer where T : struct
    {
        public VertexArray Vao { get; protected set; }

        public Buffer<int> IndexBuffer { get; protected set; }

        public int IndexCount => (IndexBuffer != null && IndexBuffer.Initialized) ? IndexBuffer.ElementCount : 0;

        public Buffer<T> VertexBuffer { get; protected set; }

        public int VertexCount => (VertexBuffer != null && VertexBuffer.Initialized) ? VertexBuffer.ElementCount : 0;

        public bool BufferInitialized { get; protected set; }

        public bool IsDisposed { get; protected set; }

        public void Bind()
        {
            if (BufferInitialized)
                Vao.Bind();
        }

        public void BindAttribute(VertexAttrib attribute, int offset)
        {
            if (BufferInitialized)
                Vao.BindAttribute(attribute, VertexBuffer, offset);
        }

        public void BindVertexBuffer()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer.Handle);
        }

        public void BindVertexPointer()
        {
            Bind();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer.Handle);
            GL.VertexPointer(3, VertexPointerType.Float, VertexBuffer.ElementSize, 0);
        }

        public void UnbindAttribute(VertexAttrib attribute)
        {
            if (BufferInitialized)
                Vao.UnbindAttribute(attribute);
            
        }

        public void CreateBuffers()
        {
            if (!BufferInitialized)
            {
                Vao = new VertexArray();
                IndexBuffer = new Buffer<int>();
                VertexBuffer = new Buffer<T>();
                BufferInitialized = true;

                Vao.Bind();
                Vao.BindElementBuffer(IndexBuffer);

            }
        }

        public void ClearBuffers()
        {
            if (BufferInitialized)
            {
                IndexBuffer.Clear(BufferTarget.ElementArrayBuffer);
                VertexBuffer.Clear(BufferTarget.ArrayBuffer);
            }
        }

        public void SetIndices(IEnumerable<int> indices)
        {
            if (!BufferInitialized)
                CreateBuffers();
            IndexBuffer.Clear(BufferTarget.ElementArrayBuffer);
            IndexBuffer.Init(BufferTarget.ElementArrayBuffer, indices.ToArray());
        }

        public void AppendIndices(IEnumerable<int> indices)
        {
            if (!BufferInitialized)
                CreateBuffers();
            var currentIndices = IndexBuffer.Content;
            IndexBuffer.Clear(BufferTarget.ElementArrayBuffer);
            IndexBuffer.Init(BufferTarget.ElementArrayBuffer, currentIndices.Concat(indices).ToArray());
        }

        public void SetVertices(IEnumerable<T> vertices)
        {
            if (!BufferInitialized)
                CreateBuffers();
            VertexBuffer.Clear(BufferTarget.ArrayBuffer);
            VertexBuffer.Init(BufferTarget.ArrayBuffer, vertices.ToArray());
        }

        public void AppendVertices(IEnumerable<T> vertices)
        {
            if (!BufferInitialized)
                CreateBuffers();
            var currentVerts = VertexBuffer.Content;
            VertexBuffer.Clear(BufferTarget.ArrayBuffer);
            VertexBuffer.Init(BufferTarget.ArrayBuffer, currentVerts.Concat(vertices).ToArray());
        }

        public void LoadModelVertices(Assimp.Mesh mesh, bool append = false)
        {
            var verts = new List<VertVNT>();
            bool isTextured = mesh.HasTextureCoords(0);

            for (int i = 0; i < mesh.VertexCount; i++)
            {
                verts.Add(new VertVNT()
                {
                    Position = mesh.Vertices[i].ToGL(),
                    Normal = mesh.Normals[i].ToGL(),
                    TexCoord = isTextured ? mesh.TextureCoordinateChannels[0][i].ToGL().Xy : Vector2.Zero
                });
            }

            if (append)
                AppendVertices(verts.Select(x => x.Cast<T>()));
            else
                SetVertices(verts.Select(x => x.Cast<T>()));

            var indices = new List<int>();
            int indexPerFace = mesh.Faces[0].IndexCount;
            foreach (var face in mesh.Faces)
                if (face.IndexCount == indexPerFace)
                    indices.AddRange(face.Indices);

            if (append)
                AppendIndices(indices);
            else
                SetIndices(indices);
        }

        #region Draw Methods

        public void DrawElementsBaseVertex(PrimitiveType mode, int baseVertex, int count, int offset = 0)
        {
            Vao.Bind();
            Vao.DrawElementsBaseVertex(mode, baseVertex, count, DrawElementsType.UnsignedInt, offset);
        }

        public void DrawElements(PrimitiveType drawMode = PrimitiveType.Triangles)
        {
            Vao.Bind();
            Vao.DrawElements(drawMode, IndexBuffer.ElementCount);
        }

        public void DrawElements(PrimitiveType drawMode, int count)
        {
            Vao.Bind();
            Vao.DrawElements(drawMode, count);
        }

        public void DrawArrays(PrimitiveType drawMode, int first, int count)
        {
            Vao.Bind();
            Vao.DrawArrays(drawMode, first, count);
        }

        #endregion

        ~IndexedVertexBuffer()
        {
            if (!IsDisposed)
                Dispose();
        }

        public void Dispose()
        {
            if (IndexBuffer != null)
            {
                IndexBuffer.Dispose();
                IndexBuffer = null;
            }

            if (VertexBuffer != null)
            {
                VertexBuffer.Dispose();
                VertexBuffer = null;
            }

            if (Vao != null)
            {
                Vao.Dispose();
                Vao = null;
            }

            IsDisposed = true;
        }
    }
}
