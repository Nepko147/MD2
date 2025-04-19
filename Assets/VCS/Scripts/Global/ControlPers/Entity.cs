using UnityEngine;

public class ControlPers_Entity : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
