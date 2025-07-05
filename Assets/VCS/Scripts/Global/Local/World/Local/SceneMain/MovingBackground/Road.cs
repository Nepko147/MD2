using UnityEngine;

public class World_Local_SceneMain_MovingBackground_Road : World_Local_SceneMain_MovingBackground_Parent
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Texture2D normalMap;

    protected override void Awake()
    {
        base.Awake();
        Speed = 10;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetTexture("_BumpMap", normalMap);
    }
}
