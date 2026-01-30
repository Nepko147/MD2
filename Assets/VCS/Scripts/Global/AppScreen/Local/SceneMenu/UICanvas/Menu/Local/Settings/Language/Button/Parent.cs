using UnityEngine;

public abstract class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Parent : AppScreen_General_UICanvas_Button_Parent
{
    [SerializeField] private Sprite image_currennt_idle_sf;
    [SerializeField] private Sprite image_currennt_pointed_sf;
    [SerializeField] private Sprite image_currennt_pressed_sf;

    protected void Pressed_OffAll()
    {
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_English.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Russian.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Portuguese.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_German.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_French.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Italian.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Polish.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Turkish.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Kazakh.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Belarusian.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Ukrainian.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Uzbek.SingleOnScene.Pressed = false;
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Settings_Language_Button_Indonesian.SingleOnScene.Pressed = false;
    }

    protected override void Awake()
    {
        image_currennt_idle = image_currennt_idle_sf;
        image_currennt_pointed = image_currennt_pointed_sf;
        image_currennt_pressed = image_currennt_pressed_sf;

        base.Awake();
    }

    protected override void Start()
    {
        base.Start();

        Image_PointsRefresh();
    }
}
