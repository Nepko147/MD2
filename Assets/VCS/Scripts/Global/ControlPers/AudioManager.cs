using UnityEngine;

public class ControlPers_AudioManager : MonoBehaviour
{
    public static ControlPers_AudioManager Singletone { get; private set; }    
    public AudioSource source;
    
    private void Awake()
    {
        Singletone = this;
        source = GetComponent<AudioSource>();        
    }
    private void Start()
    {
        float volume = (((float)ControlPers_SaveLoader.Singletone.Load("volume")) / 10);
        source.volume = volume;
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }   

    public void Pause()
    {        
        source.Pause();
    }

    public void UnPause()
    {
        source.UnPause();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void SetVolume(float _volume)
    {
        source.volume = _volume;
    }
}

