using UnityEngine;
using Utils;

public class World_Local_SceneMain_MovingBackground_Road : World_Local_SceneMain_MovingBackground_Parent
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Texture2D normalMap;

    public const float SPEED = 500f;

    protected override void Awake()
    {
        base.Awake();

        Speed = SPEED;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, normalMap);
    }
}
