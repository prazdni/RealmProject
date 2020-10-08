using UnityEngine;

namespace Realm
{
    public static class Reference
    {
        public static Material GetMaterial(PuzzleNodeType nodeType)
        {
            Material material = null;
            
            switch (nodeType)
            {
                case PuzzleNodeType.Star:
                    material = Resources.Load("Star") as Material;
                    break;
                case PuzzleNodeType.Diamond:
                    material = Resources.Load("Diamond") as Material;
                    break;
                case PuzzleNodeType.Medal:
                    material = Resources.Load("Medal") as Material;
                    break;
            }

            return material;
        }
    }
}