using UnityEngine;

public class World_Local_SceneMain_DriftSection_Point_Start : World_Local_SceneMain_DriftSection_Point_Parent
{
    public static World_Local_SceneMain_DriftSection_Point_Start SingleOnScene { get ; private set; }

    private void Awake()
    {
        SingleOnScene = this;
    }
}
