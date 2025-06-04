using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Main_Button_Play : AppScreen_UICanvas_Parent
{
    public static AppScreen_UICanvas_Menu_Main_Button_Play SingleOnScene { get; private set; }

    public bool Pressed { get; private set; }

    [SerializeField] private AudioClip pressSound;

    Image image;

    public bool Visible
    {
        get { return (image.enabled); }
        set { image.enabled = value; }
    }

    public void OnClick()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(pressSound);
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
