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
        var _dest_ofs = new Vector2(640f, 0);
        Shift_Pos_Define(Vector2.zero, _dest_ofs);
    }
}
