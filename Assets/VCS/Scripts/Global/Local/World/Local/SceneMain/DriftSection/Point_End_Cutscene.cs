using UnityEngine;

public class World_Local_SceneMain_DriftSection_Point_End_Cutscene : MonoBehaviour
{
    public static World_Local_SceneMain_DriftSection_Point_End_Cutscene SingleOnScene { get ; private set; }

    private void Awake()
    {
        SingleOnScene = this;
    }
}
