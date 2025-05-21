using UnityEngine;

public class World_Bonus_Up : MonoBehaviour
{
    public bool Active { get; set; }

    [SerializeField] private float              bonus_speed;
    [SerializeField] private AudioClip          bonus_sound;

    [SerializeField] private World_BonusString  bonus_popUpString;
    [SerializeField] private string             bonus_popUpString_text;

    Animator                                    bonus_animation;
    const string                                BONUS_ANIMATION_TYPE = "type";
    BoxCollider2D                               bonus_boxCollider;  

    private void Awake()
    {
        Active = true;

        bonus_animation = GetComponent<Animator>();
        bonus_animation.SetInteger(BONUS_ANIMATION_TYPE, 1);

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
                ControlPers_AudioManager.SingleOnScene.PlaySound(bonus_sound); //∆дЄм по€влени€ јудиоћиксера
                World_Player.SingleOnScene.TakeDamage(-1);
                bonus_popUpString.DisplayPopUp(bonus_popUpString_text, transform.position.x, transform.position.y);
                Universal_DistortionDynamic.SingleOnScene.WorldDistortion(transform.position);
                Destroy(gameObject);
            }

            //”ничтожаем объект, когда он уходит за пределы экрана
            if (gameObject.transform.position.x <= -10.0f)
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
