using System.Collections.Generic;
using UnityEngine;

namespace PMC.Meshy
{
    public class TKMesh
    {
        public List<TKVert> Verts { get; } = new List<TKVert>();
        public List<TKEdge> Edges { get; } = new List<TKEdge>();
        public List<TKFace> Faces { get; } = new List<TKFace>();
        public List<TKLoop> Loops { get; } = new List<TKLoop>();

        public TKMesh()
        {

        }

        public TKMesh(UnityEngine.Mesh mesh)
        {
            TKVert[] tkVertex = new TKVert[mesh.vertices.Length];
            for (var i = 0; i < mesh.vertices.Length; i++)
            {
                tkVertex[i] = new TKVert
                {
                    pos = new Vector3(mesh.vertices[i].x, mesh.vertices[i].y, mesh.vertices[i].z)
                };
            }

            for (int i = 0; i < mesh.triangles.Length - 3; i++)
            {
                int tr1 = mesh.triangles[i];
                int tr2 = mesh.triangles[i + 1];
                int tr3 = mesh.triangles[i + 2];
            }
        }

        private TKLoop _createInitialLoop(TKVert v, TKEdge e, out TKFace face)
        {
            face = new TKFace();
            TKLoop loop = _createLoop(v,e,face);
            Faces.Add(face);
            loop.AttachRadialLoopEdge(e);
            face.LoopFirst = loop;
            return loop;
        }

        private TKLoop _createLoop(TKVert v, TKEdge e, TKFace f)
        {
            TKLoop loop = new TKLoop(e,v,f);
            Loops.Add(loop);
            return loop;
        }


        public TKFace CreateFace(TKVert[] verts, TKEdge[] edges, Vector3 normal)
        {
            TKLoop startLoop, lastLoop;
            startLoop = lastLoop = _createInitialLoop(verts[0], edges[0], out var f);

            f.LoopFirst = startLoop;

            for (int i = 1; i < verts.Length; i++)
            {
                var l = _createLoop(verts[i], edges[i], f);

                l.AttachRadialLoopEdge(edges[i]);

                l.Prev = lastLoop;
                lastLoop.Next = l;
                lastLoop = l;
            }

            startLoop.Prev = lastLoop;
            lastLoop = startLoop;
            f.Length = verts.Length;
            return f;
        }

        public TKFace CreateFace(TKVert[] verts, bool createEdges)
        {
            TKEdge[] edges = new TKEdge[verts.Length];
            if (createEdges)
            {
                EdgesFromVertEnsure(edges, verts);
            }
            else
            {
                if (EdgesFromVerts(edges, verts) == false)
                {
                    return null;
                }
            }

            return CreateFace(verts, edges, new Vector3());
        }

        public bool EdgesFromVerts(TKEdge[] edges, TKVert[] verts)
        {
            int i, i_prev = verts.Length - 1;
            for (i = 0; i < verts.Length; i++)
            {
                edges[i_prev] = TKEdge.EdgeExists(verts[i], verts[i_prev]);
                if (edges[i_prev] == null)
                {
                    return false;
                }

                i_prev = i;
            }

            return true;
        }

        /// <summary>
        /// Fill in edges if verts are not filled
        /// </summary>
        public void EdgesFromVertEnsure(TKEdge[] edges, TKVert[] verts)
        {
            int i_prev = verts.Length - 1;
            for (int i = 0; i < verts.Length; i++)
            {
                edges[i_prev] = CreateEdge(verts[i_prev], verts[i], false);
                i_prev = i;
            }
        }

        public TKEdge CreateEdge(TKVert v1, TKVert v2, bool allowDouble)
        {
            TKEdge e;
            if (!allowDouble && (e = TKEdge.EdgeExists(v1, v2)) != null)
            {
                return e;
            }

            e = new TKEdge(v1, v2);
            Edges.Add(e);
            return e;
        }


        public TKVert CreateVert(Vector3 pos) {
            TKVert vert = new TKVert();
            Verts.Add(vert);
            return vert;
        }


    }
}
