using UnityEngine;

namespace PMC.Meshy
{
    public class Face
    {
        public int TotVerts { get; set; }
        public Vector3 Normal { get; set; } = new Vector3();
        public Loop First { get; set; }

        public Face(Vector3 normal, Loop l)
        {
            Normal.Set(normal.x,normal.y,normal.z);
            First = l;
            l.Face = this;
        }
    }

}
