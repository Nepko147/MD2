using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AppScreen_Local_SceneMain_Camera_Background_Entity : AppScrren_General_Camera_Parent
{
    public static AppScreen_Local_SceneMain_Camera_Background_Entity SingleOnScene { get; private set; }
    
    public bool Active { get; set; }

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

        camera_background.fieldOfView = AppScreen_General_Camera_World_Entity.SingleOnScene.Cmera_World_FieldOfView; // Гарантируем, одинаковое поле зрение у камер

        if (Active
        && postProcess_profile_chromaticAberration_started)
        {
            postProcess_profile_chromaticAberration.intensity.value += postProcess_profile_chromaticAberration_speed;
            postProcess_profile_chromaticAberration.intensity.value = Mathf.Clamp(postProcess_profile_chromaticAberration.intensity.value, 0, postProcess_profile_chromaticAberration_max);
        }            
    }
}
