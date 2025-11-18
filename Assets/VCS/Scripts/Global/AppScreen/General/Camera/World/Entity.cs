using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AppScreen_General_Camera_World_Entity : AppScrren_General_Camera_Parent
{
    #region General

    public static AppScreen_General_Camera_World_Entity SingleOnScene { get; private set; }

    [SerializeField] private Material passthrough_material;

    public float FieldOfView_Current
    {
        get
        {
            return (camera_component.fieldOfView);
        } 
        set
        {
            camera_component.fieldOfView = value;
        }
    }

    private float fieldOfView_init;

    private Vector3 position_init;

    #endregion

    #region Blur

    private PostProcessVolume   blur_postProcess_volume;
    private DepthOfField        blur_postProcess_profile_depthOfField;
    private const float         BLUR_POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MIN = 1;
    private const float         BLUR_POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MAX = 3;
    private float               blur_postProcess_profile_depthOfField_aperture_step = 0;
    private float               blur_postProcess_profile_depthOfField_aperture_duration;
    private bool                blur_postProcess_profile_depthOfField_aperture_change = false;

    /// <summary>
    /// <para> _value - Нормализованная величина Блюра (0..1) </para>
    /// <para> _duration - Кол-во секунд, за которое величина Блюра приёдт к _value </para>
    /// </summary>
    public void Blur(float _value, float _duration)
    {
        blur_postProcess_profile_depthOfField_aperture_change = true;
        _value = Mathf.Clamp(_value, 0, 1);
        blur_postProcess_profile_depthOfField_aperture_duration = _duration;
        var _aperture_value = BLUR_POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MIN + (BLUR_POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MAX - BLUR_POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MIN) * (1 - _value);
        blur_postProcess_profile_depthOfField_aperture_step = (_aperture_value - blur_postProcess_profile_depthOfField.aperture.value) / _duration;
    }

    #endregion

    #region Zoom

    public bool Zoom_Active { get; set; }

    private enum Zoom_State
    {
        pulse,
        toSpeedUp,
        speedUp,
        toPulse
    }

    private Zoom_State zoom_state_current = Zoom_State.pulse;

    public void Zoom_State_ToSpeedUp()
    {
        zoom_state_current = Zoom_State.toSpeedUp;
    }

    public void Zoom_State_ToPulse()
    {
        zoom_state_current = Zoom_State.toPulse;
    }

    private bool zoom_pulse_state = false;
    private const float ZOOM_PULSE_FOW_OFS = 1f;
    private float zoom_pulse_fow_min;
    private const float ZOOM_PULSE_FOW_SPEED = 0.1f;
    private const float ZOOM_PULSE_DELAY_INIT = 1f;    
    private float zoom_pulse_delay_current = ZOOM_PULSE_DELAY_INIT;

    private const float ZOOM_TOSPEEDUP_FOW_SPEED = 1f;

    private const float ZOOM_SPEEDUP_FOW_OFS = 2f;

    private const float ZOOM_TOPULSE_FOW_SPEED = 1f;

    #endregion

    #region Shake

    public bool Shake_Active { get; set; }
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
        shake_on = true;
    }

    #endregion

    #region Distortion

    private bool distortion_started = false;

    [SerializeField] private Material distortion_material;

    private const string DISTORTION_MATERIAL_U_MAIN_NORMALMAP = "u_main_normalMap";
    private const string DISTORTION_MATERIAL_U_OVERLAY_NORMALMAP = "u_overlay_normalMap";
    private const string DISTORTION_MATERIAL_U_OVERLAY_SCREENPOS = "u_overlay_screenPos";
    private const string DISTORTION_MATERIAL_U_OVERLAY_SCALE = "u_overlay_scale";
    private const string DISTORTION_MATERIAL_U_OVERLAY_ALPHA = "u_overlay_aplha";

    private Texture2D distortion_material_main_normalMap_clear;
    [SerializeField] private Texture2D distortion_material_main_normalMap_gameOver;

    public void Material_Main_NormalMap_GameOver_Apply()
    {
        distortion_started = true;
        distortion_material.SetTexture(DISTORTION_MATERIAL_U_MAIN_NORMALMAP, distortion_material_main_normalMap_gameOver);
    }

    public bool Distortion_Material_Overlay_Active { get; set; }
    [SerializeField] private Texture2D distortion_material_overlay_normalMap_coinRush;
    private const float DISTORTION_MATERIAL_OVERLAY_NORMALMAP_COINRUSH_VISUALDIAMETERINPIXELS = 120f;
    public bool Distortion_Material_Overlay_NormalMap_CoinRush_Active { get; set; }
    public Vector3 Distortion_Material_Overlay_NormalMap_CoinRush_WorldPos { get; private set; }
    private Vector4 distortion_material_overlay_normalMap_coinRush_screenPos_normalized = Vector2.zero;
    private float distortion_material_overlay_normalMap_coinRush_time = 0;
    private const float MATERIAL_OVERLAY_NORMALMAP_COINRUSH_TIME_MAX = 2f;
    private float distortion_material_overlay_normalMap_coinRush_scale = 0;
    private Vector4 distortion_material_overlay_normalMap_coinRush_scale_withAspect = Vector4.zero;
    private const float DISTORTION_MATERIAL_OVERLAY_NORMALMAP_COINRUSH_SCALE_MAX = 8f;
    private const float DISTORTION_MATERIAL_OVERLAY_NORMALMAP_COINRUSH_SCALE_STEP = DISTORTION_MATERIAL_OVERLAY_NORMALMAP_COINRUSH_SCALE_MAX / MATERIAL_OVERLAY_NORMALMAP_COINRUSH_TIME_MAX;

    public void Material_Overlay_NormalMap_CoinRush_Start(Vector3 _position)
    {
        distortion_started = true;
        Distortion_Material_Overlay_NormalMap_CoinRush_Active = true;
        Distortion_Material_Overlay_NormalMap_CoinRush_WorldPos = _position;
        distortion_material_overlay_normalMap_coinRush_time = 0;
        distortion_material_overlay_normalMap_coinRush_scale = 0;
    }

    public float Material_Overlay_NormalMap_CoinRush_Distance_Get()
    {
        var _pos_screen_source = camera_component.WorldToScreenPoint(Distortion_Material_Overlay_NormalMap_CoinRush_WorldPos);
        var _pos_screen_diameter = new Vector3(_pos_screen_source.x + DISTORTION_MATERIAL_OVERLAY_NORMALMAP_COINRUSH_VISUALDIAMETERINPIXELS * distortion_material_overlay_normalMap_coinRush_scale, _pos_screen_source.y, _pos_screen_source.z);
        var _pos_world_source = camera_component.ScreenToWorldPoint(_pos_screen_source);
        var _pos_world_diameter = camera_component.ScreenToWorldPoint(_pos_screen_diameter);
        var _visualDiameterInWorld = Mathf.Abs(_pos_world_diameter.x - _pos_world_source.x);
        
        return (_visualDiameterInWorld);
    }

    #endregion

    #region ZoomBlur

    public bool ZoomBlur_Active { get; set; } = true;
    
    [SerializeField] private Material zoomBlur_material;
    private const string ZOOMBLUR_MATERIAL_U_INTENSITY = "u_intensity";

    private float zoomBlur_intensity_current = 0;
    private const float ZOOMBLUR_INTENSITY_MAX = 0.5f;
    private const float ZOOMBLUR_INTENSITY_SPEED = 1f;
    public float ZoomBlur_Intensity_Scale { get; set; } = 1f;

    private enum ZoomBlur_Stage
    {
        on,
        off
    }

    private ZoomBlur_Stage zoomBlur_stage_current = ZoomBlur_Stage.on;

    private bool zoomBlur_started = false;

    public void ZoomBlur_Start()
    {
        zoomBlur_intensity_current = 0;
        zoomBlur_started = true;
        zoomBlur_stage_current = ZoomBlur_Stage.on;
    }
    
    #endregion

    protected override void Awake()
    {
        base.Awake();

        #region General

        SingleOnScene = this;

        position_init = transform.localPosition;

        #endregion

        #region Blur

        blur_postProcess_volume = GetComponent<PostProcessVolume>();
        blur_postProcess_volume.profile.TryGetSettings(out blur_postProcess_profile_depthOfField);

        #endregion

        #region Zoom

        Zoom_Active = false;

        fieldOfView_init = camera_component.fieldOfView;
        zoom_pulse_fow_min = fieldOfView_init - ZOOM_PULSE_FOW_OFS;

        #endregion

        #region Shake

        Shake_Active = true;

        #endregion

        #region Distortion

        Distortion_Material_Overlay_Active = true;
        Distortion_Material_Overlay_NormalMap_CoinRush_Active = false;

        #endregion
    }

    private void Start()
    {
        #region Distortion

        distortion_material_main_normalMap_clear = new Texture2D(1, 1, TextureFormat.RGBA32, false);
        var _color = new Color(0.5f, 0.5f, 1f);
        distortion_material_main_normalMap_clear.SetPixel(0, 0, _color);
        distortion_material_main_normalMap_clear.Apply();
        distortion_material.SetTexture(DISTORTION_MATERIAL_U_MAIN_NORMALMAP, distortion_material_main_normalMap_clear);

        distortion_material.SetTexture(DISTORTION_MATERIAL_U_OVERLAY_NORMALMAP, distortion_material_overlay_normalMap_coinRush);
        distortion_material.SetFloat(DISTORTION_MATERIAL_U_OVERLAY_ALPHA, 0);

        #endregion
    }

    protected override void Update()
    {
        base.Update();

        #region Blur

        if (blur_postProcess_profile_depthOfField_aperture_change)
        {
            blur_postProcess_profile_depthOfField_aperture_duration -= Time.deltaTime;            
            blur_postProcess_profile_depthOfField.aperture.value += blur_postProcess_profile_depthOfField_aperture_step * Time.deltaTime;
            blur_postProcess_profile_depthOfField.aperture.value = Mathf.Clamp(blur_postProcess_profile_depthOfField.aperture.value, BLUR_POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MIN, BLUR_POSTPROCESS_PROFILE_DEPTHOFFIELD_APERTURE_MAX);
            
            if (blur_postProcess_profile_depthOfField_aperture_duration <= 0)
            {
                blur_postProcess_profile_depthOfField_aperture_change = false;
            }
        }

        #endregion

        #region Zoom

        if (Zoom_Active)
        {
            switch (zoom_state_current)
            {
                case Zoom_State.pulse:
                    if (!zoom_pulse_state)
                    {
                        if (zoom_pulse_delay_current > 0)
                        {
                            zoom_pulse_delay_current -= Time.deltaTime;
                        }
                        else
                        {
                            FieldOfView_Current -= ZOOM_PULSE_FOW_SPEED * Time.deltaTime;

                            if (FieldOfView_Current <= zoom_pulse_fow_min)
                            {
                                zoom_pulse_delay_current = ZOOM_PULSE_DELAY_INIT;
                                zoom_pulse_state = true;
                            }
                        }
                    }
                    else
                    {
                        if (zoom_pulse_delay_current > 0)
                        {
                            zoom_pulse_delay_current -= Time.deltaTime;
                        }
                        else
                        {
                            FieldOfView_Current += ZOOM_PULSE_FOW_SPEED * Time.deltaTime;

                            if (FieldOfView_Current >= fieldOfView_init)
                            {
                                zoom_pulse_delay_current = ZOOM_PULSE_DELAY_INIT;
                                zoom_pulse_state = false;
                            }
                        }
                    }
                break;

                case Zoom_State.toSpeedUp:
                    FieldOfView_Current += ZOOM_TOSPEEDUP_FOW_SPEED * Time.deltaTime;

                    if (FieldOfView_Current >= fieldOfView_init + ZOOM_SPEEDUP_FOW_OFS * World_Local_SceneMain_Player_Entity.SingleOnScene.Moving_Drift_Speed_Current_Normalized_Get())
                    {
                        zoom_state_current = Zoom_State.speedUp;
                    }
                break;

                case Zoom_State.speedUp:
                    FieldOfView_Current = fieldOfView_init + ZOOM_SPEEDUP_FOW_OFS * World_Local_SceneMain_Player_Entity.SingleOnScene.Moving_Drift_Speed_Current_Normalized_Get();
                break;

                case Zoom_State.toPulse:
                    FieldOfView_Current -= ZOOM_TOPULSE_FOW_SPEED * Time.deltaTime;

                    if (FieldOfView_Current <= fieldOfView_init)
                    {
                        FieldOfView_Current = fieldOfView_init;
                        zoom_state_current = Zoom_State.pulse;
                    }
                break;
            }
        }

        #endregion

        #region Shake

        if (Shake_Active
        && shake_on)
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

        #region Distortion

        if (Distortion_Material_Overlay_Active
        && Distortion_Material_Overlay_NormalMap_CoinRush_Active)
        {
            distortion_material_overlay_normalMap_coinRush_time += Time.deltaTime;

            Vector2 _screen_pos = camera_component.WorldToScreenPoint(Distortion_Material_Overlay_NormalMap_CoinRush_WorldPos);
            distortion_material_overlay_normalMap_coinRush_screenPos_normalized.x = _screen_pos.x / Screen.width;
            distortion_material_overlay_normalMap_coinRush_screenPos_normalized.y = _screen_pos.y / Screen.height;
            distortion_material.SetVector(DISTORTION_MATERIAL_U_OVERLAY_SCREENPOS, distortion_material_overlay_normalMap_coinRush_screenPos_normalized);

            distortion_material_overlay_normalMap_coinRush_scale += DISTORTION_MATERIAL_OVERLAY_NORMALMAP_COINRUSH_SCALE_STEP * Time.deltaTime;
            distortion_material_overlay_normalMap_coinRush_scale_withAspect.x = distortion_material_overlay_normalMap_coinRush_scale * distortion_material_overlay_normalMap_coinRush.width / Screen.width;
            distortion_material_overlay_normalMap_coinRush_scale_withAspect.y = distortion_material_overlay_normalMap_coinRush_scale * distortion_material_overlay_normalMap_coinRush.height / Screen.height;
            distortion_material.SetVector(DISTORTION_MATERIAL_U_OVERLAY_SCALE, distortion_material_overlay_normalMap_coinRush_scale_withAspect);

            var _alpha = Mathf.Clamp(1f - distortion_material_overlay_normalMap_coinRush_scale / DISTORTION_MATERIAL_OVERLAY_NORMALMAP_COINRUSH_SCALE_MAX, 0, 1f);
            distortion_material.SetFloat(DISTORTION_MATERIAL_U_OVERLAY_ALPHA, _alpha);

            if (distortion_material_overlay_normalMap_coinRush_time >= MATERIAL_OVERLAY_NORMALMAP_COINRUSH_TIME_MAX)
            {
                distortion_started = false;
                distortion_material.SetFloat(DISTORTION_MATERIAL_U_OVERLAY_ALPHA, 0);
                Distortion_Material_Overlay_NormalMap_CoinRush_Active = false;
            }
        }

        #endregion

        #region ZoomBlur

        if (ZoomBlur_Active
        && zoomBlur_started)
        {
            switch (zoomBlur_stage_current)
            {
                case ZoomBlur_Stage.on:
                    zoomBlur_intensity_current += ZOOMBLUR_INTENSITY_SPEED * ZoomBlur_Intensity_Scale * Time.deltaTime;

                    if (zoomBlur_intensity_current >= ZOOMBLUR_INTENSITY_MAX * ZoomBlur_Intensity_Scale)
                    {
                        zoomBlur_intensity_current = ZOOMBLUR_INTENSITY_MAX * ZoomBlur_Intensity_Scale;
                        zoomBlur_stage_current = ZoomBlur_Stage.off;
                    }
                break;

                case ZoomBlur_Stage.off:
                    zoomBlur_intensity_current -= ZOOMBLUR_INTENSITY_SPEED * ZoomBlur_Intensity_Scale * Time.deltaTime;

                    if (zoomBlur_intensity_current <= 0)
                    {
                        zoomBlur_intensity_current = 0;
                        zoomBlur_started = false;
                    }
                break;
            }

            zoomBlur_material.SetFloat(ZOOMBLUR_MATERIAL_U_INTENSITY, zoomBlur_intensity_current);
        }

        #endregion
    }

    private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        if (distortion_started)
        {
            Graphics.Blit(_source, _destination, distortion_material); //Применяем шейдер искажения к исходной рендер-текстуре камеры текущего GameObject'а. Затем помещаем результат в конечную рендер-текстуру камеры текущего GameObject'а
        }
        else
        {
            if (zoomBlur_started)
            {
                Graphics.Blit(_source, _destination, zoomBlur_material);
            }
            else
            {
                Graphics.Blit(_source, _destination, passthrough_material);
            }
        }
    }
}
