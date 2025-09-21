using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_Cutscene_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Cutscene_Entity SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    private void Start()
    {
        var _destination = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y + 200.0f, rectTransform.localPosition.z);
        Shift_Positions_Set(rectTransform.localPosition, _destination);
    }
}
