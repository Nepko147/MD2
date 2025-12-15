using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMain_UICanvas_Cutscene_Statistics_NewRecord : MonoBehaviour
{
    [SerializeField] private Color color_pink;
    [SerializeField] private Color color_cyan;
    private float                  color_speed = 4.0f;

    private Vector3 upscale_max = Vector3.one * 1.2f;
    private Vector3 upscale_min = Vector3.one * 0.9f;
    private float   upscale_speed = 2.0f;

    private RectTransform rectTransform;

    private Text text;
    public void Text_LanguageRefresh()
    {
        text.text = ControlPers_LanguageHandler_Entity.SingleOnScene.Text_Get(ControlPers_LanguageHandler_Entity.Text_Key.statistics_newRecord);
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<Text>();
    }

    private void Start()
    {
        Text_LanguageRefresh();
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate += Text_LanguageRefresh;
    }

    private void Update()
    {
        var _lerpedScale = Vector3.Lerp(upscale_min, upscale_max, Mathf.PingPong(Time.time * upscale_speed, 1));
        rectTransform.localScale = _lerpedScale;

        var _lerpedColor = Color.Lerp(color_cyan, color_pink, Mathf.PingPong(Time.time * color_speed, 1));
        text.color = _lerpedColor;
    }
    private void OnDestroy()
    {
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate -= Text_LanguageRefresh;
    }
}
