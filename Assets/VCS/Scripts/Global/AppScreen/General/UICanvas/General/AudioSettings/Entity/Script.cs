using UnityEngine;

public class AppScreen_General_UICanvas_General_AudioSettings_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_General_UICanvas_General_AudioSettings_Entity SingleOnScene { get; private set; }

    public void Active_Set(bool _state)
    {
        gameObject.SetActive(_state);
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }
}
