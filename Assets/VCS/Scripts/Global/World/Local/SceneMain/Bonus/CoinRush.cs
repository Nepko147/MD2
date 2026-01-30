using UnityEngine;

public class World_Local_SceneMain_Bonus_CoinRush : World_Local_SceneMain_Bonus_Parent
{
    protected override void Update()
    {         
        base.Update();
        
        if (Active)
        {
            if (boxCollider.bounds.Intersects(World_Local_SceneMain_Player_Entity.SingleOnScene.Collision_Bonus.bounds))
            {
                ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound);
                World_Local_SceneMain_BonusSpawner.SingleOnScene.CoinRush = true;
                World_Local_SceneMain_Player_Entity.SingleOnScene.Invul_Activate(2.4f, Color.blue);
                AppScreen_General_MainCameraCarrier_MainCamera_World.SingleOnScene.Material_Overlay_NormalMap_CoinRush_Start(transform.position);

                var _popUp = Instantiate(popUp, transform.position, transform.rotation, transform.parent);
                _popUp.Display_AsCoinRush();
                
                Destroy(gameObject);
            }         
        }    
    }
}
