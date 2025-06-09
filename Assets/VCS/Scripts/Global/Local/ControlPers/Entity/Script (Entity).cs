using UnityEngine;

public class ControlPers_Entity : MonoBehaviour
{
    public const int SCENEINDEX_OPENING = 0;
    public const int SCENEINDEX_MENU = 1;
    public const int SCENEINDEX_MAIN = 2;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
    }
}
