using UnityEngine;

public class World_Entity : MonoBehaviour
{
    public static World_Entity SingleOnScene;

    private void Awake()
    {
        SingleOnScene = this;
    }
}
