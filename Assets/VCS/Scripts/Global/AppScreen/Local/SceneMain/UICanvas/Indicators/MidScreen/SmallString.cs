using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMain_UICanvas_Indicators_MidScreen_SmallString : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Indicators_MidScreen_SmallString SingleOnScene { get; private set; }

    Text text;

    private Vector3 position_init;
    private Vector3 position_ending;
    private float   position_ending_offset_y = 20.0f;

    public void Position_Ending_Set()
    {
        rectTransform.anchoredPosition3D = position_ending;
    }

    public void Enable(bool _state)
    {
        text.enabled = _state;
    }

    public void UpdateText(string _string)
    {
        text.text = _string;
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        text = GetComponent<Text>();
        text.font.material.mainTexture.filterMode = FilterMode.Point;

        position_init = rectTransform.anchoredPosition3D;
        position_ending = position_init - Vector3.up * position_ending_offset_y;
    }
}
