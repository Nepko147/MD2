using UnityEngine;

public class World_Background_BushesTitle_Animation : MonoBehaviour
{
    public static World_Background_BushesTitle_Animation Singletone { get; private set; }

    Animator animator;
    const string ANIMATOR_PARAMETER_PLAY = "play";    

    public void PlayAnimation()
    {        
        animator.SetBool(ANIMATOR_PARAMETER_PLAY, true);
    }
    
    void Awake()
    {
        Singletone = this;
        animator = GetComponent<Animator>();
    }
}
