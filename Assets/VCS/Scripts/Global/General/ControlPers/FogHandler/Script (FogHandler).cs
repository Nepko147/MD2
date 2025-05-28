using UnityEngine;

public class ControlPers_FogHandler : MonoBehaviour
{
    public static ControlPers_DataHandler SingleOnScene { get; private set; }

    private static Vector4 color_val;
    private static int color_state;

    private static float offset = 0;

    public static void Color_Save()
    {
        color_val = World_Fog.SingleOnScene.Material_Color_Val;
        color_state = (int)World_Fog.SingleOnScene.Material_Color_CurrentState;
    }

    public static void Color_Load()
    {
        World_Fog.SingleOnScene.Material_Color_Val = color_val;
        World_Fog.SingleOnScene.Material_Color_CurrentState = (World_Fog.Material_Color_State)color_state;
    }

    public static void Move()
    {
        offset = World_Fog.SingleOnScene.Material_Offset_Step(offset);
    }
}
