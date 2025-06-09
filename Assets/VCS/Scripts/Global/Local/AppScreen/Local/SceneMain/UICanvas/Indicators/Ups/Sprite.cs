using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Sprite : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Indicators_Ups_Sprite SingleOnScene { get; private set; }

    Animator animator;
    SpriteRenderer spriteRenderer;

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
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, _newAlpha);
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
