using UnityEngine;

public class World_Local_SceneMain_Bonus_Coin : World_Local_SceneMain_Bonus_Parent
{
    private bool visible = true;

    private SpriteRenderer spriteRenderer;

    public void MakeInvisible()
    {
        visible = false;
        spriteRenderer.enabled = false;
    }

    public void MakeVisible()
    {
        visible = true;
        spriteRenderer.enabled = true; 
    }

    protected override void Awake()
    {
        base.Awake();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();

        if (Active)
        {
            if (boxCollider.bounds.Intersects(World_Local_SceneMain_Player_Entity.SingleOnScene.Collision_Bonus.bounds)
            && visible)
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
