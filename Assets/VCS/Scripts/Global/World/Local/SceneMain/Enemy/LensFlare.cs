using UnityEngine;

public class World_Local_SceneMain_Enemy_LensFlare : MonoBehaviour
{
    public bool Active { get; set; }

    private Vector3 scale = Vector3.one;
    private const float SCALE_DISTANSE_MIN = 2f;
    private const float SCALE_DISTANSE_MAX = 6f;

    private void Awake()
    {
        Active = true;
    }

    void Update()
    {
        if (Active)
        {
            var _scale = Mathf.Clamp((transform.position.x - (World_Local_SceneMain_Player_Entity.SingleOnScene.transform.position.x + SCALE_DISTANSE_MIN)) / (SCALE_DISTANSE_MAX - SCALE_DISTANSE_MIN), 0, 1f);
            scale.x = _scale;
            scale.y = _scale;
            transform.localScale = scale;
        }
    }
}
