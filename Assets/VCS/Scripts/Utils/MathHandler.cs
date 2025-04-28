using UnityEngine;

namespace Utils
{
    public class MathHandler
    {
        //»сходное направление от которого считаютс€ углы (угол равный ноль градусов)
        static Vector2 initialVector = new Vector2(1f, 0);

        //ѕреобразование вектора в угол
        public static float VectorToAngle(Vector2 _vector)
        {
            if (_vector.magnitude != 0)
            {
                var _scalar = initialVector.x * _vector.x + initialVector.y * _vector.y;
                var _angle_cos = _scalar / (initialVector.magnitude * _vector.magnitude);
                var _angle_rad = Mathf.Acos(_angle_cos);
                var _angle = _angle_rad * Mathf.Rad2Deg;
                return _angle * Mathf.Sign(_vector.y);
            }
            else
            {
                return 0;
            }
        }

        //ѕреобразование угла в вектор
        public static Vector2 AngleToVector(float _angle, float _length)
        {
            var _angle_rad = _angle * Mathf.Deg2Rad;
            var _x = initialVector.x * Mathf.Cos(_angle_rad) - initialVector.y * Mathf.Sin(_angle_rad);
            var _y = initialVector.x * Mathf.Sin(_angle_rad) + initialVector.y * Mathf.Cos(_angle_rad);
            var _vector = new Vector2(_x, _y) * _length;
            return _vector;
        }

        //ѕреобразование исходного угла (_srcVal) в другой угол, повернутый в направлении заданного угла (_destVal) на величину
        //завис€щую от коэффициента плавного поворота (_stepCf), что позвол€ет при каждом вызове функции плавно поворачивать
        //исходный угол к заданному.
        //¬озвращаемое значение угла лежит в диапазоне от -180 до 180
        public static float AngleSmoothStep(float _srcVal, float _destVal, float _stepCf)
        {
            float _smoothVal;
            var _srcVal_abs = Mathf.Abs(_srcVal);
            var _destVal_abs = Mathf.Abs(_destVal);

            if (_srcVal * _destVal < 0
            && _srcVal_abs + _destVal_abs > 180f)
            {
                var _diff = 360f - _srcVal_abs - _destVal_abs;
                var _smoothStep = _diff * _stepCf;
                _smoothVal = _srcVal + _smoothStep * Mathf.Sign(_srcVal);

                if (_smoothVal < -180f)
                {
                    _smoothVal = 360f + _smoothVal;
                }
                else
                {
                    if (_smoothVal > 180f)
                    {
                        _smoothVal = -360f + _smoothVal;
                    }
                }
            }
            else
            {
                var _diff = _srcVal - _destVal;
                var _smoothStep = _diff * _stepCf;
                _smoothVal = _srcVal - _smoothStep;
            }

            return _smoothVal;
        }

        //ѕолучение случайного направлени€
        /*
        public static Vector2 GetRandomDirection()
        {
            var _dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            return _dir;
        }
        */
    }
}
