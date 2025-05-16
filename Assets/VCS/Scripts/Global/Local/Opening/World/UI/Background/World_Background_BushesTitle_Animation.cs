using UnityEngine;

public class World_Background_BushesTitle_Animation : MonoBehaviour
{
    public static World_Background_BushesTitle_Animation Singletone { get; private set; }

    public bool Done { get; private set; }

    const string ANIMATOR_PARAMETER_PLAY = "play";    

    public void PlayAnimation()
    {
        var _animator = GetComponent<Animator>();
        _animator.SetBool(ANIMATOR_PARAMETER_PLAY, true);
    }
    
    private void OnAnimationEnd()
    {
        Done = true;
    }

    void Awake()
    {
        Singletone = this;

        Done = false;        
    }
}
