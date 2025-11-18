using UnityEngine;

public class ControlScene_SceneMain_Sound_Police : MonoBehaviour
{
    public static ControlScene_SceneMain_Sound_Police SingleOnScene { get; private set; }

    [SerializeField] private AudioSource audioSource;

    public void Play()
    {
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void UnPause()
    {
        audioSource.UnPause();
    }

    private void Awake()
    {
        SingleOnScene = this;
    }
}
