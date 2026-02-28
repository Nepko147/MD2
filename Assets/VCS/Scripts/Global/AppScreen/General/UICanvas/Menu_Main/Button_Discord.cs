using UnityEngine;
using System.Collections;

public class AppScreen_General_UICanvas_Menu_Main_Button_Discord : AppScreen_General_UICanvas_Button_Parent
{
    public static AppScreen_General_UICanvas_Menu_Main_Button_Discord SingleOnScene { get; private set; }

    private void ImageRefresh()
    {
        var _idle = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_discord_idle);
        var _pointed = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_discord_pointed);
        var _pressed = ControlPers_LanguageHandler_Entity.SingleOnScene.Sprite_Get(ControlPers_LanguageHandler_Entity.Sprite_Key.button_discord_pressed);

        Image_LanguageRefresh(_idle, _pointed, _pressed);
    }

    public void Hide(float _time)
    {
        if (gameObject.activeInHierarchy)
        {
            IEnumerator _Coroutine(float _time)
            {
                while (true)
                {
                    var _col = image.color;
                    _col.a -= Time.deltaTime / _time;
                
                    if (_col.a > 0)
                    {
                        image.color = _col;

                        yield return (null);
                    }
                    else
                    {
                        _col.a = 0;
                        image.color = _col;

                        break;
                    }
                }

                image.enabled = false;
            }

            var _routine = _Coroutine(_time);
            StartCoroutine(_routine);
        }
    }

    public void Show(float _time)
    {
        if (gameObject.activeInHierarchy)
        {
            image.enabled = true;

            IEnumerator _Coroutine(float _time)
            {
                while (true)
                {
                    var _col = image.color;
                    _col.a += Time.deltaTime / _time;
                
                    if (_col.a < 1)
                    {
                        image.color = _col;

                        yield return (null);
                    }
                    else
                    {
                        _col.a = 1;
                        image.color = _col;

                        break;
                    }
                }
            }

            var _routine = _Coroutine(_time);
            StartCoroutine(_routine);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    protected override void Start()
    {
        base.Start();
        
        ImageRefresh();
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate += ImageRefresh;

        if (ControlPers_BuildSettings.SingleOnScene.BuildRuntimeType_Current == ControlPers_BuildSettings.BuildRuntimeType.web_yandexGames_desktop
        || ControlPers_BuildSettings.SingleOnScene.BuildRuntimeType_Current == ControlPers_BuildSettings.BuildRuntimeType.web_yandexGames_mobile_android)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler_Entity.SingleOnScene.GameLanguage_OnUpdate -= ImageRefresh;
    }
}
