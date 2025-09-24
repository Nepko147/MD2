using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AppScreen_Local_SceneMain_Camera_Background_Entity : AppScrren_General_Camera_Parent
{
    public static AppScreen_Local_SceneMain_Camera_Background_Entity SingleOnScene { get; private set; }
    
    public bool Active { get; set; }

    private Vector3 position_init;

    private Camera camera_background; 

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

        camera_background = GetComponent<Camera>();
        postProcess_volume = GetComponent<PostProcessVolume>();
        postProcess_volume.profile.TryGetSettings(out postProcess_profile_chromaticAberration);
    }

    protected override void Update()
    {
        base.Update();

        camera_background.fieldOfView = AppScreen_General_Camera_World_Entity.SingleOnScene.FieldOfView_Current; // Гарантируем, одинаковое поле зрение у камер

        if (Active
        && postProcess_profile_chromaticAberration_started)
        {
            postProcess_profile_chromaticAberration.intensity.value += postProcess_profile_chromaticAberration_speed;
            postProcess_profile_chromaticAberration.intensity.value = Mathf.Clamp(postProcess_profile_chromaticAberration.intensity.value, 0, postProcess_profile_chromaticAberration_max);
        }

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
