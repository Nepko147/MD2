using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_Bushes : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Bushes SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    private void Start()
    {
        var _destination = new Vector3(rectTransform.localPosition.x - 1200f, rectTransform.localPosition.y, rectTransform.localPosition.z);
        Shift_Positions_Set(rectTransform.localPosition, _destination);
    }
}
