using UnityEngine;
using System.Collections.Generic;
using Utils;

[ExecuteAlways] public class World_Path_Entity : MonoBehaviour
{
    [SerializeField] private Transform[] initPoint_array;

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
            var _segment_ind = (int)Mathf.Ceil(_spline_dist) ;
                
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
    public Vector3 Spline_Point_Get(float _length)
    {
        _length = Mathf.Clamp(_length, 0, spline_point_list[spline_point_list.Count - 1].length);
        var _point = Vector3.zero;

        for (var _i = 0; _i < spline_point_list.Count; ++_i)
        {
            if (spline_point_list[_i].length >= _length)
            {
                _point = spline_point_list[_i].position;
                break;
            }
        }

        return (_point);
    }

    #if UNITY_EDITOR

    private bool editor_draw_enable;
    [SerializeField] private GameObject editor_pointer;
    [SerializeField] [Range(0, 1)] private float editor_distance;
    [SerializeField] private bool editor_spline_point_show;
    private float EDITOR_SPLINE_POINT_SIZE = 0.025f;

    #endif

    private void Awake()
    {
        Spline_BuildPoints();
    }

    #if UNITY_EDITOR

    private void Update()
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

            if (editor_spline_point_show)
            {
                Spline_BuildPoints();

                for (var _i = 0; _i < spline_point_list.Count; ++_i)
                {
                    Gizmos.DrawSphere(spline_point_list[_i].position, EDITOR_SPLINE_POINT_SIZE);
                }
            }
        }
    }

    #endif
}
