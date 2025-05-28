using UnityEngine;

public class ControlPers_AudioMixer : MonoBehaviour
{
    public static ControlPers_AudioMixer SingleOnScene { get; private set; }

    public void Pause()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.AudioSource.Pause();
        ControlPers_AudioMixer_Music.SingleOnScene.AudioSource.Pause();
    }

    public void UnPause()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.AudioSource.UnPause();
        ControlPers_AudioMixer_Music.SingleOnScene.AudioSource.UnPause();
    }

    public void Stop()
    {
        ControlPers_AudioMixer_Sounds.SingleOnScene.Stop();
        ControlPers_AudioMixer_Music.SingleOnScene.Stop();
    }

    public void SetVolume(float _volume)
    {
        //Настройка громкости через условный слайдер на спрайте громкости
    }

    private void Awake()
    {
        SingleOnScene = this;     
    }

    private void Start()
    {
        //float _volume = (float)ControlPers_DataHandler.SingleOnScene.Settings_Volume_Get() / 10;
    }
}
