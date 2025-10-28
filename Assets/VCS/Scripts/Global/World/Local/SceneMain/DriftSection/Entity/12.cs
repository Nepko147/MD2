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

    protected override void Start()
    {
        base.Start();

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
