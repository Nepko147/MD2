using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Entity_Opening : MonoBehaviour
{
    bool stage_pressAnyKey = true;
    bool stage_titleAnimation = true;

    [SerializeField] private AudioClip audio_crickets;


    private void Start()
    {
        ControlPers_AudioMixer_Music.SingleOnScene.Play(audio_crickets);

        var _soundValue = ControlPers_DataHandler.SingleOnScene.Settings_SoundValue;

        if (_soundValue == 0)
        {
            ControlPers_AudioMixer_Sounds.SingleOnScene.Volume_Mute();
        }
        else
        {
            ControlPers_AudioMixer_Sounds.SingleOnScene.Volume_Set(_soundValue);
        }

        var _musicValue = ControlPers_DataHandler.SingleOnScene.Settings_MusicValue;

        if (_musicValue == 0)
        {
            ControlPers_AudioMixer_Music.SingleOnScene.Volume_Mute();
        }
        else
        {
            ControlPers_AudioMixer_Music.SingleOnScene.Volume_Set(_musicValue);
        }
    }

    private void Update()
    {
        if (stage_pressAnyKey)
        {
            ControlPers_FogHandler.Move();

            if (Input.anyKey)
            {
                AppScreen_UICanvas_Car.SingleOnScene.Activate();
                World_UI_TitleAnimation.SingleOnScene.PlayAnimation();

                stage_pressAnyKey = false;
                stage_titleAnimation = true;                
            }
        }

        if (stage_titleAnimation)
        {
            ControlPers_FogHandler.Move();

            if (AppScreen_UICanvas_Car.SingleOnScene.Done
                && World_UI_TitleAnimation.SingleOnScene.Done)
            {
                ControlPers_FogHandler.Color_Save();
                SceneManager.LoadScene(ControlPers_Entity.SCENEINDEX_MENU);
            }
        }
    }
}
