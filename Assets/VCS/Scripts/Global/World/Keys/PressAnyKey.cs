using UnityEngine;
using UnityEngine.SceneManagement;

public class World_PressAnyKey : MonoBehaviour
{
    [SerializeField] private AudioClip switchSound;    

    private void FixedUpdate()
    {
        string button = "";
        //��������� ����� "���������" ������
        
        if (Input.GetKey(KeyCode.Backspace))
        {
            button = "Backspace_Sprite";
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            button = "Down_Sprite"; 
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            button = "Up_Sprite";
        }
        if (Input.GetKey(KeyCode.Return))
        {
            button = "Enter_Sprite"; 
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
