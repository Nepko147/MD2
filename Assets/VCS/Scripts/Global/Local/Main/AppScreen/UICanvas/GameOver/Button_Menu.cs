using UnityEngine;
using UnityEngine.UI;

public class Appscreen_UICanvas_Button_Menu : MonoBehaviour
{
    public static Appscreen_UICanvas_Button_Menu Singletone { get; private set; }

    public bool Pressed { get; set; }

    [SerializeField] private AudioClip switchSound;

    private void Awake()
    {
        Singletone = this;

        Pressed = false;
        GetComponent<Image>().enabled = false;
    }

    public void OnClick()
    {
        Pressed = true;
        ControlPers_AudioManager.Singletone.PlaySound(switchSound);
    }
}
