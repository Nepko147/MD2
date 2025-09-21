using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class AppScreen_Local_SceneMain_UICanvas_Cutscene_Dialogue_Entity : MonoBehaviour
{
    public static AppScreen_Local_SceneMain_UICanvas_Cutscene_Dialogue_Entity SingleOnScene { get; private set; }

    private CanvasGroup canvasGroup;
    private float canvasGroup_deltaApha = 4.0f;

    [SerializeField] private AudioClip sound;

    [SerializeField] private Image  image_player;
    [SerializeField] private Image  image_npc;
    private Color                   image_color_active;
    private Color                   image_color_inactive;

    [SerializeField] private Text text;
    
    private List<string[]>  dialogue;
    private int             dialogue_string_number = 0;
    private int             dialogue_string_number_crush = 4; //����� ������ ����� ������� �� �����������
    private string          dialogue_string_current = "";    

    private enum dialogue_state
    {
        onDisplay,
        inProgress,
        hidden,
        idle,
        size
    }

    dialogue_state dialogue_state_currnet;

    private bool    onScreenText_isUpdated;
    private float   onScreenText_charecters_updateTimer_init = 0.04f;
    private float   onScreenText_charecters_updateTimer;
    private int     onScreenText_characters_number = 0;

    public const string PLAYER = "player";
    public const string NPC = "npc";

    public bool IsCrushed { get; private set; }

    public bool Done { get; private set; }

    public void Show(float _delay)
    {
        IEnumerator _coroutine(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            dialogue_state_currnet = dialogue_state.onDisplay; 
        }

        var _routine = _coroutine(_delay);
        StartCoroutine(_routine);
        
    }

    public void Text_LanguageRefresh()
    {
        dialogue.Clear();
        dialogue = ControlPers_LanguageHandler.SingleOnScene.Dialogue_Ending_Get(PLAYER, NPC);
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

        IsCrushed = false;
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
                    canvasGroup.alpha = 1; // ����������� ������ ���������
                }

            break;
            
            case dialogue_state.inProgress:

                if (Input.anyKeyDown) //���� ������ �����...
                {
                    if (onScreenText_isUpdated) // ... � ��� ����, ������ ������� ��� �� ������...
                    {
                        if (dialogue.Count != dialogue_string_number) // ... � ��� ���� ������ �� ��������...
                        {
                            if (dialogue_string_number == dialogue_string_number_crush)
                            {
                                IsCrushed = true;
                            }

                            UpdateDialogue(); // ... �� ������� ��������� ������ �� �������...
                        }
                        else // ...�����, ������ �������� � ��� ����������� ����� �����, ��� ����� ����������.
                        {
                            dialogue_state_currnet = dialogue_state.hidden;
                        }
                    }
                    else // ... � ��� ����, ������ ������� �� ��� �� ������...
                    {
                        text.text = dialogue_string_current;    // ������������� ������� ��� ������ �� �����.
                        onScreenText_isUpdated = true;          // ��������� ��� ��� ��� �����, �������������
                        onScreenText_charecters_updateTimer = onScreenText_charecters_updateTimer_init; // ���������� ������ ������ ���������� �������
                    }
                }
                else if (!onScreenText_isUpdated) // ����, ���� �� ��� � ������ ������� �� ��� �� ������...
                {
                    if (onScreenText_charecters_updateTimer <= 0) // ... � ������ ����� �������� ����� ������ �� �����, ��:
                    {
                        ++onScreenText_characters_number; // ��������� � �������� ���-�� ��������

                        var _text = dialogue_string_current.Substring(0, onScreenText_characters_number); // ���� ������ ���-�� �������� �� ������� ������ �������
                        text.text = _text; // � ������� �� �� �����...
                        ControlPers_AudioMixer_Sounds.SingleOnScene.Play(sound); // ... � ����������� ������

                        if (onScreenText_characters_number == dialogue_string_current.Length) // ���������, ��� �� ������ �����
                        {
                            onScreenText_isUpdated = true; // ������� ��, ���� ����� ���
                        }
                        onScreenText_charecters_updateTimer = onScreenText_charecters_updateTimer_init; // ���������� ������ ������ ���������� �������
                    }
                    else // ���� ����� �� ������...
                    {
                        onScreenText_charecters_updateTimer -= Time.deltaTime; // ... �� ���
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
                    canvasGroup.alpha = 0; // ����������� ������ ������������
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
