using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }    
    public AudioSource source;
    
    private void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();        
    }
    private void Start()
    {
        float volume = (((float)SaveLoader.Instance.Load("Settings.db")) / 10);
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

