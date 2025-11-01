using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AppScreen_Local_SceneMain_UICanvas_Tutorial_Entity : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private GameObject virtualStick;

    public void Text_LanguageRefresh()
    {
        switch (ControlPers_BuildSettings.SingleOnScene.PlatformType_Current)
        {
            case ControlPers_BuildSettings.PlatformType.windows:
                text.text = ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.tutorial_desktop);
            break;
            case ControlPers_BuildSettings.PlatformType.web_yandexGames_desktop:
                text.text = ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.tutorial_desktop);
            break;
            case ControlPers_BuildSettings.PlatformType.web_yandexGames_mobile_android:
                text.text = ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.tutorial_mobile);
            break;
        }
    }

    private void Start()
    {
        text.font.material.mainTexture.filterMode = FilterMode.Point;
        Text_LanguageRefresh();
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate += Text_LanguageRefresh;
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            ControlPers_DataHandler.SingleOnScene.ProgressData_Tutorial = false;

            virtualStick.SetActive(false);
            
            IEnumerator _hider()
            {
                var _canvasGroup = GetComponent<CanvasGroup>();
                var _aplha_step = 1;

                while (true)
                {
                    _canvasGroup.alpha -= _aplha_step * Time.deltaTime;
                    if (_canvasGroup.alpha > 0)
                    {
                        yield return null;
                    }
                    else
                    {
                        Destroy(gameObject);
                        break;
                    }
                }
            }

            var _routine = _hider();
            StartCoroutine(_routine);
        }
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate -= Text_LanguageRefresh;
    }
}
