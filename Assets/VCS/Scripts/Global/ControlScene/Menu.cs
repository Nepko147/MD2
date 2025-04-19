using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Menu : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(PersControl_SceneHandler.SCENENAME_MAIN);
        }
    }
}
