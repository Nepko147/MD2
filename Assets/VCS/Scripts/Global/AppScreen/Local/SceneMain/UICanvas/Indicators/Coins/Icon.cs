using UnityEngine;
using UnityEngine.UI;

public class AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Icon : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Indicators_Coins_Icon SingleOnScene { get; private set; }
    
    Animator animator;
    Image image;

    public void Pause()
    {
        animator.speed = 0;
    }

    public void UnPause()
    {
        animator.speed = 1;
    }

    public void SetAlpha(float _newAlpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, _newAlpha);
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
    }
}
