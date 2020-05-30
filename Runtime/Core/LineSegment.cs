/*
 * The MIT License
 *
 * Copyright (c) 2017-2020 JOML
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using UnityEngine;

namespace PMC.Meshy
{
    public class LineSegment
    {
        /**
     * The x coordinate of the first point.
     */
        public float aX { get; set; }

        /**
     * The y coordinate of the first point.
     */
        public float aY { get; set; }

        /**
     * The z coordinate of the first point.
     */
        public float aZ { get; set; }

        /**
     * The x coordinate of the second point.
     */
        public float bX { get; set; }

        /**
     * The y coordinate of the second point.
     */
        public float bY { get; set; }

        /**
     * The z coordinate of the second point.
     */
        public float bZ { get; set; }


        /**
     * Create a new {@link LineSegmentf} of zero length on the point <code>(0, 0, 0)</code>.
     */
        public LineSegment()
        {
        }

        /**
     * Create a new {@link LineSegmentf} as a copy of the given <code>source</code>.
     *
     * @param source
     *          the {@link LineSegmentf} to copy from
     */
        public LineSegment(LineSegment source)
        {
            this.aX = source.aX;
            this.aY = source.aY;
            this.aZ = source.aZ;
            this.aX = source.bX;
            this.bY = source.bY;
            this.bZ = source.bZ;
        }

        /**
     * Create a new {@link LineSegmentf} between the given two points.
     *
     * @param a
     *          the first point
     * @param b
     *          the second point
     */
        public LineSegment(Vector3 a, Vector3 b)
        {
            this.aX = a.x;
            this.aY = a.y;
            this.aZ = a.z;
            this.bX = b.x;
            this.bY = b.y;
            this.bZ = b.z;
        }

        /**
     * Create a new {@link LineSegmentf} between the two points.
     *
     * @param aX
     *          the x coordinate of the first point
     * @param aY
     *          the y coordinate of the first point
     * @param aZ
     *          the z coordinate of the first point
     * @param bX
     *          the x coordinate of the second point
     * @param bY
     *          the y coordinate of the second point
     * @param bZ
     *          the z coordinate of the second point
     */
        public LineSegment(float aX, float aY, float aZ, float bX, float bY, float bZ)
        {
            this.aX = aX;
            this.aY = aY;
            this.aZ = aZ;
            this.bX = bX;
            this.bY = bY;
            this.bZ = bZ;
        }

    }
}
