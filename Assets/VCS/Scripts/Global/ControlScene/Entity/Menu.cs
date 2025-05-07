using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Entity_Menu : MonoBehaviour
{
    public static ControlScene_Entity_Menu Singletone { get; private set; }

    public bool GameStart { get; set; }

    private void Start()
    {
        Singletone = this;

        GameStart = false;          
    }

        public void Update()
    {
        if (Appscreen_Canvas_Buttons.Singletone.GO)
        {
            SceneManager.LoadScene(2);
        }
    }
}
