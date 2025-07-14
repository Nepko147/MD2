using UnityEngine;

public class World_Local_SceneMain_Bonus_Parent : MonoBehaviour
{
    public bool Active { get; set; }

    protected float speed = 8f;

    [SerializeField] protected World_Local_SceneMain_PopUp popUp;

    [SerializeField] protected AudioClip sound;

    protected new Animator animation;

    protected BoxCollider2D boxCollider;

    protected const float DESTROYPOSITION_X = -5.0f; //Позиция уничтожения объекта за пределами экрана

    private void Awake()
    {
        Active = true;

        animation = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();
    }
}
