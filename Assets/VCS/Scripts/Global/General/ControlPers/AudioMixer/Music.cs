using UnityEngine;

public class ControlPers_AudioMixer_Music : MonoBehaviour
{
    public static ControlPers_AudioMixer_Music SingleOnScene { get; private set; }

    public AudioSource AudioSource { get; private set; }

    public void Play(AudioClip _music)
    {
        AudioSource.PlayOneShot(_music);
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
