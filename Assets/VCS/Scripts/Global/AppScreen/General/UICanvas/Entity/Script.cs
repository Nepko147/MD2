using UnityEngine;

public class AppScreen_General_UICanvas_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_General_UICanvas_Entity SingleOnScene { get; private set; }
    
    public Camera Camera { get; private set; }

    [SerializeField] private AppScrren_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_PopUpMessage_Entity popUpMessage_prefab;
    private AppScrren_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_PopUpMessage_Entity popUpMessage_instance;
    public bool PopUpMessage_IsActive { get; private set; }

    public void PopUpMessage_Show(string _text)
    {
        if (!PopUpMessage_IsActive)
        {
            popUpMessage_instance = Instantiate(popUpMessage_prefab, Vector3.zero, transform.rotation, transform);
            popUpMessage_instance.Text = _text;

            PopUpMessage_IsActive = true;
        }
    }

    public void PopUpMessage_Remove()
    {
        if (PopUpMessage_IsActive)
        {
            Destroy(popUpMessage_instance.gameObject);

            PopUpMessage_IsActive = false;
        }
    }

    [SerializeField] private GameObject tutorial_prefab;

    public void Tutorial_Show()
    {
        Instantiate(tutorial_prefab, transform);
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;

        Camera = GetComponent<Canvas>().worldCamera;

        PopUpMessage_IsActive = false;
    }
}
