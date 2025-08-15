using UnityEngine;

public class World_Local_SceneMain_DriftSection_Coin : MonoBehaviour
{
    private bool active = true;
    public bool Active 
    { 
        get 
        { 
            return (active); 
        }
        set 
        { 
            active = value;

            if (value)
            {
                animator.speed = 1;
            }
            else
            {
                animator.speed = 0;
            }
        }
    }

    [SerializeField] private World_Local_SceneMain_PopUp popUp;

    [SerializeField] private AudioClip sound;

    private Animator animator;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (active)
        {
            if (boxCollider.bounds.Intersects(World_Local_SceneMain_Player_Entity.SingleOnScene.Collision_Bonus.bounds))
            {
                ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound);

                World_Local_SceneMain_Player_Entity.SingleOnScene.TakeCoin();

                var _popUp = Instantiate(popUp, transform.position, transform.rotation, transform.parent);
                _popUp.Display_AsCoin();

                Destroy(gameObject);
            }
        }
    }
}
