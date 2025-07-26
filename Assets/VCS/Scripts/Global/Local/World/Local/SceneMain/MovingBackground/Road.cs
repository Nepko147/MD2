using UnityEngine;
using Utils;

public class World_Local_SceneMain_MovingBackground_Road : World_Local_SceneMain_MovingBackground_Parent
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Texture2D normalMap;

    public const float SPEED = 10f;

    protected override void Awake()
    {
        base.Awake();

        Speed = SPEED;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetTexture(Constants.MATERIAL_2D_BUMP_U_BUMPMAP, normalMap);
    }
}
