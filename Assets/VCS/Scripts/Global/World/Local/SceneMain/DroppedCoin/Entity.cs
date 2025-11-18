using UnityEngine;
using System.Collections.Generic;
using Utils;

public class World_Local_SceneMain_DroppedCoin_Entity : MonoBehaviour
{
    public bool Active { get; set; }

    [SerializeField] protected Transform[] initPoint_array;

    private enum Segment_InitPoint_Ind
    {
        first,
        second,
        third,
        fourth,
        size
    }

    private const float SEGMENT_POINT_NUMBER = 200f;

    private Vector3 Segment_Point_Get(float _distance)
    {
        var _spline_dist = initPoint_array.Length / BezierCurve.DEGREE * _distance;
        var _segment_ind = (int)Mathf.Ceil(_spline_dist);

        if (_segment_ind > 0)
        {
            --_segment_ind;
        }

        var _ind_ofs = BezierCurve.DEGREE * _segment_ind;
        var _point_1 = initPoint_array[(int)Segment_InitPoint_Ind.first + _ind_ofs].position;
        var _point_2 = initPoint_array[(int)Segment_InitPoint_Ind.second + _ind_ofs].position;
        var _point_3 = initPoint_array[(int)Segment_InitPoint_Ind.third + _ind_ofs].position;
        var _point_4 = initPoint_array[(int)Segment_InitPoint_Ind.fourth + _ind_ofs].position;

        return (BezierCurve.Point_Get(_point_1, _point_2, _point_3, _point_4, _spline_dist - _segment_ind));
    }

    private struct Spline_Point
    {
        public Spline_Point(int _segment_ind, Vector3 _position, float _length)
        {
            segment_ind = _segment_ind;
            position = _position;
            length = _length;
        }

        public int segment_ind;
        public Vector3 position;
        public float length;
    }

    private List<Spline_Point> spline_point_list = new List<Spline_Point>();
    private delegate void Spline_WalkThrou_CustomFunc(int _segment_ind, Vector3 _point_prev, Vector3 _point_current);

    private void Spline_WalkThrou(Spline_WalkThrou_CustomFunc _CustomFunc)
    {
        var _segment_num = initPoint_array.Length / BezierCurve.DEGREE;
        var _i_max = _segment_num * SEGMENT_POINT_NUMBER;
        var _point_prev = initPoint_array[0].position;

        for (float _i = 1; _i <= _i_max; ++_i)
        {
            var _spline_dist = _segment_num * _i / _i_max;
            var _segment_ind = (int)Mathf.Ceil(_spline_dist);

            if (_segment_ind > 0)
            {
                --_segment_ind;
            }

            var _ind_ofs = BezierCurve.DEGREE * _segment_ind;
            var _point_current = BezierCurve.Point_Get(initPoint_array[0 + _ind_ofs].position, initPoint_array[1 + _ind_ofs].position, initPoint_array[2 + _ind_ofs].position, initPoint_array[3 + _ind_ofs].position, _spline_dist - _segment_ind);

            _CustomFunc(_segment_ind, _point_prev, _point_current);

            _point_prev = _point_current;
        }
    }

    private void Spline_BuildPoints()
    {
        spline_point_list.Clear();
        var _spline_point = new Spline_Point(0, initPoint_array[0].position, 0);
        spline_point_list.Add(_spline_point);

        Spline_Length = 0;

        Spline_WalkThrou_CustomFunc _CustomFunc = (_segment_ind, _point_prev, _point_current) =>
        {
            var _spline_point_length = Spline_Length + Vector3.Distance(_point_prev, _point_current);
            var _spline_point = new Spline_Point(_segment_ind, _point_current, _spline_point_length);
            spline_point_list.Add(_spline_point);
            Spline_Length = _spline_point_length;
        };

        Spline_WalkThrou(_CustomFunc);
    }

    ///<summary>
    ///Получение длинны всего пути в юнитах.
    ///</summary>
    public float Spline_Length { get; private set; }

    ///<summary>
    ///Получение точки на пути по промежуточному значению её длинны в юнитах.
    ///</summary>    

    private bool editor_draw_enable;
    [SerializeField] protected GameObject editor_pointer;
    [SerializeField] [Range(0, 1)] protected float editor_distance;

    private const float DROP_SPEED = 3.0f;
    private const float DROP_RADIUS = 1.0f;

    private void Awake()
    {
        Active = true;

        Spline_BuildPoints();

        var _x = Random.Range(-DROP_RADIUS, DROP_RADIUS);
        var _y = Random.Range(-DROP_RADIUS, DROP_RADIUS);
        var _position = Vector2.right * _x + Vector2.up * _y;

        foreach (var _point in initPoint_array)
        {
            _point.gameObject.transform.localPosition *= _position;
        }

        editor_distance = 0;
    }

    private void Update()
    {
        if (Active)
        {
            editor_draw_enable = false;

            var _point_array_full = true;

            for (var _i = 0; _i < initPoint_array.Length; ++_i)
            {
                if (initPoint_array[_i] == null)
                {
                    _point_array_full = false;
                    break;
                }
            }

            if (_point_array_full == true)
            {
                var _spline_dist_full = initPoint_array.Length / BezierCurve.DEGREE;
                var _segment_ind_full = (int)Mathf.Ceil(_spline_dist_full);

                if (_segment_ind_full > 0)
                {
                    --_segment_ind_full;
                }

                var _ind_ofs_full = BezierCurve.DEGREE * _segment_ind_full;

                if ((int)Segment_InitPoint_Ind.size - 1 + _ind_ofs_full < initPoint_array.Length)
                {
                    editor_pointer.transform.position = Segment_Point_Get(editor_distance);
                    editor_draw_enable = true;
                }
            }

            editor_distance += DROP_SPEED * (1 - editor_distance) * ControlScene_Main.SingleOnScene.TimeDilation_Coef * Time.deltaTime;

            if (editor_distance >= 0.9f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (editor_draw_enable)
        {
            Spline_WalkThrou_CustomFunc _CustomFunc = (_segment_ind, _point_prev, _point_current) =>
            {
                Gizmos.DrawLine(_point_prev, _point_current);
            };

            Spline_WalkThrou(_CustomFunc);
        }
    }
}
