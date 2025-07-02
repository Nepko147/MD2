using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneOpening_UICanvas_StartText : MonoBehaviour
{
    public static AppScreen_Local_SceneOpening_UICanvas_StartText SingleOnScene { get; private set; }

    private enum Stage
    {
        PressAnyKey,
        LoadData
    }
    private Stage stage = Stage.PressAnyKey;

    private Text text;
    private Color text_color;
    private bool text_color_anim_state = false;
    [SerializeField] private float text_color_anim_timeCf = 1.25f;

    private void Awake()
    {
        text = GetComponent<Text>();
        text.text = "PRESS ANY KEY";
        text_color = text.color;
    }

    private void Update()
    {
        if (!text_color_anim_state)
        {
            text_color.a -= text_color_anim_timeCf * Time.deltaTime;

            if (text_color.a <=0)
            {
                text_color_anim_state = true;
            }
        }
        else
        {
            text_color.a += text_color_anim_timeCf * Time.deltaTime;

            if (text_color.a >= 1)
            {
                text_color_anim_state = false;
            }
        }
        
        text.color = text_color;

        switch (stage)
        {
            case Stage.PressAnyKey:
            if (ControlScene_Opening.SingleOnScene.Stage_PressAnyKey_Pressed)
            {
                if (!ControlPers_DataHandler.SingleOnScene.IsDataLoaded)
                {
                    text.text = "LOADING CLOUD DATA";
                    stage = Stage.LoadData;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            break;

            case Stage.LoadData:
            if (ControlPers_DataHandler.SingleOnScene.IsDataLoaded)
            {
                Destroy(gameObject);
            }
            break;
        }
    }
}
