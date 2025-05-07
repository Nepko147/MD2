using UnityEngine;

public class World_Bonus_Coin : MonoBehaviour
{
    public bool Active { get; set; }

    [SerializeField] private float              bonus_speed;
    [SerializeField] private bool               bonus_distortionEffect;
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
        bonus_animation.SetInteger(BONUS_ANIMATION_TYPE, 0);

        bonus_boxCollider = GetComponent<BoxCollider2D>();           
    }

    private void FixedUpdate()
    {         
        if (Active)
        {
            bonus_animation.speed = 1;
            transform.position += Vector3.left * bonus_speed * World_Background_Entity.Singletone.SpeedScale; 
            
            if (bonus_boxCollider.bounds.Intersects(World_Player.Singletone.GetComponent<BoxCollider2D>().bounds))
            {
                ControlPers_AudioManager.Singletone.PlaySound(bonus_sound);
                World_Player.Singletone.TakeCoin();
                bonus_popUpString.DisplayPopUp(bonus_popUpString_text, transform.position.x, transform.position.y);
                
                if (bonus_distortionEffect)
                {
                    Universal_DistortionDynamic _distortion = Universal_DistortionDynamic.Singletone;
                    Vector2 _screenPosition = _distortion.GetComponent<Camera>().WorldToScreenPoint(transform.position);
                    _distortion.NormalMapMix_Material_Active = true;
                    _distortion.NormalMapMix_Material_Pos_X = _screenPosition.x / Screen.width;
                    _distortion.NormalMapMix_Material_Pos_Y = _screenPosition.y / Screen.height;
                }

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
            bonus_animation.speed = 0;
        }        
    }
 }
