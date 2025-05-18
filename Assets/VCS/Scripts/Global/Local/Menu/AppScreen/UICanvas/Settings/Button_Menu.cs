using UnityEngine;

public class AppScreen_UICanvas_Settings_Button_Menu : MonoBehaviour
{
    public static AppScreen_UICanvas_Settings_Button_Menu Singletone { get; private set; }

    public bool Pressed { get; set; }

    [SerializeField] private AudioClip switchSound;

    public void OnClick()
    {
        ControlPers_AudioManager.Singletone.PlaySound(switchSound);
        Pressed = true;
    }

    private void Awake()
    {
        Singletone = this;

        Pressed = false;
    }
}
