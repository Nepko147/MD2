using UnityEngine;

public class World_Local_SceneMain_DriftSection_Enity : MonoBehaviour
{
    public static World_Local_SceneMain_DriftSection_Enity SingleOnScene { get; private set; }

    public bool Active { get; set; }

    public bool Move { get; set; }

    private void Awake()
    {
        SingleOnScene = this;

        Active = true;
    }

    private void FixedUpdate()
    {
        if (Active
        && Move)
        {
            transform.position += Vector3.left * World_Local_SceneMain_MovingBackground_Road.SPEED * World_Local_SceneMain_MovingBackground_Entity.SingleOnScene.SpeedScale;
        }
    }
}
