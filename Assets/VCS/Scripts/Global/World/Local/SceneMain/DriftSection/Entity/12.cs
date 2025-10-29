using UnityEngine;

public class World_Local_SceneMain_DriftSection_Enity_12 : World_Local_SceneMain_DriftSection_Enity_Parent
{
    [SerializeField] private GameObject segment_1_point;
    [SerializeField] private GameObject segment_1_part_1;
    [SerializeField] private GameObject segment_1_part_2;

    [SerializeField] private GameObject segment_2_point;
    [SerializeField] private GameObject segment_2;

    [SerializeField] private AudioClip[] segment_switch_sound_array;

    private bool segment_1_activated = false;
    private bool segment_2_activated = false;

    [SerializeField] private World_Local_SceneMain_DriftSection_People people_prefab;
    [SerializeField] private GameObject[] people_positions_1;
    [SerializeField] private GameObject[] people_positions_2;
    [SerializeField] private GameObject[] people_positions_3;
    [SerializeField] private GameObject[] people_positions_4;
    [SerializeField] private GameObject[] people_positions_5;
    [SerializeField] private GameObject[] people_positions_6;
    [SerializeField] private GameObject[] people_positions_7;
    [SerializeField] private GameObject[] people_positions_8;
    [SerializeField] private GameObject[] people_positions_9;
    [SerializeField] private GameObject[] people_positions_10;

    protected override void Start()
    {
        base.Start();

        People_Spawn(people_prefab, people_positions_1);
        People_Spawn(people_prefab, people_positions_2);
        People_Spawn(people_prefab, people_positions_3);
        People_Spawn(people_prefab, people_positions_4);
        People_Spawn(people_prefab, people_positions_5);
        People_Spawn(people_prefab, people_positions_6);
        People_Spawn(people_prefab, people_positions_7);
        People_Spawn(people_prefab, people_positions_8);
        People_Spawn(people_prefab, people_positions_9);
        People_Spawn(people_prefab, people_positions_10);

        segment_1_part_2.SetActive(false);
        segment_2.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        if (!segment_1_activated)
        {
            if (World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position.x > segment_1_point.transform.position.x
            && World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position.y > segment_1_point.transform.position.y)
            {
               segment_1_part_1.SetActive(false);
               segment_1_part_2.SetActive(true);

               segment_1_activated = true;
            }
        }
        else
        {
            if (segment_2_activated)
            {
                if (World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position.x < segment_1_point.transform.position.x)
                {
                    segment_2.SetActive(true);

                    segment_2_activated = false;
                }
            }
        }
    }
}
