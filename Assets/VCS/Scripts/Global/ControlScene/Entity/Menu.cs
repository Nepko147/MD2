using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Entity_Menu : MonoBehaviour
{
    //[SerializeField] SceneAsset scene_main;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            SceneManager.LoadScene(3);
        }
    }
}
