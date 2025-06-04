using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Main_Button_Upgrades : AppScreen_UICanvas_Parent
{
    public static AppScreen_UICanvas_Menu_Main_Button_Upgrades SingleOnScene { get; private set; }

    public bool Pressed { get; set; }

    private Image image;

    private AudioSource audioSource;

    public bool Visible
    {
        get { return (image.enabled); }
        set { image.enabled = value; }
    }

    public void OnClick()
    {
        Pressed = true;

        audioSource.Play();
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Pressed = false;

        image = GetComponent<Image>();

        audioSource = GetComponent<AudioSource>();
    }
}
