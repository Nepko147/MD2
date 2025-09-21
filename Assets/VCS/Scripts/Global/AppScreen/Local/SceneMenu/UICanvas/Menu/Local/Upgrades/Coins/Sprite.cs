using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Coins_Sprite : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Coins_Sprite SingleOnScene { get; private set; }

    private Image image;
    private Color image_color = Color.white;

    public void Alpha_Set(float _alpha)
    {
        image_color.a = _alpha;
        image.color = image_color;
    }

    protected override void Awake()
    {
        SingleOnScene = this;

        image = GetComponent<Image>();
    }
}
