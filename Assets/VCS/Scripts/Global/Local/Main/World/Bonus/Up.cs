using UnityEngine;

public class World_Bonus_Up : MonoBehaviour
{
    public bool Active { get; set; }

    [SerializeField] private float          speed;
    [SerializeField] private AudioClip      sound;

    [SerializeField] private World_PopUp    popUpString;

    new Animator animation;
    BoxCollider2D boxCollider;

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
            
            if (boxCollider.bounds.Intersects(World_Player.SingleOnScene.GetComponent<BoxCollider2D>().bounds))
            {
                Active = false;

                ControlPers_AudioManager.SingleOnScene.PlaySound(sound);
                World_Player.SingleOnScene.TakeUp();

                var _popUp = Instantiate(popUpString, transform.position, transform.rotation);
                _popUp.Display_AsUp();

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
