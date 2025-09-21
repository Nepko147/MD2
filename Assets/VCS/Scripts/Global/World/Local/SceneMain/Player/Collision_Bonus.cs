using UnityEngine;

public class World_Local_SceneMain_Player_Collision_Bonus : MonoBehaviour
{
    public static World_Local_SceneMain_Player_Collision_Bonus SingleOnScene { get; private set; }

    private BoxCollider2D collision;

    public void Collision_Param_Set(float _ang, float _size_x, float _size_y)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _ang);
        collision.size = new Vector2(_size_x, _size_y);
    }

    private void Awake()
    {
        SingleOnScene = this;

        collision = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        World_Local_SceneMain_Player_Entity.SingleOnScene.Collision_Bonus = collision;
    }
}
