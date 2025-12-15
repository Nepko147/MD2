using UnityEngine;
using System.Collections;

public class AppScreen_Local_SceneMain_UICanvas_MidScreen_Entity : AppScreen_General_UICanvas_Parent
{
    public static AppScreen_Local_SceneMain_UICanvas_MidScreen_Entity SingleOnScene { get; private set; }

    public void Shift_toDestination_WithDelay(float _delay, float _time)
    {
        IEnumerator _coroutine(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            Shift_toDestination(_time);
        }

        var _routine = _coroutine(_delay);
        StartCoroutine(_routine);
    }

    protected override void Awake()
    {
        base.Awake();

        SingleOnScene = this;
    }

    private void Start()
    {
        var _source = transform.localPosition;
        var _destination = transform.localPosition + Vector3.up * 100f;
        Shift_Positions_Set(_source, _destination);
    }
}
