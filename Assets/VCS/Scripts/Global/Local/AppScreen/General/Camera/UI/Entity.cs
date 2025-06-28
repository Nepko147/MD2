using UnityEngine;

public class AppScreen_General_Camera_UI_Entity : AppScrren_General_Camera_Parent
{
    public static AppScreen_General_Camera_UI_Entity SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }
}
