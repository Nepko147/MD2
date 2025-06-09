using UnityEngine;

public class World_Local_SceneOpening_UI_TitleAnimation : MonoBehaviour
{
    public static World_Local_SceneOpening_UI_TitleAnimation SingleOnScene { get; private set; }

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
        SingleOnScene = this;

        Done = false;        
    }
}
