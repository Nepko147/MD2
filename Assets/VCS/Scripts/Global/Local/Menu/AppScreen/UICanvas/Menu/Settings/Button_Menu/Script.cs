using UnityEngine;

public class AppScreen_UICanvas_Menu_Settings_Button_Menu : AppScreen_UICanvas_Parent
{
    public static AppScreen_UICanvas_Menu_Settings_Button_Menu SingleOnScene { get; private set; }

    public bool Pressed { get; set; }

    private AudioSource audioSource;

    public void OnClick()
    {
        audioSource.Play();
        Pressed = true;
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Pressed = false;

        audioSource = GetComponent<AudioSource>();
    }
}
