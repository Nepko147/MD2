using UnityEngine;

public class ControlPers_LanguageHandler : MonoBehaviour
{
    public static ControlPers_LanguageHandler SingleOnScene { get; private set; }

    public enum GameLanguage
    {
        english,
        russian
    }

    public GameLanguage GameLanguage_Current { get; private set; }

    [SerializeField] private GameLanguage gameLanguage_init;

    private void Awake()
    {
        SingleOnScene = this;

        GameLanguage_Current = gameLanguage_init;
    }
}
