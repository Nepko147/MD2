using UnityEngine;

public class AppScreen_Local_SceneOpening_UICanvas_Title : MonoBehaviour
{
    public static AppScreen_Local_SceneOpening_UICanvas_Title SingleOnScene { get; private set; }

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
