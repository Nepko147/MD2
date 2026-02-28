using UnityEngine;
using System.Collections;
using YG;

public class ControlPers_YandexGamesHandler : MonoBehaviour
{
    public static ControlPers_YandexGamesHandler SingleOnScene { get; private set; }

    public const float AD_WAITINGTIME = 0.3f;

    private bool gameReadyAPI_focus_switch = true;
    public bool GameReadyAPI_IsOnGameplay { get; private set; } = false;

    public void GameReadyAPI_Start()
    {
        if (ControlPers_BuildSettings.SingleOnScene.BuildRuntimeType_Current == ControlPers_BuildSettings.BuildRuntimeType.web_yandexGames_desktop
        || ControlPers_BuildSettings.SingleOnScene.BuildRuntimeType_Current == ControlPers_BuildSettings.BuildRuntimeType.web_yandexGames_mobile_android)
        {
            GameReadyAPI_IsOnGameplay = true;

            YG2.GameplayStart();
        }
    }

    public void GameReadyAPI_Stop()
    {
        if (ControlPers_BuildSettings.SingleOnScene.BuildRuntimeType_Current == ControlPers_BuildSettings.BuildRuntimeType.web_yandexGames_desktop
        || ControlPers_BuildSettings.SingleOnScene.BuildRuntimeType_Current == ControlPers_BuildSettings.BuildRuntimeType.web_yandexGames_mobile_android)
        {
            GameReadyAPI_IsOnGameplay = false;

            YG2.GameplayStop();
        }
    }

    private IEnumerator Update_Coroutine()
    {
        while (true)
        {
            if (YG2.isFocusWindowGame)
            {
                if (!gameReadyAPI_focus_switch)
                {
                    if (GameReadyAPI_IsOnGameplay)
                    {
                        YG2.GameplayStart();
                    }

                    gameReadyAPI_focus_switch = true;
                }
            }
            else
            {
                if (gameReadyAPI_focus_switch)
                {
                    if (GameReadyAPI_IsOnGameplay)
                    {
                        YG2.GameplayStop();
                    }

                    gameReadyAPI_focus_switch = false;
                }
            }

            yield return (null);
        }                
    }

    private void Awake()
    {
        SingleOnScene = this;
    }

    private void Start()
    {
        if (ControlPers_BuildSettings.SingleOnScene.BuildRuntimeType_Current == ControlPers_BuildSettings.BuildRuntimeType.web_yandexGames_desktop
        || ControlPers_BuildSettings.SingleOnScene.BuildRuntimeType_Current ==  ControlPers_BuildSettings.BuildRuntimeType.web_yandexGames_mobile_android)
        {
            var _routine = Update_Coroutine();
            StartCoroutine(_routine);
        }
    }
}
