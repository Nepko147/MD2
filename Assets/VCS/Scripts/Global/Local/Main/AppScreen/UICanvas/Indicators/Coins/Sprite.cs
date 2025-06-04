using UnityEngine;

public class AppScreen_UICanvas_Indicators_Coins_Sprite : MonoBehaviour
{
    public static AppScreen_UICanvas_Indicators_Coins_Sprite SingleOnScene { get; private set; }
    
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

    private void Awake()
    {
        SingleOnScene = this;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
