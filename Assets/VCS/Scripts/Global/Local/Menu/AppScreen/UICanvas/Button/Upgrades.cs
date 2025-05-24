using UnityEngine;
using UnityEngine.UI;

public class Appscreen_UICanvas_Button_Upgrades : MonoBehaviour
{
    public static Appscreen_UICanvas_Button_Upgrades SingleOnScene { get; private set; }

    public bool Pressed { get; private set; }

    [SerializeField] private AudioClip pressSound;
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
        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(pressSound);
    }

    private void Awake()
    {
        SingleOnScene = this;

        Pressed = false;
        image = GetComponent<Image>();
    }
}
