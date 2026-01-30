using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_Received_AD_Button : AppScreen_General_UICanvas_Button_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Received_AD_Button SingleOnScene { get; private set; }

    [SerializeField] private Sprite image_currennt_idle_sf;
    [SerializeField] private Sprite image_currennt_pointed_sf;
    [SerializeField] private Sprite image_currennt_pressed_sf;

    protected override void Awake()
    {
        image_currennt_idle = image_currennt_idle_sf;
        image_currennt_pointed = image_currennt_pointed_sf;
        image_currennt_pressed = image_currennt_pressed_sf;

        base.Awake();

        SingleOnScene = this;

        Visible = false;
    }

    protected override void Start()
    {
        base.Start();

        Image_PointsRefresh();
    }
}
