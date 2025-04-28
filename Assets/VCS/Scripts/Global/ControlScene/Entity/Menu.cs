using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Entity_Menu : MonoBehaviour
{
    public void Update()
    {
        if (Buttons.Instance.GO)
        {
            SceneManager.LoadScene(2);
        }
    }
}
