using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class AppScreen_General_UICanvas_Parent : MonoBehaviour
{
    #region General

    protected RectTransform rectTransform;

    /// <summary>
    /// <para> Возвращает экранные координаты левого нижнего угла Rect Transform </para>
    /// <para> Вызывать после Awake() </para>
    /// </summary>
    public Vector2 RectTransform_ScreenPoint_Min(Vector2 _local_ofs = default(Vector2))
    {
        var _pos_local = new Vector3(rectTransform.offsetMin.x - rectTransform.anchoredPosition.x + _local_ofs.x, rectTransform.offsetMin.y - rectTransform.anchoredPosition.y + _local_ofs.y, 0);
        var _pos_world = rectTransform.localToWorldMatrix.MultiplyPoint(_pos_local);
        Vector2 _pos_screen = AppScreen_General_UICanvas_Entity.SingleOnScene.Camera.WorldToScreenPoint(_pos_world);

        return (_pos_screen);
    }

    /// <summary>
    /// <para> Возвращает экранные координаты правого верхнего угла Rect Transform </para>
    /// <para> Вызывать после Awake() </para>
    /// </summary>
    public Vector2 RectTransform_ScreenPoint_Max(Vector2 _local_ofs = default(Vector2))
    {
        var _pos_local = new Vector3(rectTransform.offsetMax.x - rectTransform.anchoredPosition.x + _local_ofs.x, rectTransform.offsetMax.y - rectTransform.anchoredPosition.y + _local_ofs.y, 0);
        var _pos_world = rectTransform.localToWorldMatrix.MultiplyPoint(_pos_local);
        Vector2 _pos_screen = AppScreen_General_UICanvas_Entity.SingleOnScene.Camera.WorldToScreenPoint(_pos_world);

        return (_pos_screen);
    }

    /// <summary>
    /// <para> Возвращает попадание позиции ввода на экране в указанные координаты </para>
    /// <para> Вызывать после Awake() </para>
    /// </summary>
    protected bool Pointed(Vector2 _min, Vector2 _max)
    {
        var _screen_position = ControlPers_InputHandler.SingleOnScene.Screen_Position;

        if (_screen_position.x >= _min.x
        && _screen_position.y >= _min.y
        && _screen_position.x <= _max.x
        && _screen_position.y <= _max.y)
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }

    #endregion

    #region Image

    /// <summary>
    /// <para> Возвращает экранные координаты левого нижнего угла Image (Raycast Padding) </para>
    /// <para> Вызывать после Awake() </para>
    /// </summary>
    protected Vector2 Image_ScreenPoint_Min(Image _image)
    {
        var _local_ofs = new Vector2(_image.raycastPadding.x , _image.raycastPadding.y);
        return (RectTransform_ScreenPoint_Min(_local_ofs));
    }

    /// <summary>
    /// <para> Возвращает экранные координаты правого верхнего угла Image (Raycast Padding) </para>
    /// <para> Вызывать после Awake() </para>
    /// </summary>
    protected Vector2 Image_ScreenPoint_Max(Image _image)
    {
        var _local_ofs = new Vector2(-_image.raycastPadding.z, -_image.raycastPadding.w);
        return (RectTransform_ScreenPoint_Max(_local_ofs));
    }

    private static Color image_highlight_color_idle = new Color(0.85f, 0.85f, 0.85f);
    private static Color image_highlight_color_pointed = new Color(1f, 1f, 1f);
    /// <summary>
    /// <para> Выполнение общего поведения подсветки компонента Image при наведении </para>
    /// <para> Вызывать после Awake() </para>
    /// </summary>
    
    protected void Image_Highlight_Behaviour(Image _image)
    {
        var _image_min = Image_ScreenPoint_Min(_image);
        var _image_max = Image_ScreenPoint_Max(_image);
        Image_Highlight_Behaviour(_image, _image_min, _image_max);
    }

    ///<summary>
    /// <para> Выполнение общего поведения подсветки компонента Image при наведении, с явной передачей координат Image </para>
    /// <para> Вызывать после Awake() </para>
    ///</summary>
    protected void Image_Highlight_Behaviour(Image _image, Vector2 _image_min, Vector2 _image_max)
    {
        if (!Pointed(_image_min, _image_max))
        {
            _image.color = image_highlight_color_idle;
        }
        else
        {
            _image.color = image_highlight_color_pointed;
        }
    }

    #endregion

    #region Shift

    private float   shift_time = 0;
    private float   shift_time_max;
    private Vector3 shift_pos_target;
    private Vector3 shift_pos_source;
    private Vector3 shift_pos_destination;
    private Vector3 shift_pos_speed;

    protected void Shift_Positions_Set(Vector3 _source, Vector3 _destination)
    {
        shift_pos_source = _source;
        shift_pos_destination = _destination;
        rectTransform.localPosition = shift_pos_source;
    }

    private void Shift_toTarget(Vector3 _targetPos, float _time)
    {
        shift_pos_target = _targetPos;
        shift_time = 0;
        shift_time_max = _time;
        shift_pos_speed = (_targetPos - rectTransform.localPosition) / _time;

        IEnumerator _Coroutine()
        {
            while (true)
            {
                rectTransform.localPosition += shift_pos_speed * Time.deltaTime;
                shift_time += Time.deltaTime;
            
                if (shift_time < shift_time_max)
                {
                    yield return (null);
                }
                else
                {
                    rectTransform.localPosition = shift_pos_target;
                    break;
                }
            }
        }

        var _routine = _Coroutine();
        StartCoroutine(_routine);
    }

    public void Shift_toSource(float _time)
    {
        Shift_toTarget(shift_pos_source, _time);
    }

    public void Shift_toDestination(float _time)
    {
        Shift_toTarget(shift_pos_destination, _time);
    }

    #endregion

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
