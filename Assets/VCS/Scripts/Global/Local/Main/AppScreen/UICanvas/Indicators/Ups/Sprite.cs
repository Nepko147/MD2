using UnityEngine;

public class AppScreen_UICanvas_Indicators_Ups_Sprite : MonoBehaviour
{
    public static AppScreen_UICanvas_Indicators_Ups_Sprite SingleOnScene { get; private set; }

    Animator animator;

    public void Pause()
    {
        animator.speed = 0;
    }

    public void UnPause()
    {
        animator.speed = 1;
    }

    private void Awake()
    {
        SingleOnScene = this;

        animator = GetComponent<Animator>();
    }
}
