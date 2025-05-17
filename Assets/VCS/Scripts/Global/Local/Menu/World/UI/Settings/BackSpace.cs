using UnityEngine;

public class AppScreen_Canvas_Settings_BackSpace : MonoBehaviour
{
    public static AppScreen_Canvas_Settings_BackSpace Singletone { get; private set; }

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
