using UnityEngine;

public class Universal_DistortionDynamic : MonoBehaviour
{
    public static Universal_DistortionDynamic Singletone { get; private set; }

    float screenWidth;
    float screenHeight;

    [SerializeField] Material   distortion_material;
    const string                DISTORION_MATERIAL_U_ASPECT = "u_aspect";
    const string                DISTORION_MATERIAL_U_TEXNORMALMAP = "u_texNormalMap";
    float                       distortion_material_aspect;

    [SerializeField] Material   normalMapMix_material;
    const string                NORMALMAPMIX_MATERIAL_OVERLAYTEX = "_OverlayTex";
    const string                NORMALMAPMIX_MATERIAL_OVERLAYTEXALPHA = "_OverlayTexApha";
    const string                NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSX = "_OverlayTexPosX";
    const string                NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSY = "_OverlayTexPosY";
    const string                NORMALMAPMIX_MATERIAL_OVERLAYTEXSCALE = "_OverlayTexScale";
    const string                NORMALMAPMIX_MATERIAL_ASPECTX = "_AspectX";
    const string                NORMALMAPMIX_MATERIAL_ASPECTY = "_AspectY";
    [SerializeField] Texture2D  normalMapMix_material_baseNormalMap;
    [SerializeField] Texture2D  normalMapMix_material_distorionNormalMap;
    [SerializeField] float      normalMapMix_material_distortionSpeed;
    public float                NormalMapMix_Material_Pos_X { get; set; }
    public float                NormalMapMix_Material_Pos_Y { get; set; }
    public bool                 NormalMapMix_Material_Active { get; set; }
    float                       normalMapMix_material_timer;
    [SerializeField] float      normalMapMix_material_timer_max;
    RenderTexture               normalMapMix_material_output_normalMap_renderTexture;
    Texture2D                   normalMapMix_material_output_normalMap_texture2d;
    Rect                        normalMapMix_material_output_normalMap_rect;

    private void Awake()
    {
        Singletone = this;

        screenWidth = Screen.width;
        screenHeight = Screen.height;
        distortion_material_aspect = screenHeight / screenWidth;
        distortion_material.SetFloat(DISTORION_MATERIAL_U_ASPECT, distortion_material_aspect);
            
        normalMapMix_material_output_normalMap_renderTexture = new RenderTexture(normalMapMix_material_baseNormalMap.width, normalMapMix_material_baseNormalMap.height, 32); //�������������� ������-��������, � ������� ����� �������� �������� �� ���� � ��������
        Graphics.Blit(normalMapMix_material_baseNormalMap, normalMapMix_material_output_normalMap_renderTexture, normalMapMix_material); // ������������ ������ �� �������� ������ �������. �����, �������� ��������� � �������������� ������-�������       
        normalMapMix_material_output_normalMap_rect = new Rect(0, 0, normalMapMix_material_output_normalMap_renderTexture.width, normalMapMix_material_output_normalMap_renderTexture.height); //GETRECT!
        
        normalMapMix_material_output_normalMap_texture2d = new Texture2D(normalMapMix_material_baseNormalMap.width, normalMapMix_material_baseNormalMap.height, TextureFormat.RGBA32, false); //�������������� 2D-��������
        normalMapMix_material_output_normalMap_texture2d.ReadPixels(normalMapMix_material_output_normalMap_rect, 0, 0); //�������� ������� � �������������� ������-�������� � ���� Colors 2D-��������
        normalMapMix_material_output_normalMap_texture2d.Apply(); //������������ �� 2D-��������, ���������� � � ���� Colors �������
    }

    private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        if (NormalMapMix_Material_Active)
        {            
            normalMapMix_material.SetTexture(NORMALMAPMIX_MATERIAL_OVERLAYTEX, normalMapMix_material_distorionNormalMap); 
            float _alpha = 1 - normalMapMix_material_timer / normalMapMix_material_timer_max;
            _alpha = Mathf.Clamp(_alpha, 0, 1);
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXALPHA, _alpha);                      
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSX, NormalMapMix_Material_Pos_X);  
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXPOSY, NormalMapMix_Material_Pos_Y);  
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_OVERLAYTEXSCALE, normalMapMix_material_timer * normalMapMix_material_distortionSpeed);
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_ASPECTX, screenWidth);
            normalMapMix_material.SetFloat(NORMALMAPMIX_MATERIAL_ASPECTY, screenHeight);
            Graphics.Blit(normalMapMix_material_baseNormalMap, normalMapMix_material_output_normalMap_renderTexture, normalMapMix_material); //��������� ������ ������� � �������� ���������. �����, �������� ��������� � �������������� ������-������� 
            
            normalMapMix_material_output_normalMap_texture2d.ReadPixels(normalMapMix_material_output_normalMap_rect, 0, 0, false); //�������� ������� � �������������� ������-�������� � ���� Colors 2D-��������
            normalMapMix_material_output_normalMap_texture2d.Apply(); //������������ �� 2D-��������, ���������� � � ���� Colors �������
            normalMapMix_material_timer += Time.deltaTime;
            
            if (normalMapMix_material_timer >= normalMapMix_material_timer_max)
            {
                NormalMapMix_Material_Active = false;
                normalMapMix_material_timer = 0;
            }        
        }

        distortion_material.SetTexture(DISTORION_MATERIAL_U_TEXNORMALMAP, normalMapMix_material_output_normalMap_texture2d);
        Graphics.Blit(_source, _destination, distortion_material); //�������� ������-�������� � ������ �������� GameObject'�. ����� �������� ��������� � ������-�������� ������ �������� GameObject'�
    }
}
