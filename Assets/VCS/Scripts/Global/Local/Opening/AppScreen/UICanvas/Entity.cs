using UnityEngine;

public class Opening_AppScreen_UICanvas_Entity : MonoBehaviour
{
    public static Opening_AppScreen_UICanvas_Entity Singltone { get; private set; }

    private void Awake()
    {
        Singltone = this;
    }
}
