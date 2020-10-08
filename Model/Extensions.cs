using System.Collections.Generic;
using System.Linq;

namespace Realm
{
    public static class Extensions
    {
        public static Dictionary<float, List<InteractableObject>> ToGroupedDictionary(
            this List<InteractableObject> self)
        {
            Dictionary<float, List<InteractableObject>> dictList = new Dictionary<float, List<InteractableObject>>();
            
            var tmp = self.GroupBy(u => u.transform.localPosition.x);
            
            foreach (var o in tmp)
            {
                var tmpList = new List<InteractableObject>();
                
                foreach (var t in o)
                {
                    tmpList.Add(t);
                }
                
                dictList.Add(o.Key, tmpList);
            }

            return dictList;
        }
    }
}