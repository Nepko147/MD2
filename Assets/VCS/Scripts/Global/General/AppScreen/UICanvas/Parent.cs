using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Parent : MonoBehaviour
{
    protected RectTransform rectTransform;
    /// <summary>
    /// Возвращает экранные координаты левого нижнего угла Rect Transform
    /// </summary>
    protected Vector2 RectTransform_ScreenPoint_Min(Vector2 _local_ofs = default(Vector2))
    {
        var _pos_local = new Vector3(rectTransform.offsetMin.x - rectTransform.localPosition.x + _local_ofs.x, rectTransform.offsetMin.y - rectTransform.localPosition.y + _local_ofs.y, 0);
        var _pos_world = rectTransform.localToWorldMatrix.MultiplyPoint(_pos_local);
        Vector2 _pos_screen = General_AppScreen_UICanvas_Entity.SingleOnScene.Camera.WorldToScreenPoint(_pos_world);

        return (_pos_screen);
    }
    /// <summary>
    /// Возвращает экранные координаты правого верхнего угла Rect Transform
    /// </summary>
    protected Vector2 RectTransform_ScreenPoint_Max(Vector2 _local_ofs = default(Vector2))
    {
        var _pos_local = new Vector3(rectTransform.offsetMax.x - rectTransform.localPosition.x + _local_ofs.x, rectTransform.offsetMax.y - rectTransform.localPosition.y + _local_ofs.y, 0);
        var _pos_world = rectTransform.localToWorldMatrix.MultiplyPoint(_pos_local);
        Vector2 _pos_screen = General_AppScreen_UICanvas_Entity.SingleOnScene.Camera.WorldToScreenPoint(_pos_world);

        return (_pos_screen);
    }

    private static Color image_color_pointed = new Color(1f, 1f, 1f);
    private static Color image_color_notPointed = new Color(0.85f, 0.85f, 0.85f);
    /// <summary>
    /// Возвращает экранные координаты левого нижнего угла Image
    /// </summary>
    protected Vector2 Image_ScreenPoint_Min(Image _image)
    {
        var _local_ofs = new Vector2(_image.raycastPadding.x , _image.raycastPadding.y);
        return (RectTransform_ScreenPoint_Min(_local_ofs));
    }
    /// <summary>
    /// Возвращает экранные координаты правого верхнего угла Image
    /// </summary>
    protected Vector2 Image_ScreenPoint_Max(Image _image)
    {
        var _local_ofs = new Vector2(-_image.raycastPadding.z, -_image.raycastPadding.w);
        return (RectTransform_ScreenPoint_Max(_local_ofs));
    }
    /// <summary>
    /// Выполнение общего поведения компонента Image
    /// </summary>
    protected void Image_Behaviour(Image _image, Vector2 _image_min, Vector2 _image_max)
    {
        if (Pointed(_image_min, _image_max))
        {
            _image.color = image_color_pointed;
        }
        else
        {
            _image.color = image_color_notPointed;
        }
    }

    /// <summary>
    /// Возвращает попадание позиции ввода на экране в указанные координаты
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

    protected void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
