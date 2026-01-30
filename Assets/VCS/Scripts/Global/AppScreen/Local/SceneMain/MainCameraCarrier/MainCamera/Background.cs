using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AppScreen_Local_SceneMain_MainCameraCarrier_MainCamera_Background : AppScreen_General_MainCameraCarrier_MainCamera_Parent
{
    #region General

    public static AppScreen_Local_SceneMain_MainCameraCarrier_MainCamera_Background SingleOnScene { get; private set; }
    
    public bool Active { get; set; }

    private Vector3 position_init;

    #endregion

    #region Post-Process

    private PostProcessLayer postProcess_layer;

    private PostProcessVolume   postProcess_volume;

    private ChromaticAberration postProcess_profile_chromaticAberration;
    private  bool               postProcess_profile_chromaticAberration_started = false;
    [SerializeField] float      postProcess_profile_chromaticAberration_speed = 0.0001f;
    [SerializeField] float      postProcess_profile_chromaticAberration_max = 0.4f;

    public void PostProcess_Profile_ChromaticAberration_Start()
    {
        postProcess_profile_chromaticAberration_started = true;
    }
    public void PostProcess_Profile_ChromaticAberration_Discard()
    {
        postProcess_profile_chromaticAberration_started = false;
        postProcess_profile_chromaticAberration.intensity.value = 0;
    }

    #endregion

    #region Shake

    private bool shake_on = false;
    private const float SHAKE_DELAY_INIT = 0.016f;
    private float shake_delay_current = SHAKE_DELAY_INIT;
    private const float SHAKE_STEPS_INIT = 20f;
    private float shake_steps_current = SHAKE_STEPS_INIT;
    private const float SHAKE_OFS_X = 0.4f;
    private const float SHAKE_OFS_Y = 0.1f;
    private Vector3 shake_ofs_vec3 = Vector3.zero;

    public void Shake()
    {
        position_init = transform.localPosition;
        shake_on = true;
    }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Active = false;

        postProcess_layer = GetComponent<PostProcessLayer>();
        postProcess_volume = GetComponent<PostProcessVolume>();
        postProcess_volume.profile.TryGetSettings(out postProcess_profile_chromaticAberration);
    }

    private void Update()
    {
        #region General

        camera_component.fieldOfView = AppScreen_General_MainCameraCarrier_MainCamera_World.SingleOnScene.FieldOfView_Current;

        #endregion

        #region Post-Process

        if (camera_component.rect.x == 0 //Костыль для фикса бага компонента Post-process Layer, который вызывает неправильное отображение камеры
        && camera_component.rect.y == 0)
        {
            if (Active
            && postProcess_profile_chromaticAberration_started)
            {
                if (!postProcess_layer.enabled)
                {
                    postProcess_layer.enabled = true;
                }

                postProcess_profile_chromaticAberration.intensity.value += postProcess_profile_chromaticAberration_speed;
                postProcess_profile_chromaticAberration.intensity.value = Mathf.Clamp(postProcess_profile_chromaticAberration.intensity.value, 0, postProcess_profile_chromaticAberration_max);
            }
        }
        else
        {
            if (postProcess_layer.enabled)
            {
               postProcess_layer.enabled = false;
            }
        }

        #endregion

        #region Shake

        if (shake_on)
        {
            shake_delay_current -= Time.deltaTime;

            if (shake_delay_current <= 0)
            {
                shake_delay_current = SHAKE_DELAY_INIT;

                var _shake_ofs_scale = shake_steps_current / SHAKE_STEPS_INIT;
                shake_ofs_vec3.x = position_init.x + Random.Range(-SHAKE_OFS_X, SHAKE_OFS_X) * _shake_ofs_scale;
                shake_ofs_vec3.y = position_init.y + Random.Range(-SHAKE_OFS_Y, SHAKE_OFS_Y) * _shake_ofs_scale;
                transform.localPosition = shake_ofs_vec3;

                --shake_steps_current;

                if (shake_steps_current == 0)
                {
                    shake_on = false;
                    shake_steps_current = SHAKE_STEPS_INIT;
                    transform.localPosition = position_init;
                }
            }
        }

        #endregion
    }
}
