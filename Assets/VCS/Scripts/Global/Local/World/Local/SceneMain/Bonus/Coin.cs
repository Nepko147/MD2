using UnityEngine;

public class World_Local_SceneMain_Bonus_Coin : World_Local_SceneMain_Bonus_Parent
{
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

    private void Update()
    {         
        if (Active)
        {
            animation.speed = 1;
            transform.position += Vector3.left * speed * World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale * Time.deltaTime; 
            
            if (boxCollider.bounds.Intersects(World_Local_SceneMain_Player.SingleOnScene.BoxCollider.bounds)
            && visible)
            {
                Active = false;

                ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound);

                World_Local_SceneMain_Player.SingleOnScene.TakeCoin();

                var _popUp = Instantiate(popUp, transform.position, transform.rotation, transform.parent);
                _popUp.Display_AsCoin();

                Destroy(gameObject);
            }

            if (transform.position.x <= DESTROYPOSITION_X)
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
