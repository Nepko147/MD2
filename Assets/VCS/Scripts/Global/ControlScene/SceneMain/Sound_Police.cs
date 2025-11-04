using UnityEngine;

public class ControlScene_SceneMain_Sound_Police : MonoBehaviour
{
    public static ControlScene_SceneMain_Sound_Police SingleOnScene { get; private set; }

    [SerializeField] public AudioSource audioSource;

    private void Awake()
    {
        SingleOnScene = this;
    }
}
