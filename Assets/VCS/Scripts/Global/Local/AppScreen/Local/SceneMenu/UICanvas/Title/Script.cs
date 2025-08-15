using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Title : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Title SingleOnScene { get; private set; }

    private SpriteRenderer spriteRenderer;
    
    public bool Visible 
    {
        get { return (spriteRenderer.enabled); }
        set { spriteRenderer.enabled = value; } 
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        var _destination = new Vector3(rectTransform.localPosition.x - 640f, rectTransform.localPosition.y, rectTransform.localPosition.z);
        Shift_Positions_Set(rectTransform.localPosition, _destination);
    }
}
