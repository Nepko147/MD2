using UnityEngine;
using UnityEngine.Audio;

public class ControlPers_AudioMixer_Sounds : MonoBehaviour
{
    public static ControlPers_AudioMixer_Sounds SingleOnScene { get; private set; }

    public AudioSource audioSource { get; private set; }

    [SerializeField] private AudioMixerGroup audioMixerGroup;
    private const string AUDIOMIXERGROUP_VOLUME_NAME = "Sound_Volume";
    private const float AUDIOMIXERGROUP_VOLUME_RANGE = -24f;

    public void Play(AudioClip _sound)
    {
        audioSource.PlayOneShot(_sound);
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Volume_Set(float _soundValue)
    {
        audioMixerGroup.audioMixer.SetFloat(AUDIOMIXERGROUP_VOLUME_NAME, AUDIOMIXERGROUP_VOLUME_RANGE * (1f - _soundValue));
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
        if (ControlPers_DataHandler.SingleOnScene.SettingsData_SoundValue == 0)
        {
            Volume_Mute();
        }
        else
        {
            Volume_Set(ControlPers_DataHandler.SingleOnScene.SettingsData_SoundValue);
        }
    }
}
