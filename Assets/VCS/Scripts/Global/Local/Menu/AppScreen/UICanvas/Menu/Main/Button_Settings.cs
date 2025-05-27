using UnityEngine;
using UnityEngine.UI;

public class AppScreen_UICanvas_Menu_Main_Button_Settings : MonoBehaviour
{
    public static AppScreen_UICanvas_Menu_Main_Button_Settings SingleOnScene { get; private set; }

    public bool Pressed { get; set; }

    [SerializeField] private AudioClip pressSound;
    Image image;

    public bool Visible
    {
        get { return (image.enabled); }
        set { image.enabled = value; }
    }

    public void OnClick()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(pressSound);
        Pressed = true;
    }

    private void Awake()
    {
        SingleOnScene = this;

        Pressed = false;
        image = GetComponent<Image>();
    }
}
