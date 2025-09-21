using UnityEngine;

public class ControlPers_AudioMixer : MonoBehaviour
{
    public static ControlPers_AudioMixer SingleOnScene { get; private set; }

    public void Pause()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.audioSource.Pause();
        ControlPers_AudioMixer_Music.SingleOnScene.audioSource.Pause();
    }

    public void UnPause()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.audioSource.UnPause();
        ControlPers_AudioMixer_Music.SingleOnScene.audioSource.UnPause();
    }

    public void Stop()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.Stop();
        ControlPers_AudioMixer_Music.SingleOnScene.Stop();
    }

    private void Awake()
    {
        SingleOnScene = this;     
    }
}
