using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_Tutorial_VirtualStick : MonoBehaviour
{
    [SerializeField] private GameObject visual_inner;
    private Vector3                     visual_inner_position_min;
    private Vector3                     visual_inner_position_max;
    [SerializeField] private float      visual_inner_position_offset_speed = 1.5f;

    private void Awake()
    {        
        var _visual_inner_position_offset = 20.0f;
        visual_inner_position_min = visual_inner.transform.localPosition + Vector3.down * _visual_inner_position_offset;
        visual_inner_position_max = visual_inner.transform.localPosition + Vector3.up * _visual_inner_position_offset;
    }

    private void Update()
    {
        var _interpolation = Mathf.PingPong(Time.time * visual_inner_position_offset_speed, 1);
        var _position = Vector3.Lerp(visual_inner_position_min, visual_inner_position_max, _interpolation);
        visual_inner.transform.localPosition = _position;
    }
}
