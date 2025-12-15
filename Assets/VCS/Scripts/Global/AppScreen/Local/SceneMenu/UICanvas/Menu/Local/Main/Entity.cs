using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Main_Entity SingleOnScene { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        SingleOnScene = this;
    }

    private void Start()
    {
        var _source = new Vector3(0, 0, 0);
        var _destination = new Vector3(640f, 0, 0);
        Shift_Positions_Set(_source, _destination);
    }
}
