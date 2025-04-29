using UnityEngine;

public class AppScreen_Camera_DistortionDynamic : MonoBehaviour
{
    [SerializeField] Material distortion;
    [SerializeField] Material normalMapMix;
    [SerializeField] Texture2D baseNormalMap;
    [SerializeField] Texture2D distorionNormalMap;
    [SerializeField] float distortionSpeed;
    public float posX;
    public float posY;
    float screenWidth;
    float screenHeight;
    private Texture2D updatedTextute;
    RenderTexture rt;
    Rect rect;

    float timer = 3.0f;
    public bool distortionStart;

    public static AppScreen_Camera_DistortionDynamic Singletone { get; private set; }

    private void Awake()
    {
        Singletone = this;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        updatedTextute = new Texture2D(baseNormalMap.width, baseNormalMap.height, TextureFormat.RGBA32, false);
        rt = new RenderTexture(baseNormalMap.width, baseNormalMap.height, 32);    // Объявляем временную Рендер текстуру, в которой будем складывать основную нормалку с нормалкой искажения
        normalMapMix.SetTexture("_OverlayTex", null);                             
        
        Graphics.Blit(baseNormalMap, rt, normalMapMix);
        RenderTexture.active = rt;
        rect = new Rect(0, 0, rt.width, rt.height);
        updatedTextute.ReadPixels(rect, 0, 0);
        updatedTextute.Apply();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (distortionStart)
        {
            timer = 0.0f;
            distortionStart = false;
        }        
        
        if (timer <= 2.0f + Time.deltaTime)
        {
            // Готовим нормалку
            // Передаём в ШейдерМикс все необходимые параметры. ШейдерМикс - Упрощённый ТекстурМикс
            normalMapMix.SetTexture("_OverlayTex", distorionNormalMap);                 // Нормалка искажения
            normalMapMix.SetFloat("_OverlayTexApha", 1 - timer / 2 > 0 ? 1 - timer / 2 : 0);    // Уровень альфы
            normalMapMix.SetFloat("_OverlayTexPosX", posX);                             // Позиция на экране Х
            normalMapMix.SetFloat("_OverlayTexPosY", posY);                             // Позиция на экране У
            normalMapMix.SetFloat("_OverlayTexScale", timer * distortionSpeed);                       // Размер
            normalMapMix.SetFloat("_AspectX", screenWidth);                             // Ширина Экрана
            normalMapMix.SetFloat("_AspectY", screenHeight);                            // Высота Экрана
            Graphics.Blit(baseNormalMap, rt, normalMapMix);                             // Получаемм комбо из двух нормалей
            //RenderTexture.active = rt;           
            updatedTextute.ReadPixels(rect, 0, 0, false);       // Считываем пиксели в текстуру
            updatedTextute.Apply();                             // Применяем
            timer += Time.deltaTime;    // Обновляем таймер
        } 

        float u_aspect = screenHeight / screenWidth;
        distortion.SetFloat("u_aspect", u_aspect);
        distortion.SetTexture("u_tex", updatedTextute);
        if (updatedTextute != null) Graphics.Blit(source, destination, distortion);        
    }
}
