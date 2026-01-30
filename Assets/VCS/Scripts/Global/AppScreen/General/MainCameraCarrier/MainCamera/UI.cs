using UnityEngine;

public class AppScreen_General_MainCameraCarrier_MainCamera_UI : AppScreen_General_MainCameraCarrier_MainCamera_Parent
{
    [SerializeField] private Material material;

    protected override void Awake()
    {
        base.Awake();

        material.SetTexture("_MainTex", null); //Проводим шаманский ритуал
    }

    private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        Graphics.Blit(_source, _destination, material); //Применяем шейдер экранного фильтра к исходной рендер-текстуре камеры текущего GameObject'а. Затем помещаем результат в конечную рендер-текстуру камеры текущего GameObject'а
    }
}
