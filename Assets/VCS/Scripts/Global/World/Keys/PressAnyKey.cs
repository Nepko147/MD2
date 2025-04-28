using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    [SerializeField] private AudioClip switchSound;    

    private void FixedUpdate()
    {
        string button = "";
        //Обработка ввода "начальных" клавиш
        
        if (Input.GetKey(KeyCode.Backspace))
        {
            button = "BackSpaceKey";
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            button = "DownKey"; 
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            button = "UpKey";
        }
        if (Input.GetKey(KeyCode.Return))
        {
            button = "EnterKey"; 
        }

        if (button == "")
        {
            return;
        }
        AudioManager.Instance.PlaySound(switchSound);
        GameObject.Find(button).GetComponent<Animator>().SetBool("blinking", true);               
        Destroy(GameObject.Find("Keys"), 1);
        Destroy(this.gameObject);
    }    
}
