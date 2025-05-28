using UnityEngine;

public class General_AppScreen_UICanvas_Entity : MonoBehaviour
{
    public static General_AppScreen_UICanvas_Entity SingleOnScene { get; private set; }

    public Camera Camera { get; private set; }
    
    /// <summary>
    /// Возвращает экранные координаты левого нижнего угла указанного Rect Transform
    /// </summary>
    public Vector2 RectTransformToScreenPoint_Min(RectTransform _rectTransform)
    {
        var _pos_local = new Vector3(_rectTransform.offsetMin.x - _rectTransform.localPosition.x, _rectTransform.offsetMin.y - _rectTransform.localPosition.y, 0);
        var _pos_world = _rectTransform.localToWorldMatrix.MultiplyPoint(_pos_local);
        Vector2 _pos_screen = Camera.WorldToScreenPoint(_pos_world);
        
        return (_pos_screen);
    }

    /// <summary>
    /// Возвращает экранные координаты правого верхнего угла указанного Rect Transform
    /// </summary>
    public Vector2 RectTransformToScreenPoint_Max(RectTransform _rectTransform)
    {
        var _pos_local = new Vector3(_rectTransform.offsetMax.x - _rectTransform.localPosition.x, _rectTransform.offsetMax.y - _rectTransform.localPosition.y, 0);
        var _pos_world = _rectTransform.localToWorldMatrix.MultiplyPoint(_pos_local);
        Vector2 _pos_screen = Camera.WorldToScreenPoint(_pos_world);

        return (_pos_screen);
    }

    private void Awake()
    {
        SingleOnScene = this;

        Camera = GetComponent<Canvas>().worldCamera;
    }
}
