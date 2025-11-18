using UnityEngine;

public class World_Local_SceneMain_DriftSection_Enity_7 : World_Local_SceneMain_DriftSection_Enity_Parent
{
    [SerializeField] private GameObject segment_point;
    [SerializeField] private GameObject segment_scenario_1;
    [SerializeField] private GameObject segment_scenario_2;

    private enum Segment_State
    {
        scenario_1,
        scenario_2,
        size
    }

    private Segment_State segment_state_current;

    private bool segment_activated = false;

    protected override void Start()
    {
        base.Start();

        switch ((Segment_State)Random.Range((int)Segment_State.scenario_1, (int)Segment_State.size))
        {
            case Segment_State.scenario_1:
                segment_scenario_2.SetActive(false);
                segment_state_current = Segment_State.scenario_1;
            break;

            case Segment_State.scenario_2:
                segment_scenario_1.SetActive(false);
                segment_state_current = Segment_State.scenario_2;
            break;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!segment_activated
        && World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position.x > segment_point.transform.position.x
        && World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position.y < segment_point.transform.position.y)
        {
            ControlScene_Main.SingleOnScene.Audio_Sound_Mental_Play();
            ControlScene_Main.SingleOnScene.TimeDilation();
            AppScreen_General_Camera_World_Entity.SingleOnScene.ZoomBlur_Start();

            switch (segment_state_current)
            {
                case Segment_State.scenario_1:
                    segment_scenario_1.SetActive(false);
                    segment_scenario_2.SetActive(true);
                    segment_state_current = Segment_State.scenario_2;
                break;

                case Segment_State.scenario_2:
                    segment_scenario_2.SetActive(false);
                    segment_scenario_1.SetActive(true);
                    segment_state_current = Segment_State.scenario_1;
                break;
            }

            segment_activated = true;
        }
    }
}
