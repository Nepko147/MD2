using UnityEngine;

public abstract class AppScreen_Local_SceneMenu_UICanvas_Menu_General_Entity_Parent : AppScreen_General_UICanvas_Parent
{
    protected virtual void Start()
    {
        var _source = new Vector3(0, -360, 0);
        var _destination = new Vector3(0, -10, 0);
        Shift_Positions_Set(_source, _destination);
    }
}
