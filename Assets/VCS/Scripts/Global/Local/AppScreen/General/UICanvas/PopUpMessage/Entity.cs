using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class AppScrren_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_PopUpMessage_Entity
: AppScreen_General_UICanvas_Parent
{
    private Text text;

    private float scale = 0;
    private Vector3 scale_vec3;
    private const float SCALE_TIME = 0.2f;
    private bool scale_change = true;

    public string Text
    {
        get { return (text.text); }
        set { text.text = value; }
    }

    protected override void Awake()
    {
        base.Awake();

        text = GetComponentInChildren<Text>();

        scale_vec3 = new Vector3(scale, scale, 1f);
        rectTransform.localScale = scale_vec3;
    }

    private void Update()
    {
        if (scale_change)
        {
            scale += Time.deltaTime / SCALE_TIME;
            scale_vec3.x = scale;
            scale_vec3.y = scale;
            rectTransform.localScale = scale_vec3;

            if (scale >= 1f)
            {
                scale = 1f;
                scale_change = false;
            }
        }

        if (AppScrren_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_PopUpMessage_Button.SingleOnScene.Pressed)
        {
            AppScreen_General_UICanvas_Entity.SingleOnScene.PopUpMessage_Remove();
        }
    }
}
