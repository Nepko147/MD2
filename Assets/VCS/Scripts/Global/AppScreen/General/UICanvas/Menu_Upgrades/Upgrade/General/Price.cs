using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Upgrades_Upgrade_General_Price : AppScreen_General_UICanvas_Parent
{
    private Text text;

    public void LocalPosition_Set(Vector3 _pos)
    {
        rectTransform.localPosition = _pos;
    }

    public void Coins_Set(int _coins)
    {
        text.text = _coins.ToString();
    }

    protected override void Awake()
    {
        base.Awake();

        text = GetComponentInChildren<Text>();
    }
}
