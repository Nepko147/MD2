using UnityEngine;

public class AppScreen_UICanvas_Menu_Settings_Entity : AppScreen_UICanvas_Menu_Entity_Parent
{
    public static AppScreen_UICanvas_Menu_Settings_Entity SingleOnScene { get; private set; }

    private new void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    private void Start()
    {
        var _source = new Vector3(0, -360, 0);
        var _destination = new Vector3(0, 0, 0);
        Shift_Positions_Set(_source, _destination);
    }
}
