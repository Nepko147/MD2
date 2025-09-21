using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AppScreen_Local_SceneMain_UICanvas_Cutscene_Statistics_Entity : MonoBehaviour
{
    public static AppScreen_Local_SceneMain_UICanvas_Cutscene_Statistics_Entity SingleOnScene { get; private set; }

    public bool Done { get; private set; }

    private CanvasGroup canvasGroup;
    private float canvasGroup_deltaApha = 4.0f;

    [SerializeField] private Text gameBy;

    [SerializeField] private GameObject newRecord;   

    [SerializeField] private Text reviveNumber_text;
    [SerializeField] private Text reviveNumber_number;

    [SerializeField] private Text reviveNumberBest_text;
    [SerializeField] private Text reviveNumberBest_number;
    private Vector3               reviveNumberBest_newRecord_position;
    private float                 reviveNumberBest_newRecord_position_offsetX = 0.2f;
    private float                 reviveNumberBest_newRecord_position_offsetY = 0.1f;
    private Quaternion            reviveNumberBest_newRecord_rotation = Quaternion.Euler(0.0f, 0.0f, -10.0f);

    [SerializeField] private Text coinsTotal_text;
    [SerializeField] private Text coinsTotal_number;

    [SerializeField] private Text coinsSpentOnRevivals_text;
    [SerializeField] private Text coinsSpentOnRevivals_number;

    [SerializeField] private Text defeats_text;
    [SerializeField] private Text defeats_number;

    [SerializeField] private Text totalDrivings_text;
    [SerializeField] private Text totalDrivings_number;
    
    private enum statistics_state
    {
        onDisplay,
        hidden,
        idle,
        size
    }

    statistics_state statistics_state_currnet;

    public void Statistics_Refresh()
    {
        reviveNumber_number.text = ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_ReviveNumber.ToString();
        
        var _reviveNumberBest = ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_ReviveNumberBest;
        
        if (ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_ReviveNumber < _reviveNumberBest)
        {
            _reviveNumberBest = ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_ReviveNumber;
            ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_ReviveNumberBest = _reviveNumberBest;
            Instantiate(newRecord, reviveNumberBest_newRecord_position, reviveNumberBest_newRecord_rotation, transform);
        }
        
        reviveNumberBest_number.text = _reviveNumberBest.ToString();
        coinsTotal_number.text =            ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_CoinsTotal.ToString();
        coinsSpentOnRevivals_number.text =  ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_CoinsSpentOnRevivals.ToString();
        defeats_number.text =               ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_Defeats.ToString();
        totalDrivings_number.text =         ControlPers_DataHandler.SingleOnScene.ProgressData_Statistics_TotalDrivings.ToString();
    }

    public void Show(float _delay)
    {
        IEnumerator _coroutine(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            statistics_state_currnet = statistics_state.onDisplay;
        }

        var _routine = _coroutine(_delay);
        StartCoroutine(_routine);

    }

    public void Hide()
    {
        statistics_state_currnet = statistics_state.hidden;
    }

    public void Text_LanguageRefresh()
    {
        gameBy.text =                       ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.gameBy);
        reviveNumber_text.text =            ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.statistics_reviveNumber) + ":";
        reviveNumberBest_text.text =        ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.statistics_best) + ":";
        coinsTotal_text.text =              ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.statistics_coinsTotal) + ":";
        coinsSpentOnRevivals_text.text =    ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.statistics_coinsSpentOnRevivals) + ":";
        defeats_text.text =                 ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.statistics_defeats) + ":";
        totalDrivings_text.text =           ControlPers_LanguageHandler.SingleOnScene.Text_Get(ControlPers_LanguageHandler.Text_Key.statistics_totalDrivings) + ":";
    }

    private void Awake()
    {        
        SingleOnScene = this;

        Done = false;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        statistics_state_currnet = statistics_state.idle;
    }

    private void Start()
    {
        reviveNumberBest_newRecord_position = reviveNumberBest_number.rectTransform.position 
            + Vector3.right * reviveNumberBest_newRecord_position_offsetX 
            + Vector3.up * reviveNumberBest_newRecord_position_offsetY;

        Text_LanguageRefresh();
        ControlPers_LanguageHandler.SingleOnScene.GameLanguage_OnUpdate += Text_LanguageRefresh;
    }

    private void Update()
    {
        switch (statistics_state_currnet)
        {
            case statistics_state.onDisplay:
                if (canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha += canvasGroup_deltaApha * Time.deltaTime;
                }
                else
                {
                    statistics_state_currnet = statistics_state.idle;
                    canvasGroup.alpha = 1; // ����������� ������ ���������
                }
            break;

            case statistics_state.hidden:
                if (canvasGroup.alpha > 0)
                {
                    canvasGroup.alpha -= canvasGroup_deltaApha * Time.deltaTime;
                }
                else
                {
                    statistics_state_currnet = statistics_state.idle;
                    canvasGroup.alpha = 0;
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
