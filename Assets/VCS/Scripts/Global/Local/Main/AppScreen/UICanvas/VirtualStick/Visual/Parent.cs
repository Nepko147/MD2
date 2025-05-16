using UnityEngine;

public class AppScreen_GeneralCanvas_VirtualStick_Visual_Parent : MonoBehaviour
{
    protected CanvasRenderer canvasRenderer;

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
    
    protected void Awake()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();
        canvasRenderer.SetAlpha(0);
    }
}
