using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class ControlPers_AudioMixer_Music : MonoBehaviour
{
    #region General

    public static ControlPers_AudioMixer_Music SingleOnScene { get; private set; }

    private AudioSource audioSource;

    [SerializeField] private AudioMixerGroup audioMixerGroup;
    private const string AUDIOMIXERGROUP_VOLUME_NAME = "Music_Volume";
    private const float AUDIOMIXERGROUP_VOLUME_RANGE = -24f;

    public void Play(AudioClip _music, bool _looped)
    {
        audioSource.clip = _music;
        audioSource.loop = _looped;
        audioSource.pitch = 1f;
        audioSource.Play();
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void UnPause()
    {
        audioSource.UnPause();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    #endregion

    #region Volume

    private float volume_settings;

    public void Volume_Settings_Set(float _volume)
    {
        volume_settings = _volume;
        volume_mute = false;
        Volume_Refresh();
    }

    private void Volume_Refresh()
    {
        if (!volume_mute)
        {
            audioMixerGroup.audioMixer.SetFloat(AUDIOMIXERGROUP_VOLUME_NAME, AUDIOMIXERGROUP_VOLUME_RANGE * (1f - volume_settings * volume_scale));
        }
    }

    private float volume_scale = 1f;
    private const float VOLUME_SCALE_SPEED = 1f;
    private IEnumerator Volume_Scale_Coroutine_Current;

    public void Volume_Scale_Set(float _scale)
    {
        volume_scale = _scale;
        Volume_Refresh();
    }

    private IEnumerator Volume_Scale_ToZero_Coroutine()
    {
        while (true)
        {
            Volume_Scale_Set(volume_scale - VOLUME_SCALE_SPEED * Time.deltaTime);
            
            if (volume_scale > 0)
            {
                yield return null;
            }
            else
            {
                Volume_Scale_Set(0);
                break;
            }
        }
    }

    private IEnumerator Volume_Scale_ToOne_Coroutine()
    {
        while (true)
        {
            Volume_Scale_Set(volume_scale + VOLUME_SCALE_SPEED * Time.deltaTime);
            
            if (volume_scale < 1)
            {
                yield return null;
            }
            else
            {
                Volume_Scale_Set(1);
                break;
            }
        }
    }

    public void Volume_Scale_ToZero()
    {
        StopCoroutine(Volume_Scale_Coroutine_Current);
        Volume_Scale_Coroutine_Current = Volume_Scale_ToZero_Coroutine();
        StartCoroutine(Volume_Scale_Coroutine_Current);
    }

    public void Volume_Scale_ToOne()
    {
        StopCoroutine(Volume_Scale_Coroutine_Current);
        Volume_Scale_Coroutine_Current = Volume_Scale_ToOne_Coroutine();
        StartCoroutine(Volume_Scale_Coroutine_Current);
    }

    private bool volume_mute;

    public void Volume_Mute()
    {
        audioMixerGroup.audioMixer.SetFloat(AUDIOMIXERGROUP_VOLUME_NAME, -80f);
        volume_mute = true;
    }

    #endregion

    #region Pitch

    private const float PITCH_TIME = 1.5f;
    private IEnumerator Pitch_Coroutine_Current;

    private IEnumerator Pitch_Coroutine_ToZero()
    {
        while (true)
        {
            audioSource.pitch -= PITCH_TIME * Time.deltaTime;

            if (audioSource.pitch > 0)
            {
                yield return null;
            }
            else
            {
                audioSource.pitch = 0;
                audioSource.Pause();
                break;
            }
        }
    }
        
    private IEnumerator Pitch_Coroutine_ToNormal()
    {
        while (true)
        {
            audioSource.pitch += PITCH_TIME * Time.deltaTime;

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

    public void Pitch_ToZero()
    {
        StopCoroutine(Pitch_Coroutine_Current);
        Pitch_Coroutine_Current = Pitch_Coroutine_ToZero();
        StartCoroutine(Pitch_Coroutine_Current);
    }

    public void Pitch_ToNormal()
    {
        audioSource.UnPause();

        StopCoroutine(Pitch_Coroutine_Current);
        Pitch_Coroutine_Current = Pitch_Coroutine_ToNormal();
        StartCoroutine(Pitch_Coroutine_Current);
    }

    #endregion

    private void Awake()
    {
        SingleOnScene = this;

        audioSource = GetComponent<AudioSource>();

        Volume_Scale_Coroutine_Current = Pitch_Coroutine_ToNormal();

        Pitch_Coroutine_Current = Pitch_Coroutine_ToNormal();
    }

    private void Start()
    {
        if (ControlPers_DataHandler.SingleOnScene.SettingsData_MusicValue == 0)
        {
            Volume_Mute();
        }
        else
        {
            Volume_Settings_Set(ControlPers_DataHandler.SingleOnScene.SettingsData_MusicValue);
        }
    }
}
