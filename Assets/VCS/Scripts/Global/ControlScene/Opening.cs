using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Opening : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(PersControl_SceneHandler.SCENENAME_MENU);
        }
    }
}
