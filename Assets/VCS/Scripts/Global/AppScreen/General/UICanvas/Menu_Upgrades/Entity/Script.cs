using UnityEngine;
using System.Collections;

public class AppScreen_UICanvas_Menu_Upgrades_Entity : AppScreen_Local_SceneMenu_UICanvas_Menu_General_Entity_Parent
{
    public static AppScreen_UICanvas_Menu_Upgrades_Entity SingleOnScene { get; private set; }

    private CanvasGroup canvasGroup;
    private float canvasGroup_deltaApha = 1.0f;

    private enum menu_upgrades_state
    {
        onDisplay,
        hidden,
        idle,
        size
    }

    menu_upgrades_state menu_upgrades_state_currnet;

    public void Show(float _delay)
    {
        IEnumerator _coroutine(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            menu_upgrades_state_currnet = menu_upgrades_state.onDisplay;
        }

        var _routine = _coroutine(_delay);
        StartCoroutine(_routine);
    }

    public void Show_Instantly()
    {
        Show(0);
        canvasGroup.alpha = 1f;
    }

    public void Hide(float _delay)
    {
        IEnumerator _coroutine(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            menu_upgrades_state_currnet = menu_upgrades_state.hidden;
        }

        var _routine = _coroutine(_delay);
        StartCoroutine(_routine);
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0.0f;

        menu_upgrades_state_currnet = menu_upgrades_state.idle;
    }

    private void Update()
    {
        switch (menu_upgrades_state_currnet)
        {
            case menu_upgrades_state.onDisplay:

                if (canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha += canvasGroup_deltaApha * Time.deltaTime;
                }
                else
                {
                    menu_upgrades_state_currnet = menu_upgrades_state.idle;
                    canvasGroup.alpha = 1; // Гарантируем полное появление
                }

                break;

            case menu_upgrades_state.hidden:

                if (canvasGroup.alpha > 0)
                {
                    canvasGroup.alpha -= canvasGroup_deltaApha * Time.deltaTime;
                }
                else
                {
                    menu_upgrades_state_currnet = menu_upgrades_state.idle;
                    canvasGroup.alpha = 0; // Гарантируем полное исчезновение
                }

                break;
        }
    }
}
