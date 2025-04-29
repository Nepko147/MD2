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
        rt = new RenderTexture(baseNormalMap.width, baseNormalMap.height, 32);    // ��������� ��������� ������ ��������, � ������� ����� ���������� �������� �������� � ��������� ���������
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
            // ������� ��������
            // ������� � ���������� ��� ����������� ���������. ���������� - ���������� �����������
            normalMapMix.SetTexture("_OverlayTex", distorionNormalMap);                 // �������� ���������
            normalMapMix.SetFloat("_OverlayTexApha", 1 - timer / 2 > 0 ? 1 - timer / 2 : 0);    // ������� �����
            normalMapMix.SetFloat("_OverlayTexPosX", posX);                             // ������� �� ������ �
            normalMapMix.SetFloat("_OverlayTexPosY", posY);                             // ������� �� ������ �
            normalMapMix.SetFloat("_OverlayTexScale", timer * distortionSpeed);                       // ������
            normalMapMix.SetFloat("_AspectX", screenWidth);                             // ������ ������
            normalMapMix.SetFloat("_AspectY", screenHeight);                            // ������ ������
            Graphics.Blit(baseNormalMap, rt, normalMapMix);                             // ��������� ����� �� ���� ��������
            //RenderTexture.active = rt;           
            updatedTextute.ReadPixels(rect, 0, 0, false);       // ��������� ������� � ��������
            updatedTextute.Apply();                             // ���������
            timer += Time.deltaTime;    // ��������� ������
        } 

        float u_aspect = screenHeight / screenWidth;
        distortion.SetFloat("u_aspect", u_aspect);
        distortion.SetTexture("u_tex", updatedTextute);
        if (updatedTextute != null) Graphics.Blit(source, destination, distortion);        
    }
}
