using UnityEngine;

public class AppScreen_Local_SceneMenu_UICanvas_Menu_Local_Upgrades_Upgrade_General_Entity_Parent : AppScreen_General_UICanvas_Parent
{
    private const float ANIMATION_TIME_MAX= 0.25f;
    private const float ANIMATION_TIME_STAGE_MAX = ANIMATION_TIME_MAX / 2f;
    private float animation_time_stage_current = 0;
    private const float ANIMATION_SCALE_INIT = 1f;
    private const float ANIMATION_SCALE_OFFSET = 0.5f;
    private Vector3 animation_scale;
    private enum Animation_Stage
    {
        idle,
        increase,
        decrease
    }
    private Animation_Stage animation_stage;

    protected override void Awake()
    {
        base.Awake();

        animation_scale = new Vector2(ANIMATION_SCALE_INIT, ANIMATION_SCALE_INIT);
        animation_stage = Animation_Stage.idle;
    }

    private void Animation_Scale_Set(float _scale)
    {
        animation_scale.x = _scale;
        animation_scale.y = _scale;
        rectTransform.localScale = animation_scale;
    }

    public void Animation_Start()
    {
        animation_time_stage_current = 0;
        animation_stage = Animation_Stage.increase;
    }

    private void Update()
    {
        switch (animation_stage)
        {
            case Animation_Stage.increase:
                animation_time_stage_current += Time.deltaTime;

                Animation_Scale_Set(ANIMATION_SCALE_INIT + ANIMATION_SCALE_OFFSET * animation_time_stage_current / ANIMATION_TIME_STAGE_MAX);

                if (animation_time_stage_current >= ANIMATION_TIME_STAGE_MAX)
                {
                    animation_time_stage_current = 0;
                    animation_stage = Animation_Stage.decrease;
                }
            break;

            case Animation_Stage.decrease:
                animation_time_stage_current += Time.deltaTime;

                Animation_Scale_Set(ANIMATION_SCALE_INIT + ANIMATION_SCALE_OFFSET * (1f - (animation_time_stage_current / ANIMATION_TIME_STAGE_MAX)));

                if (animation_time_stage_current >= ANIMATION_TIME_STAGE_MAX)
                {
                    rectTransform.localScale = new Vector2(ANIMATION_SCALE_INIT, ANIMATION_SCALE_INIT);
                    animation_stage = Animation_Stage.idle;
                }
            break;
        }
    }
}
