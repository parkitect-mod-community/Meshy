using System;
using System.Collections.Generic;
using UnityEngine;

namespace PMC.Meshy
{
    public class Meshy
    {
        public List<Vert> Verts { get; } = new List<Vert>();
        public List<Edge> Edges { get; } = new List<Edge>();
        public List<Face> Faces { get; } = new List<Face>();
        public List<Loop> Loops { get; } = new List<Loop>();

        public Meshy()
        {

        }

        public Meshy(Mesh mesh)
        {
            Vert[] tkVertex = new Vert[mesh.vertices.Length];
            for (var i = 0; i < mesh.vertices.Length; i++)
            {
                tkVertex[i] = new Vert
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


        public Face findFace(Vert[] verts)
        {
            foreach (var ed in Vert.IterateEdges(verts[0], Direction.FORWARD))
            {
                if (ed.Loop != null)
                {
                    foreach (var loopRadial in Loop.IterateRadialLoops(ed.Loop, Direction.FORWARD))
                    {
                        int i_walk = 2;
                        if (loopRadial.Next.Vert == verts[1] && loopRadial.Face.TotVerts == verts.Length)
                        {
                            var loopIter = Loop.IterateLoop(loopRadial.Next.Next, Direction.FORWARD);
                            foreach (var l in loopIter)
                            {
                                if (l.Vert != verts[i_walk])
                                {
                                    break;
                                }
                                i_walk++;
                            }
                        }
                        else if (loopRadial.Prev.Vert == verts[1])
                        {
                            var loopIter = Loop.IterateLoop(loopRadial.Next.Next, Direction.BACKWARD);
                            foreach (var l in loopIter)
                            {
                                if (l.Vert != verts[i_walk])
                                {
                                    break;
                                }

                                i_walk++;
                            }
                        }

                        if (i_walk == verts.Length)
                        {
                            return loopRadial.Face;
                        }
                    }
                }
            }

            return null;
        }


        public Face CreateFace(Vert[] verts, bool createEdges)
        {
            Edge[] edges = new Edge[verts.Length];
            if (createEdges)
            {
                fillEdgesFromVerts(edges, verts);
            }
            else
            {
                if (findEdges(edges, verts) == false)
                {
                    return null;
                }
            }

            return CreateFace(verts, edges, new Vector3());
        }

        public Face CreateFace(Vert[] verts, Edge[] edges, Vector3 normal)
        {
            if (verts.Length != edges.Length)
                throw new Exception("faces and edges needs to match");

            Loop startLoop, lastLoop, l;
            startLoop = lastLoop = new Loop(edges[0], verts[0]);
            Face face = new Face(new Vector3(), startLoop);
            for (int i = 1; i < verts.Length; i++)
            {
                l = new Loop(edges[i], verts[i], face);

                l.Prev = lastLoop;
                lastLoop.Next = l;
                lastLoop = l;
            }

            startLoop.Prev = lastLoop;
            lastLoop.Next = startLoop;

            face.TotVerts = verts.Length;
            return face;
        }

        public void KillEdge(Edge e)
        {
            while (e.Loop != null) {
                KillFace(e.Loop.Face);
            }
            e.DetachDiskEdges();
        }

        public void KillFace(Face face)
        {
            if (face.First != null)
            {
                foreach (var fc in Loop.IterateLoop(face.First, Direction.FORWARD))
                {
                    fc.DetachRadialLoopEdge(fc.Edge);
                    face.TotVerts--;
                }
            }
        }


        /// <summary>
        /// Fill in edges if verts are not filled
        /// </summary>
        public void fillEdgesFromVerts(Edge[] edges, Vert[] verts)
        {
            int i_prev = verts.Length - 1;
            for (int i = 0; i < verts.Length; i++)
            {
                edges[i_prev] = CreateEdge(verts[i_prev], verts[i], false);
                i_prev = i;
            }
        }

        public bool findEdges(Edge[] edges, Vert[] verts)
        {
            int i, i_prev = verts.Length - 1;
            for (i = 0; i < verts.Length; i++)
            {
                edges[i_prev] = Edge.EdgeExists(verts[i], verts[i_prev]);
                if (edges[i_prev] == null)
                {
                    return false;
                }

                i_prev = i;
            }

            return true;
        }


        public bool FindEdges(Vert[] verts, Edge[] edges) {
            int i = 0;
            int last = verts.Length - 1;
            for (i = 0; i < verts.Length; i++) {
                edges[last] = Edge.FindEdge(verts[i], verts[last]);
                if (edges[last] == null) {
                    return false;
                }
                last = i;
            }
            return true;
        }

        public Edge CreateEdge(Vert v1, Vert v2, bool allowDouble)
        {
            Edge e;
            if (!allowDouble && (e = Edge.EdgeExists(v1, v2)) != null)
            {
                return e;
            }

            e = new Edge(v1, v2);
            Edges.Add(e);
            return e;
        }


    }
}
