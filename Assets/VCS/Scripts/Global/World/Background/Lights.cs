using UnityEngine;

public class World_BackGround_Lights : MonoBehaviour
{
    public static World_BackGround_Lights Singletone { get; private set; }

    void Awake()
    {
        Singletone = this;
    }
    public void PlayAnimation()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("play", true);
    }
}
