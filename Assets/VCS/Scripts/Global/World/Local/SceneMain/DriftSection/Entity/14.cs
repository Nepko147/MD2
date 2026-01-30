using UnityEngine;

public class World_Local_SceneMain_DriftSection_Enity_14 : World_Local_SceneMain_DriftSection_Enity_Parent
{
    #region General

    public new static World_Local_SceneMain_DriftSection_Enity_14 SingleOnScene { get; private set; }

    private enum Segment_State
    {
        none,
        one,
        two,
        three,
        four
    }

    private Segment_State segment_state_current = Segment_State.none;

    private float segment_timer = SEGMENT_1_TIMER;

    private void Segment_Teleport(float _x, float _y)
    {
        ControlScene_Main.SingleOnScene.Audio_Sound_Mental_Play();
        var _pos = new Vector3(_x, _y, World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position.z);
        World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position = _pos;
        World_Local_SceneMain_Player_Entity.SingleOnScene.Invul_Activate(2.4f, Color.white);
        _pos = new Vector3(_x, _y, AppScreen_General_MainCameraCarrier_Entity.SingleOnScene.transform.position.z);
        AppScreen_General_MainCameraCarrier_Entity.SingleOnScene.transform.position = _pos;
        AppScreen_General_MainCameraCarrier_MainCamera_World.SingleOnScene.Shake();
        AppScreen_General_MainCameraCarrier_MainCamera_World.SingleOnScene.ZoomBlur_Start();
    }

    #endregion

    #region Segment_None

    [SerializeField] private GameObject segment_none_point_start;

    #endregion

    #region Segment_1

    [SerializeField] private GameObject segment_1_point_trigger;
    [SerializeField] private GameObject segment_1_point_destination;
    private const float SEGMENT_1_TIMER = 5f;

    public void Segment_1_Teleport()
    {
        segment_state_current = Segment_State.two;
        segment_timer = SEGMENT_2_TIMER;

        Segment_Teleport(segment_1_point_destination.transform.position.x, segment_1_point_destination.transform.position.y);

        Destroy(segment_1_point_trigger);
        Destroy(segment_1_point_destination);
    }

    #endregion

    #region Segment_2

    [SerializeField] private GameObject segment_2_point_destination;
    private const float SEGMENT_2_TIMER = 20f;

    public void Segment_2_Teleport()
    {
        segment_state_current = Segment_State.three;
        segment_timer = SEGMENT_3_TIMER;

        Segment_Teleport(segment_2_point_destination.transform.position.x, segment_2_point_destination.transform.position.y);

        Destroy(segment_2_point_destination);
    }

    #endregion

    #region Segment_3

    [SerializeField] private GameObject segment_3_point_trigger;
    [SerializeField] private GameObject segment_3_point_destination;
    private const float SEGMENT_3_TIMER = 20f;

    public void Segment_3_Teleport()
    {
        segment_state_current = Segment_State.four;

        Segment_Teleport(segment_3_point_destination.transform.position.x, segment_3_point_destination.transform.position.y);

        Destroy(segment_3_point_trigger);
        Destroy(segment_3_point_destination);
    }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (Active)
        {
            switch (segment_state_current)
            {
                case Segment_State.none:
                    if (World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position.y < segment_none_point_start.transform.position.y)
                    {
                        segment_state_current = Segment_State.one;
                    }
                break;

                case Segment_State.one:
                    segment_timer -= Time.deltaTime;

                    if (segment_timer <= 0
                    || World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position.x > segment_1_point_trigger.transform.position.x)
                    {
                        AppScreen_General_MainCameraCarrier_MainCamera_World.SingleOnScene.ZoomBlur_Intensity_Scale = 10f;
                        Segment_1_Teleport();
                    }
                break; 

                case Segment_State.two:
                    segment_timer -= Time.deltaTime;

                    if (segment_timer <= 0)
                    {
                        AppScreen_General_MainCameraCarrier_MainCamera_World.SingleOnScene.ZoomBlur_Intensity_Scale = 200f;
                        Segment_2_Teleport();
                    }
                break; 

                case Segment_State.three:
                    segment_timer -= Time.deltaTime;

                    if (segment_timer <= 0
                    || World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position.y > segment_3_point_trigger.transform.position.y)
                    {
                        AppScreen_General_MainCameraCarrier_MainCamera_World.SingleOnScene.ZoomBlur_Intensity_Scale = 1000f;
                        Segment_3_Teleport();
                    }
                break;
            }
        }
    }
}
