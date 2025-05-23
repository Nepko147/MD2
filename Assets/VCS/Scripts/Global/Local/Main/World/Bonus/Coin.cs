using System;
using UnityEngine;
using UnityEngine.Audio;

public class World_Bonus_Coin : MonoBehaviour
{
    public bool Active { get; set; }

    [SerializeField] private float speed = 10f;
    [SerializeField] private World_PopUp popUp;
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
            transform.position += Vector3.left * speed * World_MovingBackground_Entity.SingleOnScene.SpeedScale; 
            
            if (boxCollider.bounds.Intersects(World_Player.SingleOnScene.Player_BoxCollider.bounds))
            {
                Active = false;

                ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound);

                World_Player.SingleOnScene.TakeCoin();

                var _popUp = Instantiate(popUp, transform.position, transform.rotation);
                _popUp.Display_AsCoin();

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
            animation.speed = 0;
        }        
    }
 }
