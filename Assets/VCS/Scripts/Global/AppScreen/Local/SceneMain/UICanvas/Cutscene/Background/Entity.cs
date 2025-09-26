using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AppScreen_Local_SceneMain_UICanvas_Cutscene_Background_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Cutscene_Background_Entity SingleOnScene { get; private set; }

    private CanvasGroup canvasGroup;
    private float canvasGroup_deltaApha = 4.0f;

    public bool IsMoving 
    {
        set
        {
            bushes_1.Active = value;
            bushes_2.Active = value;
        }            
    }

    private Vector3 position_init;

    [SerializeField] private AppScreen_Local_SceneMain_UICanvas_Cutscene_Background_Bushes bushes_1;
    [SerializeField] private AppScreen_Local_SceneMain_UICanvas_Cutscene_Background_Bushes bushes_2;

    #region Shake

    public bool Shake_Active { get; set; }
    private bool shake_on = false;
    private const float SHAKE_DELAY_INIT = 0.016f;
    private float shake_delay_current = SHAKE_DELAY_INIT;
    private const float SHAKE_STEPS_INIT = 20f;
    private float shake_steps_current = SHAKE_STEPS_INIT;
    private const float SHAKE_OFS_X = 40.0f;
    private const float SHAKE_OFS_Y = 10.0f;
    private Vector3 shake_ofs_vec3 = Vector3.zero;

    public void Shake()
    {
        position_init = transform.localPosition;
        shake_on = true;
    }

    #endregion

    public void Show(float _delay)
    {
        IEnumerator _coroutine(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            background_state_currnet = background_state.onDisplay;
        }

        var _routine = _coroutine(_delay);
        StartCoroutine(_routine);

    }

    private enum background_state
    {
        onDisplay,
        hidden,
        idle,
        size
    }

    background_state background_state_currnet;

    protected override void Awake()
    {
        base.Awake();

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        SingleOnScene = this;
    }

    private void Start()
    {
        bushes_1.Active = false;
        bushes_2.Active = false;

        var _destination = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y + 250, rectTransform.localPosition.z);
        Shift_Positions_Set(rectTransform.localPosition, _destination);
    }

    private void Update()
    {
        switch (background_state_currnet)
        {
            case background_state.onDisplay:
                if (canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha += canvasGroup_deltaApha * Time.deltaTime;
                }
                else
                {
                    background_state_currnet = background_state.idle;
                    canvasGroup.alpha = 1; // Гарантируем полное появление
                }
            break;
            case background_state.hidden:
                if (canvasGroup.alpha > 0)
                {
                    canvasGroup.alpha -= canvasGroup_deltaApha * Time.deltaTime;
                }
                else
                {
                    background_state_currnet = background_state.idle;
                    canvasGroup.alpha = 0; // Гарантируем полное исчезновение
                }
            break;
        }

        if (shake_on)
        {
            shake_delay_current -= Time.deltaTime;

            if (shake_delay_current <= 0)
            {
                shake_delay_current = SHAKE_DELAY_INIT;

                var _shake_ofs_scale = shake_steps_current / SHAKE_STEPS_INIT;
                shake_ofs_vec3.x = position_init.x + Random.Range(-SHAKE_OFS_X, SHAKE_OFS_X) * _shake_ofs_scale;
                shake_ofs_vec3.y = position_init.y + Random.Range(-SHAKE_OFS_Y, SHAKE_OFS_Y) * _shake_ofs_scale;
                transform.localPosition = shake_ofs_vec3;

                --shake_steps_current;

                if (shake_steps_current == 0)
                {
                    shake_on = false;
                    shake_steps_current = SHAKE_STEPS_INIT;
                    transform.localPosition = position_init;
                }
            }
        }
    }
}
