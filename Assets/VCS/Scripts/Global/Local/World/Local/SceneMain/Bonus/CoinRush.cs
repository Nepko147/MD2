using UnityEngine;

public class World_Local_SceneMain_Bonus_CoinRush : World_Local_SceneMain_Bonus_Parent
{
    private void Update()
    {         
        if (Active)
        {
            animation.speed = 1;
            transform.position += Vector3.left * speed * World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale * Time.deltaTime; 
            
            if (boxCollider.bounds.Intersects(World_Local_SceneMain_Player.SingleOnScene.BoxCollider.bounds))
            {
                Active = false;

                ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound);

                World_Local_SceneMain_BonusSpawner.SingleOnScene.CoinRush = true;
                World_Local_SceneMain_BonusSpawner.SingleOnScene.BonusSpawn_Delay_Reset();
                AppScreen_Local_SceneMain_Camera_World_CameraDistortion.SingleOnScene.Material_Overlay_NormalMap_CoinRush_Start(transform.position);

                var _popUp = Instantiate(popUp, transform.position, transform.rotation, transform.parent);
                _popUp.Display_AsCoinRush();
                
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
