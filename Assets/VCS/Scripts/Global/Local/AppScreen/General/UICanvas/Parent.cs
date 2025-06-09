using UnityEngine;
using UnityEngine.UI;

public class AppScreen_General_UICanvas_Parent : MonoBehaviour
{
    #region General

    protected RectTransform rectTransform;

    /// <summary>
    /// <para> Возвращает экранные координаты левого нижнего угла Rect Transform </para>
    /// <para> Вызывать после Awake() </para>
    /// </summary>
    protected Vector2 RectTransform_ScreenPoint_Min(Vector2 _local_ofs = default(Vector2))
    {
        var _pos_local = new Vector3(rectTransform.offsetMin.x - rectTransform.anchoredPosition.x + _local_ofs.x, rectTransform.offsetMin.y - rectTransform.anchoredPosition.y + _local_ofs.y, 0);
        var _pos_world = rectTransform.localToWorldMatrix.MultiplyPoint(_pos_local);
        Vector2 _pos_screen = AppScreen_General_UICanvas_Entity.SingleOnScene.Camera.WorldToScreenPoint(_pos_world);

        return (_pos_screen);
    }

    /// <summary>
    /// </para> Возвращает экранные координаты правого верхнего угла Rect Transform </para>
    /// <para> Вызывать после Awake() </para>
    /// </summary>
    protected Vector2 RectTransform_ScreenPoint_Max(Vector2 _local_ofs = default(Vector2))
    {
        var _pos_local = new Vector3(rectTransform.offsetMax.x - rectTransform.anchoredPosition.x + _local_ofs.x, rectTransform.offsetMax.y - rectTransform.anchoredPosition.y + _local_ofs.y, 0);
        var _pos_world = rectTransform.localToWorldMatrix.MultiplyPoint(_pos_local);
        Vector2 _pos_screen = AppScreen_General_UICanvas_Entity.SingleOnScene.Camera.WorldToScreenPoint(_pos_world);

        return (_pos_screen);
    }

    /// <summary>
    /// </para> Возвращает попадание позиции ввода на экране в указанные координаты </para>
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
    ///</para> Возвращает экранные координаты левого нижнего угла Image (Raycast Padding) </para>
    /// <para> Вызывать после Awake() </para>
    /// </summary>
    protected Vector2 Image_ScreenPoint_Min(Image _image)
    {
        var _local_ofs = new Vector2(_image.raycastPadding.x , _image.raycastPadding.y);
        return (RectTransform_ScreenPoint_Min(_local_ofs));
    }

    /// <summary>
    /// </para> Возвращает экранные координаты правого верхнего угла Image (Raycast Padding) </para>
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
    /// </para> Выполнение общего поведения визуального выделения компонента Image </para>
    /// <para> Вызывать после Awake() </para>
    /// </summary>
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

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
