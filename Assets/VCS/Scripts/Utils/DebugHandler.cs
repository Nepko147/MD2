using UnityEngine;

namespace Utils
{
    public class DebugHandler
    {
        public static void DrawLine2D(float _x1, float _y1, float _x2, float _y2, Color _color)
        {
            var _source = new Vector3(_x1, _y1, 0);
            var _destination = new Vector3(_x2, _y2, 0);
            Debug.DrawLine(_source, _destination, _color);
        }
    }
}
