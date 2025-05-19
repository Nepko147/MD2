using UnityEngine;
using UnityEngine.UI;

public class Appscreen_UICanvas_Button_Settings : MonoBehaviour
{
    public static Appscreen_UICanvas_Button_Settings SingleOnScene { get; private set; }

    public bool Pressed { get; set; }

    [SerializeField] private AudioClip switchSound;
    Image image;

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
        ControlPers_AudioManager.SingleOnScene.PlaySound(switchSound);
        Pressed = true;
    }

    private void Awake()
    {
        SingleOnScene = this;

        Pressed = false;
        image = GetComponent<Image>();
    }
}
