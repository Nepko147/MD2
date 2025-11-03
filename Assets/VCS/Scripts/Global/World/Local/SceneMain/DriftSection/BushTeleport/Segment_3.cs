using UnityEngine;
using Utils;

public class World_Local_SceneMain_DriftSection_BushTeleport_Segment_3 : World_Local_SceneMain_DriftSection_BushTeleport_Parent
{
    private void Update()
    {
        if (Teleport_Condition())
        {
            World_Local_SceneMain_DriftSection_Enity_14.SingleOnScene.Segment_3_Teleport();
        }
    }
}
