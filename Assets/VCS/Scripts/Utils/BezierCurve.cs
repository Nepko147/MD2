using UnityEngine;

namespace Utils
{
    public static class BezierCurve
    {
        ///<summary>
        /// Степерь кривой Безье (Кол-во точек которыми задается кривая без учета начальной).
        ///</summary>
        public const int DEGREE = 3;

        ///<summary>
        ///Получение позиции точки на кривой Безье, по четырем образующим точкам и нормализованному значению расстояния.
        ///</summary>
        public static Vector3 Point_Get(Vector3 _point_1, Vector3 _point_2, Vector3 _point_3, Vector3 _point_4, float _dist)
        {
            _dist = Mathf.Clamp01(_dist);
            var _dist_revert = 1f - _dist;

            return 
            (
                _point_1 * Mathf.Pow(_dist_revert, 3f) +
                _point_2 * Mathf.Pow(_dist_revert, 2f) * 3f * _dist +
                _point_3 * _dist_revert * 3f * Mathf.Pow(_dist, 2f) +
                _point_4 * Mathf.Pow(_dist, 3f)
            );
        }
    }
}
