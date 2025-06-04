using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Main_Button_Quit : AppScreen_UICanvas_Parent
{
    public static AppScreen_UICanvas_Menu_Main_Button_Quit SingleOnScene { get; private set; }

    public bool Pressed { get; private set; }

    Image image;

    public bool Visible
    {
        get { return (image.enabled); }
        set { image.enabled = value; }
    }

    public void OnClick()
    {
        Pressed = true;
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Pressed = false;

        image = GetComponent<Image>();
    }
}
