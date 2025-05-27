using UnityEngine;

public class ControlPers_AudioMixer_Sounds : MonoBehaviour
{
    public static ControlPers_AudioMixer_Sounds SingleOnScene { get; private set; }

    public AudioSource AudioSource { get; private set; }

    public void Play(AudioClip _sound)
    {
        AudioSource.PlayOneShot(_sound);
    }

    public void Stop()
    {
        AudioSource.Stop();
    }

    private void Awake()
    {
        SingleOnScene = this;

        AudioSource = GetComponent<AudioSource>();
    }
}
