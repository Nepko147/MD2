using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class AppScreen_Local_SceneMenu_UICanvas_Cutscene_Entity : MonoBehaviour
{
    public static AppScreen_Local_SceneMenu_UICanvas_Cutscene_Entity SingleOnScene { get; private set; }

    private CanvasGroup canvasGroup;
    private float canvasGroup_deltaApha = 4f;

    [SerializeField] private AudioClip sound;

    [SerializeField] private Image  image_player;
    [SerializeField] private Image  image_npc;
    private Color                   image_color_active;
    private Color                   image_color_inactive;

    [SerializeField] private Text text;
    
    private List<string[]>  dialogue;
    private int             dialogue_string_number = 0;
    private string          dialogue_string_current = "";

    private enum dialogue_state
    {
        onDisplay,
        inProgress,
        hidden,
        idle
    }

    dialogue_state dialogue_state_currnet;

    private bool    onScreenText_isUpdated;
    private float   onScreenText_charecters_updateTimer_init = 0.04f;
    private float   onScreenText_charecters_updateTimer;
    private int     onScreenText_characters_number = 0;

    public const string PLAYER = "player";
    public const string NPC = "npc";

    public bool Done { get; private set; }

    public void Show(float _delay)
    {
        IEnumerator _Coroutine(float _delay)
        {
            yield return (new WaitForSeconds(_delay));

            dialogue_state_currnet = dialogue_state.onDisplay; 
        }

        var _routine = _Coroutine(_delay);
        StartCoroutine(_routine);
        
    }

    public void Text_LanguageRefresh()
    {
        dialogue.Clear();

        var _languageHandler = ControlPers_LanguageHandler.SingleOnScene;        
       
        string[][] _dialogue_array = new string[][]
        {
            new[] { PLAYER, _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.dialogue_start_string_1) },
            new[] { NPC, _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.dialogue_start_string_2) },
            new[] { NPC, _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.dialogue_start_string_3) },
            new[] { NPC, _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.dialogue_start_string_4) },
            new[] { NPC, _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.dialogue_start_string_5) },
            new[] { PLAYER, _languageHandler.Text_Get(ControlPers_LanguageHandler.Text_Key.dialogue_start_string_6) }
        };

        foreach (var _string in _dialogue_array)
        {
            dialogue.Add(_string);
        }

        dialogue_string_current = dialogue[0][1];
    }

    private void UpdateDialogue()
    {
        dialogue_string_current = dialogue[dialogue_string_number][1];
        
        onScreenText_isUpdated = false;
        onScreenText_characters_number = 0;

        switch (dialogue[dialogue_string_number][0])
        {
            case PLAYER:
                image_player.color = image_color_active;
                image_npc.color = image_color_inactive;
                text.alignment = TextAnchor.UpperLeft;
                text.text = "";
            break;

            case NPC:
                image_player.color = image_color_inactive;
                image_npc.color = image_color_active;
                text.alignment = TextAnchor.UpperRight;
                text.text = "";
            break;
        }

        ++dialogue_string_number;
    }

    private void Awake()
    {
        SingleOnScene = this;

        Done = false;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        dialogue = new List<string[]>();
        dialogue_state_currnet = dialogue_state.idle;

        onScreenText_charecters_updateTimer = onScreenText_charecters_updateTimer_init;

        image_color_active = Color.white;
        image_color_inactive = new Color(1, 1, 1, 0.25f);
    }

    private void Start()
    {
        Text_LanguageRefresh();
        UpdateDialogue();
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate += Text_LanguageRefresh;
    }

    private void Update()
    {
        switch (dialogue_state_currnet)
        {
            case dialogue_state.onDisplay:
                
                if(canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha += canvasGroup_deltaApha * Time.deltaTime;
                }
                else
                {
                    dialogue_state_currnet = dialogue_state.inProgress;
                    canvasGroup.alpha = 1; // Гарантируем полное появление
                }

            break;
            
            case dialogue_state.inProgress:

                if (Input.anyKeyDown) //Если делаем импут...
                {
                    if (onScreenText_isUpdated) // ... и при этом, строка диалога ВСЯ на экране...
                    {
                        if (dialogue.Count != dialogue_string_number) // ... и при этом диалог не завершён...
                        {
                            UpdateDialogue(); // ... то втыкаем следующию строку из диалога...
                        }
                        else // ...иначе, диалог закончен и даём контроллеру сцены знать, что можно стартовать.
                        {
                            dialogue_state_currnet = dialogue_state.hidden;
                        }
                    }
                    else // ... и при этом, строка диалога НЕ вся на экране...
                    {
                        text.text = dialogue_string_current;    // Принудительно выводим всю строку на экран.
                        onScreenText_isUpdated = true;          // Указываем что она вся вышла, принудительно
                        onScreenText_charecters_updateTimer = onScreenText_charecters_updateTimer_init; // Сбрасываем таймер вывода следующего символа
                    }
                }
                else if (!onScreenText_isUpdated) // Если, ничо не жмём и строка диалога НЕ вся на экране...
                {
                    if (onScreenText_charecters_updateTimer <= 0) // ... и пришло время выводить новый символ на экран, то:
                    {
                        ++onScreenText_characters_number; // Добавляем к счётчику кол-ва символов

                        var _text = dialogue_string_current.Substring(0, onScreenText_characters_number); // берём нужное кол-во символов из текущей строки диалога
                        text.text = _text; // и выводим их на экран...
                        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound); // ... с характерным звуком

                        if (onScreenText_characters_number == dialogue_string_current.Length) // Проверяем, вся ли строка вышла
                        {
                            onScreenText_isUpdated = true; // Говорим да, если вышла вся
                        }
                        onScreenText_charecters_updateTimer = onScreenText_charecters_updateTimer_init; // Сбрасываем таймер вывода следующего символа
                    }
                    else // Если время не пришло...
                    {
                        onScreenText_charecters_updateTimer -= Time.deltaTime; // ... то ждём
                    }
                }

            break;

            case dialogue_state.hidden:
                
                if (canvasGroup.alpha > 0)
                {
                    canvasGroup.alpha -= canvasGroup_deltaApha * Time.deltaTime;
                }
                else
                {
                    dialogue_state_currnet = dialogue_state.idle;
                    canvasGroup.alpha = 0; // Гарантируем полное исчезновение
                    Done = true;
                }

            break;
        }               
    }

    private void OnDestroy()
    {
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate -= Text_LanguageRefresh;
    }
}
