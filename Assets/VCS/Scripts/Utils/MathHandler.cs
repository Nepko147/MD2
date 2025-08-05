using UnityEngine;

namespace Utils
{
    public class AngleHandler
    {
        //ƒанный класс работает с диапазоном значений углов от -180f до 180f

        private static readonly Vector2 vector_origin = new Vector2(1f, 0); //»сходное направление соответствующее углу в ноль градусов

        ///<summary>
        ///ѕреобразование вектора в угол.
        ///</summary>
        public static float Vector_ToAngle(Vector2 _vector)
        {
            if (_vector.magnitude != 0)
            {
                var _scalar = vector_origin.x * _vector.x + vector_origin.y * _vector.y;
                var _angle_cos = _scalar / (vector_origin.magnitude * _vector.magnitude);
                var _angle_rad = Mathf.Acos(_angle_cos);
                var _angle = _angle_rad * Mathf.Rad2Deg * Mathf.Sign(_vector.y);
                
                return (Angle_ToSupportedRange(_angle));
            }
            else
            {
                return (0);
            }
        }

        ///<summary>
        ///ѕреобразование угла в диапазон от -180f до 180f.
        ///</summary>
        public static float Angle_ToSupportedRange(float _angle) 
        {
            var _offset = _angle + 180f;

            return (_offset - (Mathf.Floor(_offset / 360f ) * 360f) - 180f);
        }

        ///<summary>
        ///ѕреобразование угла в вектор.
        ///</summary>
        public static Vector2 Angle_ToVector(float _angle, float _length)
        {
            var _angle_rad = _angle * Mathf.Deg2Rad;
            var _x = vector_origin.x * Mathf.Cos(_angle_rad) - vector_origin.y * Mathf.Sin(_angle_rad);
            var _y = vector_origin.x * Mathf.Sin(_angle_rad) + vector_origin.y * Mathf.Cos(_angle_rad);

            return (new Vector2(_x, _y) * _length);
        }

        ///<summary>
        ///ѕреобразование исходного угла (_src_ang) в другой угол, повернутый в направлении заданного угла (_dest_ang) на величину
        ///завис€щую от коэффициента плавного поворота (_smoothStepCf), что позвол€ет при каждом вызове функции плавно поворачивать
        ///исходный угол к заданному.
        ///</summary>
        public static float Angle_SmoothStep(float _src_ang, float _dest_ang, float _smoothStepCf)
        {
            _src_ang = Angle_ToSupportedRange(_src_ang);
            _dest_ang = Angle_ToSupportedRange(_dest_ang);

            var _src_ang_abs = Mathf.Abs(_src_ang);
            var _dest_ang_abs = Mathf.Abs(_dest_ang);
            float _smooth_ang;

            if (_src_ang * _dest_ang < 0
            && _src_ang_abs + _dest_ang_abs > 180f)
            {
                _smooth_ang = _src_ang + (360f - _src_ang_abs - _dest_ang_abs) * _smoothStepCf * Mathf.Sign(_src_ang);
            }
            else
            {
                _smooth_ang = _src_ang - (_src_ang - _dest_ang) * _smoothStepCf;
            }

            return (Angle_ToSupportedRange(_smooth_ang));
        }

        /*
        ///<summary>
        ///ѕолучение вектора со случайным направлением и длинной 1.
        ///</summary>
        public static Vector2 Vector_RandomDirection()
        {
            return (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized);
        }
        */
    }
}
