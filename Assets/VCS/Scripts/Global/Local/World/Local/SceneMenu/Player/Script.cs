using UnityEngine;

public class World_Local_SceneMenu_Player : MonoBehaviour
{
    private SpriteRenderer player_spriteRenderer;
    [SerializeField] Texture2D player_normalMap_stright;

    private void Awake()
    {
        player_spriteRenderer = GetComponent<SpriteRenderer>();
        player_spriteRenderer.material.SetTexture(Constants.MATERIAL_2D_BUMP_U_BUMPMAP, player_normalMap_stright);
    }
}
