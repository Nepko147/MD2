using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Opening : MonoBehaviour
{
    public static ControlScene_Opening SingleOnScene { get; private set; }

    private bool stage_pressAnyKey = true;
    public bool Stage_PressAnyKey_Pressed { get; private set; }
    private bool stage_titleAnimation = true;

    [SerializeField] private AudioClip audio_crickets;

    private void Awake()
    {
        SingleOnScene = this;

        Stage_PressAnyKey_Pressed = false;
    }

    private void Start()
    {
        ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_crickets);
    }

    private void Update()
    {
        if (stage_pressAnyKey)
        {
            ControlPers_FogHandler.Move();

            if (!Stage_PressAnyKey_Pressed)
            {
                if (Input.anyKey)
                {
                    Stage_PressAnyKey_Pressed = true;
                }
            }

            if (Stage_PressAnyKey_Pressed
            && ControlPers_DataHandler.SingleOnScene.IsDataLoaded)
            {
                AppScreen_Local_SceneOpening_UICanvas_Car.SingleOnScene.Move();
                AppScreen_Local_SceneOpening_UICanvas_Title.SingleOnScene.PlayAnimation();

                stage_pressAnyKey = false;
                stage_titleAnimation = true;
            }
        }

        if (stage_titleAnimation)
        {
            ControlPers_FogHandler.Move();

            if (AppScreen_Local_SceneOpening_UICanvas_Car.SingleOnScene.Done
            && AppScreen_Local_SceneOpening_UICanvas_Title.SingleOnScene.Done)
            {
                ControlPers_FogHandler.Color_Save();
                SceneManager.LoadScene(Constants.SCENEINDEX_MENU);
            }
        }
    }
}
