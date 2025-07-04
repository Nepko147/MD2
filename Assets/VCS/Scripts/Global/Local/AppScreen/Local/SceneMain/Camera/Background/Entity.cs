using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AppScreen_Local_SceneMain_Camera_Background_Entity : AppScrren_General_Camera_Parent
{
    public static AppScreen_Local_SceneMain_Camera_Background_Entity SingleOnScene { get; private set; }

    public bool Active { get; set; }

    private PostProcessVolume   postProcess_volume;

    private ChromaticAberration postProcess_profile_chromaticAberration;
    private  bool               postProcess_profile_chromaticAberration_intensity_change = false;
    [SerializeField] float      postProcess_profile_chromaticAberration_speed = 0.0001f;
    [SerializeField] float      postProcess_profile_chromaticAberration_max = 0.4f;

    public void ChromaticAberrationEnable(bool _state)
    {
        postProcess_profile_chromaticAberration_intensity_change = _state;
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Active = false;

        postProcess_volume = GetComponent<PostProcessVolume>();
        postProcess_volume.profile.TryGetSettings(out postProcess_profile_chromaticAberration);
    }

    protected override void Update()
    {
        base.Update();

        if (Active)
        {
            if (postProcess_profile_chromaticAberration_intensity_change)
            {
                postProcess_profile_chromaticAberration.intensity.value += postProcess_profile_chromaticAberration_speed;
                postProcess_profile_chromaticAberration.intensity.value = Mathf.Clamp(postProcess_profile_chromaticAberration.intensity.value, 0, postProcess_profile_chromaticAberration_max);
            }
            else
            {
                postProcess_profile_chromaticAberration.intensity.value = 0;
            }
        }            
    }
}
