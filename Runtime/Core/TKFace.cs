using UnityEngine;

namespace PMC.Meshy
{
    public class TKFace
    {
        public int Length { get; set; }
        public Vector3 Normal { get; set; }
        public TKLoop LoopFirst { get; set; }
    }

}
