using UnityEngine;

public class Universal_DistortionStatic : MonoBehaviour
{
    [SerializeField] Material distortion_material;
    [SerializeField] Texture2D baseNormalMap;
    [SerializeField] Texture2D gameOverNormalMap;
    float aspect;
    const string U_ASPECT = "u_aspect";
    const string U_TEXTEXNORMALMAP = "u_texNormalMap";

    public static Universal_DistortionStatic Singletone { get; private set; }

    private void Awake()
    {
        Singletone = this;        
    }

    private void Start()
    {       
        aspect = (float)Screen.height / (float)Screen.width;
        distortion_material.SetFloat(U_ASPECT, aspect);
    }

    private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {   
        Texture2D _texture2d = ControlScene_Entity_Main.Singletone.GameOver ? gameOverNormalMap : baseNormalMap;
        distortion_material.SetTexture(U_TEXTEXNORMALMAP, _texture2d);
        
        if (baseNormalMap != null)
        {
            Graphics.Blit(_source, _destination, distortion_material); //»скажаем рендер-текстуру с камеры текущего GameObject'а. «атем помещаем результат в рендер-текстуру камеры текущего GameObject'а
        }
    }
}
