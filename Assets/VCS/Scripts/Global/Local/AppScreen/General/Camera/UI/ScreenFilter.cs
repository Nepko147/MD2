using UnityEngine;

public class ScreenFilter : MonoBehaviour
{
    [SerializeField] private Material material;

    private void Awake()
    {
        material.SetTexture("_MainTex", null); //Проводим магический ритуал
    }

    private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        Graphics.Blit(_source, _destination, material); //Применяем шейдер экранного фильтра к исходной рендер-текстуре камеры текущего GameObject'а. Затем помещаем результат в конечную рендер-текстуру камеры текущего GameObject'а
    }
}
