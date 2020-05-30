using System.Collections;
using System.Collections.Generic;

namespace PMC.Meshy
{
    public class Loop
    {
        public Loop(Edge e, Vert v, Face f)
        {
            Edge = e;
            Vert = v;
            Face = f;
            AttachRadialLoopEdge(e);
        }

        public Loop(Edge e, Vert v)
        {
            Edge = e;
            Vert = v;
            AttachRadialLoopEdge(e);
        }


        public Edge Edge { get; set; }
        public Vert Vert { get; set; }
        public Face Face { get; set; }

        public Loop RadialNext { get; set; }
        public Loop RadialPrev { get; set; }

        public Loop Next { get; set; }
        public Loop Prev { get; set; }

        internal void AttachRadialLoopEdge(Edge edge)
        {
            if (edge.Loop == null)
            {
                edge.Loop = this;
                RadialNext = RadialPrev = this;
            }
            else
            {
                RadialPrev = edge.Loop;
                RadialNext = edge.Loop.RadialNext;

                edge.Loop.RadialNext.RadialPrev = this;
                edge.Loop.RadialNext = this;

                edge.Loop = this;
            }

            Edge = edge;
        }

        internal void DetachRadialLoopEdge(Edge edge)
        {
            if (RadialNext != this)
            {
                if (this == edge.Loop)
                {
                    edge.Loop = RadialNext;
                }

                RadialNext.RadialPrev = RadialPrev;
                RadialPrev.RadialNext = RadialNext;
            }
            else
            {
                if (this == edge.Loop)
                {
                    edge.Loop = null;
                }
            }
        }

        public static IEnumerable<Loop> IterateRadialLoops(Loop loop, Direction direction)
        {
            Loop first = loop;
            if (first == null)
                yield break;
            Loop iterator = first;
            switch (direction)
            {
                case Direction.FORWARD:
                    do
                    {
                        yield return iterator;
                    } while ((iterator = iterator.RadialNext) != first && iterator != null);

                    break;
                case Direction.BACKWARD:
                    do
                    {
                        yield return iterator;
                    } while ((iterator = iterator.RadialPrev) != first && iterator != null);

                    break;
            }
        }

        public static IEnumerable<Loop> IterateLoop(Loop loop, Direction direction)
        {
            Loop first = loop;
            if (first == null)
                yield break;
            Loop iterator = first;
            switch (direction)
            {
                case Direction.FORWARD:
                    do
                    {
                        yield return iterator;
                    } while ((iterator = iterator.Next) != first && iterator != null);
                    break;
                case Direction.BACKWARD:
                    do
                    {
                        yield return iterator;
                    } while ((iterator = iterator.Prev) != first && iterator != null);

                    break;
            }
        }

    }
}
