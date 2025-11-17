using UnityEngine;

public class World_General_MovingBackground_Billboard : MonoBehaviour
{
    public static World_General_MovingBackground_Billboard SingleOnScene { get; private set; }

    public bool Active { get; set; }
    public bool Move { get; set; }    
    
    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = value;
        }
    }
    
    private float Speed { get; set; }

    private const float WIDTH = 12.8f;

    private Vector3 position_init;

    public Color Color
    {
        get
        {
            return spriteRenderer.color;
        }
        set
        {
            spriteRenderer.color = value;
        }
    }

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite[]   spriteRenderer_sprites_normal;
    private int                         spriteRenderer_sprites_normal_index_prev;

    [SerializeField] private Sprite[]   spriteRenderer_sprites_redness;
    private int                         spriteRenderer_sprites_redness_index_prev = int.MaxValue;
    private float                       spriteRenderer_sprites_redness_chance = 1.0f;
    private float                       spriteRenderer_sprites_redness_chance_step = 0.25f;

    public void ImageRefresh(float _redness = 0)
    {
        switch (_redness)
        {
            default :
                var _index = Random.Range(0, spriteRenderer_sprites_normal.Length);

                //Гарантируем, что не будет 2 одинаковых билборда подряд
                while (_index == spriteRenderer_sprites_normal_index_prev
                    && spriteRenderer_sprites_normal.Length > 1)
                {
                    _index = Random.Range(0, spriteRenderer_sprites_normal.Length);
                }

                spriteRenderer.sprite = spriteRenderer_sprites_normal[_index];
                spriteRenderer_sprites_normal_index_prev = _index;
            break;
            case > 0 :
                var _rednessSprite = Random.Range(0.0f, 1.0f) < spriteRenderer_sprites_redness_chance;

                if (_rednessSprite)
                {
                    _index = Random.Range(0, spriteRenderer_sprites_redness.Length);

                    //Гарантируем, что не будет 2 одинаковых билборда подряд
                    while (_index == spriteRenderer_sprites_redness_index_prev
                        && spriteRenderer_sprites_redness.Length > 1)
                    {
                        _index = Random.Range(0, spriteRenderer_sprites_redness.Length);
                    }

                    spriteRenderer.sprite = spriteRenderer_sprites_redness[_index];
                    spriteRenderer_sprites_redness_index_prev = _index;

                    spriteRenderer_sprites_redness_chance = 0;
                }
                else
                {
                    _index = Random.Range(0, spriteRenderer_sprites_normal.Length - 1);

                    //Гарантируем, что не будет 2 одинаковых билборда подряд
                    while (_index == spriteRenderer_sprites_normal_index_prev
                        && spriteRenderer_sprites_normal.Length > 1)
                    {
                        _index = Random.Range(0, spriteRenderer_sprites_normal.Length - 1);
                    }

                    spriteRenderer.sprite = spriteRenderer_sprites_normal[_index];
                    spriteRenderer_sprites_normal_index_prev = _index;

                    spriteRenderer_sprites_redness_chance += spriteRenderer_sprites_redness_chance_step;
                }
            break;
        }
    }

    private void Awake()
    {
        SingleOnScene = this;
        
        Active = false;
        Move = false;
        Speed = 437.5f;

        position_init = transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();

        var _index = Random.Range(0, spriteRenderer_sprites_normal.Length - 1);
        spriteRenderer.sprite = spriteRenderer_sprites_normal[_index];
        spriteRenderer_sprites_normal_index_prev = _index;
    }

    private void Update()
    {
        if (Active
        && Move)
        {
            transform.position += Vector3.left * Speed * World_General_MovingBackground_Entity.SingleOnScene.SpeedScale * Time.deltaTime;

            //Город занимается СамоВоспроизводством
            if (transform.position.x <= -WIDTH)
            {
                transform.position = position_init;
                Move = false;
            }
        }
    }
}
