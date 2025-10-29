using UnityEngine;

public class World_Local_SceneMain_DriftSection_Enity_8 : World_Local_SceneMain_DriftSection_Enity_Parent
{
    [SerializeField] private World_Local_SceneMain_DriftSection_People people_prefab;
    [SerializeField] private GameObject[] people_positions_1;
    [SerializeField] private GameObject[] people_positions_2;
    [SerializeField] private GameObject[] people_positions_3;
    [SerializeField] private GameObject[] people_positions_4;
    [SerializeField] private GameObject[] people_positions_5;

    protected override void Start()
    {
        base.Start();

        People_Spawn(people_prefab, people_positions_1);
        People_Spawn(people_prefab, people_positions_2);
        People_Spawn(people_prefab, people_positions_3);
        People_Spawn(people_prefab, people_positions_4);
        People_Spawn(people_prefab, people_positions_5);
    }
}
