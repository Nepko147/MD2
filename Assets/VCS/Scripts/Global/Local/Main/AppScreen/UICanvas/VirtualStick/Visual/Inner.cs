using UnityEngine;

public class AppScreen_GeneralCanvas_VirtualStick_Visual_Inner : AppScreen_GeneralCanvas_VirtualStick_Visual_Parent
{
    private RectTransform rectTransform;

    public Vector3 RectTransform_Position_Set
    {
        set { rectTransform.position = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        AppScreen_GeneralCanvas_VirtualStick_Entity.SingleOnScene.Visual_Inner = this;
    }
}
