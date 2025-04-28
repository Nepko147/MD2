using UnityEngine;
using UnityEngine.UI;

public class AppScreen_GeneralCanvas_VirtualStick_Visual_Inner : AppScreen_GeneralCanvas_VirtualStick_Visual_Parent
{
    private RectTransform rectTransform;

    public Vector3 RectTransform_Position_Set
    {
        set { rectTransform.position = value; }
    }

    private new void Awake()
    {
        base.Awake();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        AppScreen_GeneralCanvas_VirtualStick_Entity.Singleton.Visual_Inner = this;
    }
}
