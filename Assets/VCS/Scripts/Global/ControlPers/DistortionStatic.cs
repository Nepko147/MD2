using UnityEngine;

public class DistortionStatic : MonoBehaviour
{
    [SerializeField] Material distortion;
    [SerializeField] Texture2D baseNormalMap;
    [SerializeField] Texture2D gameOverNormalMap;
    float screenWidth;
    float screenHeight;

    public static DistortionStatic Singletone { get; private set; }

    private void Awake()
    {
        Singletone = this;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        float u_aspect = screenHeight / screenWidth;
        distortion.SetFloat("u_aspect", u_aspect);
        distortion.SetTexture("u_tex", Globalist.Instance.gameOver ? gameOverNormalMap : baseNormalMap);
        if (baseNormalMap != null) Graphics.Blit(source, destination, distortion);
    }
}
