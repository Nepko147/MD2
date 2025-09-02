using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AppScreen_General_Camera_World_Entity : AppScrren_General_Camera_Parent
{
    public static AppScreen_General_Camera_World_Entity SingleOnScene { get; private set; }

    private Camera camera_world;
    public float Cmera_World_FieldOfView 
    { get
        {
            return camera_world.fieldOfView;
        } 
        set
        {
            camera_world.fieldOfView = value;
        }
    }

    private PostProcessVolume   postProcess_volume;
    private DepthOfField        postProcess_profile_depthOfField;
    private const float         POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MIN = 1;
    private const float         POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MAX = 3;
    float                       postProcess_profile_depthOfField_aperture_step = 0;
    float                       postProcess_profile_depthOfField_aperture_duration;
    bool                        postProcess_profile_depthOfField_aperture_change = false;

    /// <summary>
    /// <para> _value - Нормализованная величина Блюра (0..1) </para>
    /// <para> _duration - Кол-во секунд, за которое величина Блюра приёдт к _value </para>
    /// </summary>
    public void Blur(float _value, float _duration)
    {
        postProcess_profile_depthOfField_aperture_change = true;
        _value = Mathf.Clamp(_value, 0, 1);
        postProcess_profile_depthOfField_aperture_duration = _duration;
        var _aperture_value = POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MIN + (POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MAX - POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MIN) * (1 - _value);
        postProcess_profile_depthOfField_aperture_step = (_aperture_value - postProcess_profile_depthOfField.aperture.value) / _duration;
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        camera_world = GetComponent<Camera>();
        postProcess_volume = GetComponent<PostProcessVolume>();
        postProcess_volume.profile.TryGetSettings(out postProcess_profile_depthOfField);
    }

    protected override void Update()
    {
        base.Update();

        if (postProcess_profile_depthOfField_aperture_change)
        {
            postProcess_profile_depthOfField_aperture_duration -= Time.deltaTime;            
            postProcess_profile_depthOfField.aperture.value += postProcess_profile_depthOfField_aperture_step * Time.deltaTime;
            postProcess_profile_depthOfField.aperture.value = Mathf.Clamp(postProcess_profile_depthOfField.aperture.value, POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MIN, POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MAX);
            
            if (postProcess_profile_depthOfField_aperture_duration <= 0)
            {
                postProcess_profile_depthOfField_aperture_change = false;
            }
        }
    }
}
