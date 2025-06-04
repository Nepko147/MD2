using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

public class ControlPers_AudioMixer_Music : MonoBehaviour
{
    public static ControlPers_AudioMixer_Music SingleOnScene { get; private set; }

    public AudioSource audioSource { get; private set; }

    [SerializeField] private AudioMixerGroup audioMixerGroup;
    private const string AUDIOMIXERGROUP_VOLUME_NAME = "Music_Volume";
    private const float AUDIOMIXERGROUP_VOLUME_RANGE = -24f;

    public void Play(AudioClip _music)
    {
        audioSource.clip = _music;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Volume_Set(float _musicValue)
    {
        audioMixerGroup.audioMixer.SetFloat(AUDIOMIXERGROUP_VOLUME_NAME, AUDIOMIXERGROUP_VOLUME_RANGE * (1f - _musicValue));
    }

    public void Volume_Mute()
    {
        audioMixerGroup.audioMixer.SetFloat(AUDIOMIXERGROUP_VOLUME_NAME, -80f);
    }

    private void Awake()
    {
        SingleOnScene = this;

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (ControlPers_DataHandler.SingleOnScene.SettingsData_MusicValue == 0)
        {
            Volume_Mute();
        }
        else
        {
            Volume_Set(ControlPers_DataHandler.SingleOnScene.SettingsData_MusicValue);
        }
    }
}
