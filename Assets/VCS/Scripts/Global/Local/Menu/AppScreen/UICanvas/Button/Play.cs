using UnityEngine;
using UnityEngine.UI;

public class Appscreen_UICanvas_Button_Play : MonoBehaviour
{
    public static Appscreen_UICanvas_Button_Play Singletone { get; private set; }

    public bool Pressed { get; private set; }

    [SerializeField] private AudioClip  switchSound;
    Image                               image;    

    public void Hide()
    {
        image.enabled = false;
    }

    public void Show()
    {
        image.enabled = true;
    }

    public void OnClick()
    {
        ControlPers_AudioManager.Singletone.PlaySound(switchSound);
        Pressed = true;        
    }

    private void Awake()
    {
        Singletone = this;

        Pressed = false;
        image = GetComponent<Image>();
    }
}
