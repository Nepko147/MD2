using UnityEngine;

public class World_Local_SceneMain_Player_Collision_Hit : MonoBehaviour
{
    private void Start()
    {
        World_Local_SceneMain_Player_Entity.SingleOnScene.Collision_Hit = GetComponent<CircleCollider2D>();
    }
}
