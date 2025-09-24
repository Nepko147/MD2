using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_Cutscene_Background_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_Cutscene_Background_Entity SingleOnScene { get; private set; }

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

    protected override void Awake()
    {
        base.Awake();

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
