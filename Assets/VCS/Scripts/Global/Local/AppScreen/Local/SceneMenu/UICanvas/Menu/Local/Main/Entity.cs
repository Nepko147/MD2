using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity : AppScreen_Local_SceneMenu_UICanvas_Menu_General_Entity_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        SingleOnScene = this;
    }

    public void PrepareToGameStart()
    {
        var _source = new Vector3(0, 0, 0);
        var _destination = new Vector2(transform.position.x - 3600.0f, 0); new Vector2(3600f, 0);
        Shift_Positions_Set(_source, _destination);
    }

    private void Start()
    {
        var _source = new Vector3(0, 0, 0);
        var _destination = new Vector3(640f, 0, 0);
        Shift_Positions_Set(_source, _destination);
    }
}
