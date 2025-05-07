using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScene_Entity_Opening : MonoBehaviour
{
    public void Update()
    {
        if (World_Car.Singleton.Done)
        {
            SceneManager.LoadScene(1);
        }
    }
}
