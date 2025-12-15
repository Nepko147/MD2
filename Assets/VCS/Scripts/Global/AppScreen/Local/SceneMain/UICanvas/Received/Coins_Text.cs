using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SeneMain_UICanvas_Received_Coins_Text : AppScreen_General_UICanvas_Parent
{
    private Text text;

    protected override void Awake()
    {
        base.Awake();

        text = GetComponent<Text>();
        text.font.material.mainTexture.filterMode = FilterMode.Point;
    }
}
