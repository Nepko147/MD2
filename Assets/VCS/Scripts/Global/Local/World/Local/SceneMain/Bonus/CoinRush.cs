using UnityEngine;

public class World_Local_SceneMain_Bonus_CoinRush : MonoBehaviour
{
    public bool Active { get; set; }

    private float speed = 8f;
    [SerializeField] private World_Local_SceneMain_PopUp popUp;
    [SerializeField] private AudioClip sound;
    private new Animator animation;
    private BoxCollider2D boxCollider;

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
            transform.position += Vector3.left * speed * World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale; 
            
            if (boxCollider.bounds.Intersects(World_Local_SceneMain_Player.SingleOnScene.Player_BoxCollider.bounds))
            {
                Active = false;

                ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound);

                World_Local_SceneMain_BonusSpawner.SingleOnScene.CoinRush = true;
                World_Local_SceneMain_BonusSpawner.SingleOnScene.BonusSpawn_Delay_Reset();
                AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.CoinRush(transform.position);

                var _popUp = Instantiate(popUp, transform.position, transform.rotation, transform.parent);
                _popUp.Display_AsCoinRush();
                
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
