using UnityEngine;
using Utils;

public class World_Local_SceneMain_DriftSection_BushTeleport_Parent : MonoBehaviour
{
    [SerializeField] private Texture2D normalMap;

    protected CircleCollider2D collision;

    protected virtual bool Teleport_Condition()
    {
        if (!World_Local_SceneMain_Player_Entity.SingleOnScene.Invul_Active
        && World_Local_SceneMain_Player_Entity.SingleOnScene.Collision_Hit.bounds.Intersects(collision.bounds))
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }

    private void Awake()
    {
        GetComponent<SpriteRenderer>().material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, normalMap);

        collision = GetComponent<CircleCollider2D>();
    }
}
