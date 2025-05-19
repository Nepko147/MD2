using UnityEngine;
using UnityEngine.UI;

public class Appscreen_UICanvas_GameOver_Button_Restart : MonoBehaviour
{
    public static Appscreen_UICanvas_GameOver_Button_Restart SingleOnScene { get; private set; }

    public bool Pressed { get; set; }

    [SerializeField] private AudioClip switchSound;

    private void Awake()
    {
        SingleOnScene = this;

        Pressed = false;
        GetComponent<Image>().enabled = false;
    }

    public void OnClick()
    {
        Pressed = true;
        ControlPers_AudioManager.SingleOnScene.PlaySound(switchSound);
    }
}
