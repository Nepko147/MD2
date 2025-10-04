using UnityEngine;

public class World_Local_SceneMain_DriftSection_Transition_Barrier : MonoBehaviour
{
    public static World_Local_SceneMain_DriftSection_Transition_Barrier SingleOnScene { get; private set; }

    BoxCollider2D boxCollider;

    public bool Active 
    { 
        get { return (boxCollider.enabled); }
        set { boxCollider.enabled = value; }
    }

    private void Awake()
    {
        SingleOnScene = this;

        boxCollider = GetComponent<BoxCollider2D>();

        Active = false;
    }
}
