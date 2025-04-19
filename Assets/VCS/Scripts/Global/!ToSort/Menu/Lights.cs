using UnityEngine;

public class Lights : MonoBehaviour
{
    public static Lights Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    public void PlayAnimation()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("play", true);
    }
}
