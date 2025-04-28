using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Entity_Opening : MonoBehaviour
{
    public void Update()
    {
        if (Car.Singleton.Done)
        {
            SceneManager.LoadScene(1);
        }
    }
}
