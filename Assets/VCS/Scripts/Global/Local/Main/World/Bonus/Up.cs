using UnityEngine;

public class World_Bonus_Up : MonoBehaviour
{
    public bool Active { get; set; }

    [SerializeField] private float          bonus_speed;
    [SerializeField] private AudioClip      bonus_sound;

    [SerializeField] private World_PopUp    bonus_popUpString;

    Animator bonus_animation;
    const string BONUS_ANIMATION_TYPE = "type";
    BoxCollider2D bonus_boxCollider;

    private void Awake()
    {
        Active = true;

        bonus_animation = GetComponent<Animator>();
        bonus_boxCollider = GetComponent<BoxCollider2D>();           
    }

    private void FixedUpdate()
    {         
        if (Active)
        {
            bonus_animation.speed = 1;
            transform.position += Vector3.left * bonus_speed * World_MovingBackground_Entity.SingleOnScene.SpeedScale; 
            
            if (bonus_boxCollider.bounds.Intersects(World_Player.SingleOnScene.GetComponent<BoxCollider2D>().bounds))
            {
                Active = false;

                ControlPers_AudioManager.SingleOnScene.PlaySound(bonus_sound);
                World_Player.SingleOnScene.TakeUp();

                var _popUp = Instantiate(bonus_popUpString, transform.position, transform.rotation);
                _popUp.Display_AsUp();

                Destroy(gameObject);
            }

            //���������� ������, ����� �� ������ �� ������� ������
            if (transform.position.x <= -10.0f)
            {
                Destroy(gameObject);
            }            
        } 
        else 
        {
            bonus_animation.speed = 0;
        }        
    }
 }
