using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Opening : MonoBehaviour
{
    //[SerializeField] SceneAsset scene_menu;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(1);
        }
    }
}
