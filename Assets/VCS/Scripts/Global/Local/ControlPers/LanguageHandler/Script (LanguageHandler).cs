using UnityEngine;

public class ControlPers_LanguageHandler : MonoBehaviour
{
    public static ControlPers_LanguageHandler SingleOnScene { get; private set; }

    public enum GameLanguage
    {
        english,
        russian
    }

    public GameLanguage CurrentGameLanguage { get; private set; }

    [SerializeField] private GameLanguage gameLanguageInit;

    public void SetGameLanguage(GameLanguage _language)
    {
        CurrentGameLanguage = _language;
    }

    private void Awake()
    {
        SingleOnScene = this;

        SetGameLanguage(gameLanguageInit);
    }
}
