using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Appscreen_UICanvas_Pause_Button_Resume : MonoBehaviour
{
    public static Appscreen_UICanvas_Pause_Button_Resume SingleOnScene { get; private set; }

    public bool Pressed { get; set; }

    private AudioSource audioSource;

    private void Awake()
    {
        SingleOnScene = this;
        Pressed = false;

        audioSource = GetComponent<AudioSource>();

        GetComponent<Image>().enabled = false;
    }

    public void OnClick()
    {
        Pressed = true;
        audioSource.Play();
    }
}
