using UnityEngine;

public class AppScreen_General_Camera_UI_ScreenFilter : MonoBehaviour
{
    [SerializeField] private Material material;

    private void Awake()
    {
        material.SetTexture("_MainTex", null); //�������� ��������� ������
    }

    private void OnRenderImage(RenderTexture _source, RenderTexture _destination)
    {
        Graphics.Blit(_source, _destination, material); //��������� ������ ��������� ������� � �������� ������-�������� ������ �������� GameObject'�. ����� �������� ��������� � �������� ������-�������� ������ �������� GameObject'�
    }
}
