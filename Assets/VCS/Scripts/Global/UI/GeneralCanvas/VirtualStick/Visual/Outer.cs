using UnityEngine;

public class UI_GeneralCanvas_VirtualStick_Visual_Outer : UI_GeneralCanvas_VirtualStick_Visual_Parent
{
    private void Start()
    {
        UI_GeneralCanvas_VirtualStick_Entity.Singleton.Visual_Outer = this;
    }
}
