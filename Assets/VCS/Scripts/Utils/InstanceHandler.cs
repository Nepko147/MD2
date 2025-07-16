using UnityEngine;

namespace Utils
{
    public class InstanceHandler : MonoBehaviour
    {
        public static bool AnyInstanceExists<T>() where T : Object
        {
            foreach (var _item in FindObjectsByType<T>(FindObjectsSortMode.None))
            {
                return (true);
            }
            
            return (false);
        }
    }
}
