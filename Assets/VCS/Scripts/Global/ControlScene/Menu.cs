using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Menu : MonoBehaviour
{
    [SerializeField] SceneAsset scene_main;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(scene_main.name);
        }
    }
}
