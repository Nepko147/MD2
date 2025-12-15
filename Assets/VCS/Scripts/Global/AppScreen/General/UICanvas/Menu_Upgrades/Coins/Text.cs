using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Upgrades_Coins_Text : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_UICanvas_Menu_Upgrades_Coins_Text SingleOnScene { get; private set; }

    private Text text;
    private Color text_color = Color.white;

    public void Coins_Set(int _coins)
    {
        text.text = _coins.ToString();
    }

    public void Alpha_Set(float _alpha)
    {
        text_color.a = _alpha;
        text.color = text_color;
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        text = GetComponent<Text>();
        text.font.material.mainTexture.filterMode = FilterMode.Point;

        Coins_Set(ControlPers_DataHandler.SingleOnScene.ProgressData_Coins);
        ControlPers_DataHandler.SingleOnScene.ProgressData_Coins_OnChange += Coins_Set;
    }

    private void OnDestroy()
    {
        ControlPers_DataHandler.SingleOnScene.ProgressData_Coins_OnChange -= Coins_Set;
    }
}
