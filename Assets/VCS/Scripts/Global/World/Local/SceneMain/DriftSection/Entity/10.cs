using UnityEngine;

public class World_Local_SceneMain_DriftSection_Enity_10 : World_Local_SceneMain_DriftSection_Enity_Parent
{
    [SerializeField] private GameObject segment_point;
    [SerializeField] private GameObject segment_scenario_1;
    [SerializeField] private GameObject segment_scenario_2;

    [SerializeField] private AudioClip[] segment_switch_sound_array;

    private enum Segment_State
    {
        scenario_1,
        scenario_2,
        size
    }

    private Segment_State segment_state_current;

    private bool segment_activated = false;

    [SerializeField] private World_Local_SceneMain_DriftSection_People people_prefab;
    [SerializeField] private GameObject[] people_positions_1;
    [SerializeField] private GameObject[] people_positions_2;
    [SerializeField] private GameObject[] people_positions_3;
    [SerializeField] private GameObject[] people_positions_4;
    [SerializeField] private GameObject[] people_positions_5;
    [SerializeField] private GameObject[] people_positions_6;

    protected override void Start()
    {
        base.Start();

        People_Spawn(people_prefab, people_positions_1);
        People_Spawn(people_prefab, people_positions_2);
        People_Spawn(people_prefab, people_positions_3);
        People_Spawn(people_prefab, people_positions_4);
        People_Spawn(people_prefab, people_positions_5);
        People_Spawn(people_prefab, people_positions_6);

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
        && World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position.x > segment_point.transform.position.x)
        {
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

            var _ind = Random.Range(0, segment_switch_sound_array.Length);
            ControlPers_AudioMixer_Sounds.SingleOnScene.Play(segment_switch_sound_array[_ind]);

            segment_activated = true;
        }
    }
}
