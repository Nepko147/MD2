using UnityEngine;

public class ControlPers_FogHandler : MonoBehaviour
{
    private static Vector4 Color;
    private static int color_state;

    private static float Offset = 0;

    public static void Color_Save()
    {
        Color = World_Fog.Singleton.Material_Color_Val;
        color_state = (int)World_Fog.Singleton.Material_Color_CurrentState;
    }

    public static void Color_Load()
    {
        World_Fog.Singleton.Material_Color_Val = Color;
        World_Fog.Singleton.Material_Color_CurrentState = (World_Fog.Material_Color_State)color_state;
    }

    public static void Move()
    {
        Offset = World_Fog.Singleton.Offset_Step(Offset);
    }
}
