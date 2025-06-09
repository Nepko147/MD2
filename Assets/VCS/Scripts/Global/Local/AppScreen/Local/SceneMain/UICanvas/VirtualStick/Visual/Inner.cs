using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_VirtualStick_Visual_Inner : AppScreen_Local_SceneMain_UICanvas_VirtualStick_Visual_Parent
{
    public Vector3 RectTransform_Position_Set
    {
        set { rectTransform.position = value; }
    }
    
    private void Start()
    {
        AppScreen_Local_SceneMain_UICanvas_VirtualStick_Entity.SingleOnScene.Visual_Inner = this;
    }
}
