using UnityEngine;

namespace PMC.Meshy
{
    public class Edge
    {

        public class TKEdgeDisk
        {
            public Edge Prev { get; set; }
            public Edge Next { get; set; }
        }

        public int Group { get; set; }
        public int SupportType { get; set; }

        public Loop Loop { get; set; }

        public Vert V1 { get; set; }

        public Vert V2 { get; set; }
        public TKEdgeDisk DiskV1Link { get; } = new TKEdgeDisk();
        public TKEdgeDisk DiskV2Link { get; } = new TKEdgeDisk();

        Edge()
        {

        }

        public Edge(Vert v1, Vert v2)
        {
            V1 = v1;
            V2 = v2;
            AttachToDiskEdge(v1);
            AttachToDiskEdge(v2);
        }

        public static Edge EdgeExists(Vert v1, Vert v2)
        {
            Edge e_a, e_b;
            if ((e_a = v1.Edge) != null && (e_b = v2.Edge) != null)
            {
                Edge e_a_iter = e_a, e_b_iter = e_b;
                do
                {
                    if (e_a_iter.IsVertInEdge(v2))
                    {
                        return e_a_iter;
                    }

                    if (e_b_iter.IsVertInEdge(v1))
                    {
                        return e_b_iter;
                    }

                } while ((e_a_iter = e_a_iter.NextEdge(v1)) != e_a && (e_b_iter = e_b_iter.NextEdge(v2)) != e_b);
            }

            return null;
        }

        public bool IsVertInEdge(Vert v1)
        {
            return V1 == v1 || V2 == v1;
        }

        public bool IsVertInEdge(Vert v1, Vert v2)
        {
            return (V1 == v1 && V2 == v2) ||
                   (V1 == v2 && V2 == v1);
        }

        public bool IsEdgeLoop()
        {
            return Loop != null && Loop.RadialNext == Loop;
        }

        public static Edge FindEdge(Vert v1, Vert v2)
        {
            Edge first, iter;
            if (v1.Edge != null)
            {
                first = iter = v1.Edge;
                do
                {
                    if (iter.IsVertInEdge(v1, v2))
                    {
                        return iter;
                    }
                } while ((iter = iter.NextEdge(v1)) != first);
            }

            return null;
        }

        public Edge NextEdge(Vert v)
        {
            return GetDiskLinkFromVert(v).Next;
        }

        public Edge PrevEdge(Vert v)
        {
            return GetDiskLinkFromVert(v).Next;
        }

        public  void AttachToDiskEdge(Vert v)
        {
            if (v.Edge == null)
            {
                TKEdgeDisk dl1 = GetDiskLinkFromVert(v);
                v.Edge = this;
                dl1.Next = dl1.Prev = this;
            }
            else
            {
                TKEdgeDisk dl1 = GetDiskLinkFromVert(v);
                TKEdgeDisk dl2 = v.Edge.GetDiskLinkFromVert(v);
                TKEdgeDisk dl3 = dl2.Prev?.GetDiskLinkFromVert(v);

                dl1.Next = v.Edge;
                dl1.Prev = dl2.Prev;

                dl2.Prev = this;
                if (dl3 != null)
                {
                    dl3.Next = this;
                }
            }
        }

        public void DetachDiskEdge(Vert v)
        {
            TKEdgeDisk dl1, dl2;
            dl1 = GetDiskLinkFromVert(v);
            if (dl1.Prev != null)
            {
                dl2 = dl1.Prev.GetDiskLinkFromVert(v);
                dl2.Next = dl1.Next;
            }

            if (dl1.Next != null)
            {
                dl2 = dl1.Next.GetDiskLinkFromVert(v);
                dl2.Prev = dl1.Prev;
            }

            if (v.Edge == this)
            {
                v.Edge = (this != dl1.Next) ? dl1.Next : null;
            }

            dl1.Next = dl1.Prev = null;
        }

        public void DetachDiskEdges()
        {
            DetachDiskEdge(V1);
            DetachDiskEdge(V2);
        }


        protected bool SwapDiskVert(Vert dest, Vert src)
        {
            if (V1 == src)
            {
                V1 = dest;
                DiskV1Link.Next = DiskV1Link.Prev = null;
                return true;
            }

            if (V2 == src)
            {
                V2 = dest;
                DiskV2Link.Next = DiskV2Link.Prev = null;
                return true;
            }

            return false;
        }


        protected void swapVert(Vert dest, Vert src)
        {
            DetachDiskEdge(src);
            SwapDiskVert(dest, src);
            AttachToDiskEdge(dest);
        }

        protected void EdgeSwapVert(Vert dest, Vert src)
        {
            if (Loop != null)
            {
                Loop iter, first;
                iter = first = Loop;
                do
                {
                    if (iter.Vert == src)
                    {
                        iter.Vert = dest;
                    }
                    else if (iter.RadialNext.Vert == src)
                    {
                        iter.RadialNext.Vert = dest;
                    }
                    else
                    {
                        if (iter.RadialPrev.Vert != src)
                            Debug.Log("Previous vert is equal to src");
                    }

                } while ((iter = iter.RadialNext) != first);
            }

            swapVert(dest, src);
        }


        private TKEdgeDisk GetDiskLinkFromVert(Vert vert)
        {
            if (vert == V2)
            {
                return DiskV2Link;
            }

            return DiskV1Link;
        }

    }
}
