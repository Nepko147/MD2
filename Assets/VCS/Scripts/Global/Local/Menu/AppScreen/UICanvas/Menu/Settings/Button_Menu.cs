using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Settings_Button_Menu : MonoBehaviour
{
    public static AppScreen_UICanvas_Menu_Settings_Button_Menu SingleOnScene { get; private set; }

    public bool Pressed { get; set; }

    private AudioSource audioSource;

    public void OnClick()
    {
        audioSource.Play();
        Pressed = true;
    }

    private void Awake()
    {
        SingleOnScene = this;

        Pressed = false;

        audioSource = GetComponent<AudioSource>();
    }
}
