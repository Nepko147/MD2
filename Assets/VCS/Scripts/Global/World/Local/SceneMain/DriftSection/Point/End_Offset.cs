using UnityEngine;

public class World_Local_SceneMain_DriftSection_Point_End_Offset : World_Local_SceneMain_DriftSection_Point_Parent
{
    public static World_Local_SceneMain_DriftSection_Point_End_Offset SingleOnScene { get ; private set; }

    private void Awake()
    {
        SingleOnScene = this;
    }
}
