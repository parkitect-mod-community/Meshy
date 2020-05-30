using System.Collections;
using System.Collections.Generic;

namespace PMC.Meshy
{
    public class TKLoop
    {
        public TKLoop(TKEdge e, TKVert v, TKFace f)
        {
            Edge = e;
            Vert = v;
            Face = f;
        }

        public TKEdge Edge { get; set; }
        public TKVert Vert { get; set; }
        public TKFace Face { get; set; }

        public TKLoop RadialNext { get; set; }
        public TKLoop RadialPrev { get; set; }

        public TKLoop Next { get; set; }
        public TKLoop Prev { get; set; }

        internal void AttachRadialLoopEdge(TKEdge edge)
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

        internal void DetachRadialLoopEdge(TKEdge edge)
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

        public static IEnumerable<TKLoop> IterateRadialLoops(TKLoop loop, Direction direction)
        {
            TKLoop first = loop;
            if (first == null)
                yield break;
            TKLoop iterator = first;
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

        public IEnumerable<TKLoop> IterateLoop(TKLoop loop, Direction direction)
        {
            TKLoop first = loop;
            if (first == null)
                yield break;
            TKLoop iterator = first;
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
