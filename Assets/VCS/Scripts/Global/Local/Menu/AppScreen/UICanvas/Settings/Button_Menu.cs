using UnityEngine;

public class AppScreen_UICanvas_Settings_Button_Menu : MonoBehaviour
{
    public static AppScreen_UICanvas_Settings_Button_Menu SingleOnScene { get; private set; }

    public bool Pressed { get; set; }

    [SerializeField] private AudioClip pressSound;

    public void OnClick()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(pressSound);
        Pressed = true;
    }

    private void Awake()
    {
        SingleOnScene = this;

        Pressed = false;
    }
}
