using UnityEngine;

public class World_Local_SceneMain_Bonus_Up : World_Local_SceneMain_Bonus_Parent
{
    protected override void Update()
    {         
        base.Update();

        if (Active)
        {
            if (boxCollider.bounds.Intersects(World_Local_SceneMain_Player_Entity.SingleOnScene.Collision_Bonus.bounds))
            {
                ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound);

                World_Local_SceneMain_Player_Entity.SingleOnScene.Up_Take();

                var _popUp = Instantiate(popUp, transform.position, transform.rotation, transform.parent);
                _popUp.Display_AsUp();

                Destroy(gameObject);
            }         
        }       
    }
 }
