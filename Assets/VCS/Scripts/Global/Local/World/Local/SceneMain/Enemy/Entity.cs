using UnityEngine;
using Utils;

public class World_Local_SceneMain_Enemy_Entity : MonoBehaviour
{
    public bool Active { get; set; }

    private SpriteRenderer spriteRenderer;
    [SerializeField] Texture2D normalMap;

    private bool isDamaged = false;

    private PolygonCollider2D collider2d;

    private const float SPEED = 450f;

    private const float DESTROYPOSITION_X = -5.0f; //Позиция уничтожения объекта за пределами экрана

    private void Awake()
    {
        Active = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, normalMap);

        collider2d = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {     
        if (Active)
        {
            transform.position += Vector3.left * SPEED * World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale * Time.deltaTime;

            if (!isDamaged
            && !World_Local_SceneMain_Player_Entity.SingleOnScene.Invul
            && collider2d.bounds.Intersects(World_Local_SceneMain_Player_Entity.SingleOnScene.Collision_Hit.bounds))
            {
                isDamaged = true;
                World_Local_SceneMain_Player_Entity.SingleOnScene.Up_Lose();
            }

            if (transform.position.x <= DESTROYPOSITION_X)
            {
                Destroy(gameObject);
            }           
        }        
    }
 }
