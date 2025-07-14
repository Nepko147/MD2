using UnityEngine;

public class World_Local_SceneMain_Bonus_Up : World_Local_SceneMain_Bonus_Parent
{
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

                World_Local_SceneMain_Player.SingleOnScene.TakeUp();

                var _popUp = Instantiate(popUp, transform.position, transform.rotation, transform.parent);
                _popUp.Display_AsUp();

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
