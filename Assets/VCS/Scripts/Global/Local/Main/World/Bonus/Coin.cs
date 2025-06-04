using UnityEngine;

public class World_Bonus_Coin : MonoBehaviour
{
    public bool Active { get; set; }    

    [SerializeField] private float speed = 10f;
    [SerializeField] private World_PopUp popUp;
    [SerializeField] private AudioClip sound;
    private new Animator animation;
    private BoxCollider2D boxCollider;
    private bool visible = true;

    public void MakeInvisible()
    {
        visible = false;
        var _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    public void MakeVisible()
    {
        visible = true;
        var _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = true; 
    }

    private void Awake()
    {
        Active = true;

        animation = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {         
        if (Active)
        {
            animation.speed = 1;
            transform.position += Vector3.left * speed * World_MovingBackground_Entity.SingleOnScene.SpeedScale; 
            
            if (boxCollider.bounds.Intersects(World_Player.SingleOnScene.Player_BoxCollider.bounds)
                && visible)
            {
                Active = false;

                ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound);

                World_Player.SingleOnScene.TakeCoin();

                var _popUp = Instantiate(popUp, transform.position, transform.rotation);
                _popUp.Display_AsCoin();

                Destroy(gameObject);
            }

            //”ничтожаем объект, когда он уходит за пределы экрана
            if (transform.position.x <= -10.0f)
            {
                Destroy(gameObject);
            }            
        } 
        else 
        {
            animation.speed = 0;
        }        
    }
 }
