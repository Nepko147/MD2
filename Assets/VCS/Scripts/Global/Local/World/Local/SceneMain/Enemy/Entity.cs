using UnityEngine;
using Utils;

public class World_Local_SceneMain_Enemy_Entity : MonoBehaviour
{
    public bool Active { get; set; }

    private SpriteRenderer spriteRenderer;
    [SerializeField] Texture2D normalMap;

    private float speed = 8f;

    private AudioSource audioSource;

    private bool isDamaged = false;

    private PolygonCollider2D collider2d;

    const float DESTROYPOSITION_X = -5.0f; //Позиция уничтожения объекта за пределами экрана

    private void Awake()
    {
        Active = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetTexture(Constants.MATERIAL_2D_BUMP_U_BUMPMAP, normalMap);

        audioSource = GetComponent<AudioSource>();

        collider2d = GetComponent<PolygonCollider2D>();
    }

    private void FixedUpdate()
    {     
        if (Active)
        {
            transform.position += Vector3.left * speed * World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale;

            //Проверка на контакт с игроком
            if (!isDamaged
            && !World_Local_SceneMain_Player.SingleOnScene.Invul
            && collider2d.bounds.Intersects(World_Local_SceneMain_Player.SingleOnScene.BoxCollider.bounds))
            {
                audioSource.Play();
                isDamaged = true;
                World_Local_SceneMain_Player.SingleOnScene.Up_Lose();
            }

            if (transform.position.x <= DESTROYPOSITION_X)
            {
                Destroy(gameObject);
            }           
        }        
    }
 }
