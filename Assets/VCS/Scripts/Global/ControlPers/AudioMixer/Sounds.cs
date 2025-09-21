using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

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

    private float pitch_step_toZero = 0.02f;
    private float pitch_step_toNormal = 0.2f;

    private IEnumerator Pitch_Coroutine_ToZero()
    {
        while (true)
        {
            audioSource.pitch -= pitch_step_toZero;

            if (audioSource.pitch > 0)
            {
                yield return null;
            }
            else
            {
                audioSource.pitch = 0;
                break;
            }
        }
    }

    private IEnumerator Pitch_Coroutine_ToNormal()
    {
        while (true)
        {
            audioSource.pitch += pitch_step_toNormal * 10;

            if (audioSource.pitch < 1)
            {
                yield return null;
            }
            else
            {
                audioSource.pitch = 1;
                break;
            }
        }
    }

    private IEnumerator pitch_coroutine_current;

    public void Pitch_ToZero()
    {
        StopCoroutine(pitch_coroutine_current);
        pitch_coroutine_current = Pitch_Coroutine_ToZero();
        StartCoroutine(pitch_coroutine_current);
    }

    public void Pitch_ToNormal()
    {
        StopCoroutine(pitch_coroutine_current);
        pitch_coroutine_current = Pitch_Coroutine_ToNormal();
        StartCoroutine(pitch_coroutine_current);
    }

    private void Awake()
    {
        SingleOnScene = this;

        audioSource = GetComponent<AudioSource>();        ;
        pitch_coroutine_current = Pitch_Coroutine_ToNormal();
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
