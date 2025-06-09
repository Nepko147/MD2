using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Coins_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Coins_Entity SingleOnScene { get; private set; }

    private float alpha = 0;
    private float alpha_step;

    private enum State
    {
        idle,
        show,
        hide
    }
    private State currentState;

    public void Show(float _time)
    {
        alpha_step = (1f - alpha) / _time;
        currentState = State.show;
    }

    public void Hide(float _time)
    {
        alpha_step = alpha / _time;
        currentState = State.hide;
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    private void Start()
    {
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Coins_Text.SingleOnScene.Alpha_Set(alpha);
        AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Coins_Sprite.SingleOnScene.Alpha_Set(alpha);
    }

    private void Update()
    {
        switch (currentState) 
        {
            case State.show:
                alpha += alpha_step * Time.deltaTime;

                if (alpha >= 1f)
                {
                    alpha = 1f;
                    currentState = State.idle;
                }

                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Coins_Text.SingleOnScene.Alpha_Set(alpha);
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Coins_Sprite.SingleOnScene.Alpha_Set(alpha);
            break;
            
            case State.hide:
                alpha -= alpha_step * Time.deltaTime;

                if (alpha <= 0)
                {
                    alpha = 0;
                    currentState = State.idle;
                }

                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Coins_Text.SingleOnScene.Alpha_Set(alpha);
                AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Coins_Sprite.SingleOnScene.Alpha_Set(alpha);
            break;
        }
    }
}
