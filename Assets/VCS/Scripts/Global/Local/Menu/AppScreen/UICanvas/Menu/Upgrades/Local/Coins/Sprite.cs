using UnityEngine;

public class AppScreen_UICanvas_Menu_Upgrades_Coins_Sprite : AppScreen_UICanvas_Parent
{
    public static AppScreen_UICanvas_Menu_Upgrades_Coins_Sprite SingleOnScene { get; private set; }

    private SpriteRenderer spriteRenderer;
    private Color spriteRenderer_color = Color.white;

    public void Alpha_Set(float _alpha)
    {
        spriteRenderer_color.a = _alpha;
        spriteRenderer.color = spriteRenderer_color;
    }

    protected override void Awake()
    {
        SingleOnScene = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
