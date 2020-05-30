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
    public class Sphere
    {
        /**
     * The x coordinate of the sphere's center.
     */
        public float x { get; set; }

        /**
     * The y coordinate of the sphere's center.
     */
        public float y{ get; set; }

        /**
     * The z coordinate of the sphere's center.
     */
        public float z{ get; set; }

        /**
     * The sphere's radius.
     */
        public float r{ get; set; }

        /**
     * Create a new {@link Spheref} with center position <code>(0, 0, 0)</code> and radius = <code>0</code>.
     */
        public Sphere()
        {
        }

        /**
     * Create a new {@link Spheref} as a copy of the given <code>source</code>.
     *
     * @param source
     *          the {@link Spheref} to copy from
     */
        public Sphere(Sphere source)
        {
            this.x = source.x;
            this.y = source.y;
            this.z = source.z;
            this.r = source.r;
        }

        /**
     * Create a new {@link Spheref} with center position <code>c</code> and radius <code>r</code>.
     *
     * @param c
     *          the center position of the sphere
     * @param r
     *          the radius of the sphere
     */
        public Sphere(Vector3 c, float r)
        {
            this.x = c.x;
            this.y = c.y;
            this.z = c.z;
            this.r = r;
        }

        /**
     * Create a new {@link Spheref} with center position <code>(x, y, z)</code> and radius <code>r</code>.
     *
     * @param x
     *          the x coordinate of the sphere's center
     * @param y
     *          the y coordinate of the sphere's center
     * @param z
     *          the z coordinate of the sphere's center
     * @param r
     *          the radius of the sphere
     */
        public Sphere(float x, float y, float z, float r)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.r = r;
        }

        /**
     * Translate <code>this</code> by the given vector <code>xyz</code>.
     *
     * @param xyz
     *          the vector to translate by
     * @return this
     */
        public Sphere translate(Vector3 xyz)
        {
            return translate(xyz.x, xyz.y, xyz.z, this);
        }

        /**
     * Translate <code>this</code> by the given vector <code>xyz</code> and store the result in <code>dest</code>.
     *
     * @param xyz
     *          the vector to translate by
     * @param dest
     *          will hold the result
     * @return dest
     */
        public Sphere translate(Vector3 xyz, Sphere dest)
        {
            return translate(xyz.x, xyz.y, xyz.z, dest);
        }

        /**
     * Translate <code>this</code> by the vector <code>(x, y, z)</code>.
     *
     * @param x
     *          the x coordinate to translate by
     * @param y
     *          the y coordinate to translate by
     * @param z
     *          the z coordinate to translate by
     * @return this
     */
        public Sphere translate(float x, float y, float z)
        {
            return translate(x, y, z, this);
        }

        /**
     * Translate <code>this</code> by the vector <code>(x, y, z)</code> and store the result in <code>dest</code>.
     *
     * @param x
     *          the x coordinate to translate by
     * @param y
     *          the y coordinate to translate by
     * @param z
     *          the z coordinate to translate by
     * @param dest
     *          will hold the result
     * @return dest
     */
        public Sphere translate(float x, float y, float z, Sphere dest)
        {
            dest.x = this.x + x;
            dest.y = this.y + y;
            dest.z = this.z + z;
            return dest;
        }


    }
}
