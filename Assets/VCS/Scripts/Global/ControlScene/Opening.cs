using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Opening : MonoBehaviour
{
    [SerializeField] SceneAsset scene_menu;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(scene_menu.name);
        }
    }
}
