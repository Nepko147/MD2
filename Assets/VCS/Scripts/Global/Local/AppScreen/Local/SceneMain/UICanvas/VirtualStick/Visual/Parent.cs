using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_VirtualStick_Visual_Parent : AppScreen_General_UICanvas_Parent
{
    public bool Visible
    {
        set 
        {
            if (value)
            {
                canvasRenderer.SetAlpha(1f);
            }
            else
            {
                canvasRenderer.SetAlpha(0);
            }
        }
    }
    
    protected override void Awake()
    {
        base.Awake();

        canvasRenderer.SetAlpha(0);
    }
}
