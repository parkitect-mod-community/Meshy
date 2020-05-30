using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PMC.Meshy
{
    public class TKVert
    {
        public Vector3 pos { get; set; }
        public TKEdge Edge { get; set; }

        public int EdgeCount()
        {
            int count = 0;
            if (Edge != null)
            {
                TKEdge first, iter;
                first = iter = Edge;
                do
                {
                    count++;
                } while ((iter = iter.NextEdge(this)) != first);
            }

            return count;
        }

        public static IEnumerable<TKEdge> IterateEdges(Direction direction, TKVert vert)
        {

            TKEdge first = vert.Edge;
            if (first == null)
                yield break;
            TKEdge iterator = first;
            switch (direction)
            {
                case Direction.FORWARD:
                    do
                    {
                        yield return iterator;
                    } while ((iterator = iterator.NextEdge(vert)) != first && iterator != null);
                    break;
                case Direction.BACKWARD:
                    do
                    {
                        yield return iterator;
                    } while ((iterator = iterator.PrevEdge(vert)) != first && iterator != null);

                    break;
            }
        }
    }
}
