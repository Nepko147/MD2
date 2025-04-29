using UnityEngine;
using UnityEngine.SceneManagement;

public class World_PressAnyKey : MonoBehaviour
{
    [SerializeField] private AudioClip switchSound;    

    private void FixedUpdate()
    {
        string button = "";
        //Обработка ввода "начальных" клавиш
        
        if (Input.GetKey(KeyCode.Backspace))
        {
            button = "BackSpace";
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            button = "Down"; 
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            button = "Up";
        }
        if (Input.GetKey(KeyCode.Return))
        {
            button = "Enter"; 
        }

        if (button == "")
        {
            return;
        }
        ControlPers_AudioManager.Singletone.PlaySound(switchSound);
        GameObject.Find(button).GetComponent<Animator>().SetBool("blinking", true);               
        Destroy(GameObject.Find("Keys"), 1);
        Destroy(this.gameObject);
    }    
}
