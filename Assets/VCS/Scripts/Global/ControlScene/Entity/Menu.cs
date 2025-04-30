using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Entity_Menu : MonoBehaviour
{
    public void Update()
    {
        if (World_Buttons.Singletone.GO)
        {
            SceneManager.LoadScene(2);
        }
    }
}
