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

    private void Awake()
    {
        SingleOnScene = this;

        CurrentGameLanguage = gameLanguageInit;
    }
}
