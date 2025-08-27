using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class ControlScene_Opening : MonoBehaviour
{
    public static ControlScene_Opening SingleOnScene { get; private set; }

    private bool stage_delay = true;

    private bool stage_pressAnyKey = false;
    private float stage_pressAnyKey_delay = 1f;
    public bool Stage_PressAnyKey_Pressed { get; private set; }

    private bool stage_titleAnimation = false;

    [SerializeField] private AudioClip audio_crickets;

    private void Awake()
    {
        SingleOnScene = this;

        Stage_PressAnyKey_Pressed = false;
    }

    private void Start()
    {
        ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_crickets);

        if (ControlPers_BuildSettings.SingleOnScene.currentPlatformType != ControlPers_BuildSettings.CurrentPlatformType.web_yandexGames_desktop
        && ControlPers_BuildSettings.SingleOnScene.currentPlatformType != ControlPers_BuildSettings.CurrentPlatformType.web_yandexGames_mobile_android)
        {
            AppScreen_Local_SceneOpening_UICanvas_StartText.SingleOnScene.Enabled = true;

            stage_delay = false;
            stage_pressAnyKey = true;
        }
    }

    private void Update()
    {
        ControlPers_FogHandler.Move();

        if (stage_delay)
        {
            stage_pressAnyKey_delay -= Time.deltaTime;

            if (stage_pressAnyKey_delay <= 0)
            {
                AppScreen_Local_SceneOpening_UICanvas_StartText.SingleOnScene.Enabled = true;

                stage_delay = false;
                stage_pressAnyKey = true;
            }
        }

        if (stage_pressAnyKey)
        {
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
            if (AppScreen_Local_SceneOpening_UICanvas_Car.SingleOnScene.Done
            && AppScreen_Local_SceneOpening_UICanvas_Title.SingleOnScene.Done)
            {
                ControlPers_FogHandler.Color_Save();
                SceneManager.LoadScene(Constants.SCENEINDEX_MENU);
            }
        }
    }
}
