using UnityEngine;

public class ControlPers_AudioMixer : MonoBehaviour
{
    public static ControlPers_AudioMixer SingleOnScene { get; private set; }

    public void Pause()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.Pause();
        ControlPers_AudioMixer_Music.SingleOnScene.Pause();
    }

    public void UnPause()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.UnPause();
        ControlPers_AudioMixer_Music.SingleOnScene.UnPause();
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
