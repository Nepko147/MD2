using UnityEngine;

public class ControlPers_Entity : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
