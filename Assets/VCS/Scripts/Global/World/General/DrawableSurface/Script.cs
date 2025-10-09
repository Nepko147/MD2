using UnityEngine;
using System.Collections.Generic;

public class World_General_DrawableSurface : MonoBehaviour
{
    private Material material;
    [SerializeField] Material material_mixer;
    [SerializeField] Texture2D additionalTexture;
    [SerializeField] private Collider collision;
    [SerializeField] float angle;

    private Texture2D texture;
    private Texture2D texture_temp;
    [SerializeField] private TextureWrapMode texture_wrapMode;
    [SerializeField] private FilterMode texture_filterMode;

    private int texture_size_x;
    private int texture_size_y;

    #region Web

    private const float WEB_PAINTING_TIMETOLIVE = 5.0f;
    private const float WEB_PAINTING_LOCALSCALE = 1.5f;
    private const string WEB_PAINTING_SORTINGLAYERNAME = "Player";

    #endregion
        

    //���� ������ ����� ��������(��������) �������
    public void Texture_Refresh()
    {
        Texture_Refresh(texture_size_x, texture_size_y);
    }

    //���� ����� �������� ������ �������
    public void Texture_Refresh(int _newWidth, int _newHeight)
    {
        texture_size_x = _newWidth;
        texture_size_y = _newHeight;

        texture = new Texture2D(texture_size_x, texture_size_y, TextureFormat.RGBA32, false); //��������� ����� �������
        texture_temp = new Texture2D(texture_size_x, texture_size_y, TextureFormat.RGBA32, false); //��������� ����� �������

        Color[] _color_Clear = new Color[texture_size_x * texture_size_y]; //������ ������ ��� ������ ��������

        for (int _i = 0; _i < _color_Clear.Length; _i++)
        {
            _color_Clear[_i] = Color.clear; //��������� ������ ������� ���������
        }

        texture.SetPixels(_color_Clear); //��������� �������� ������� ��������� �� �������

        texture.wrapMode = texture_wrapMode;
        texture.filterMode = texture_filterMode;
        material.mainTexture = texture;
        texture.Apply();
        Graphics.CopyTexture(texture, texture_temp);
    }

    //���� ����� ���������� ���-�� � ����� �����.
    public void Draw(Vector3 _position, Texture2D _overlayTexture, float _overlayTexture_scaleMultiplier_x = 1, float _overlayTexture_scaleMultiplier_y = 1)
    {
        Vector3[] _positions = new Vector3[1] { _position };
        Draw(_positions, _overlayTexture, _overlayTexture_scaleMultiplier_x, _overlayTexture_scaleMultiplier_y);
    }

    //���� ����� ���������� ���-�� � ��������� ������.
    public void Draw(Vector3[] _positions, Texture2D _overlayTexture, float _overlayTexture_scaleMultiplier_x = 1, float _overlayTexture_scaleMultiplier_y = 1)
    {
        switch (ControlPers_BuildSettings.SingleOnScene.PlatformType_Current)
        {
            case ControlPers_BuildSettings.PlatformType.windows:
                material_mixer.SetTexture("_OverlayTex", _overlayTexture);
                material_mixer.SetFloat("_OverlayRotation", angle);
                var _overlayTexture_scale_x = ((float)_overlayTexture.width / texture.width) * _overlayTexture_scaleMultiplier_x;
                var _overlayTexture_scale_y = ((float)_overlayTexture.height / texture.height) * _overlayTexture_scaleMultiplier_y;
                material_mixer.SetFloat("_OverlayScaleX", _overlayTexture_scale_x);
                material_mixer.SetFloat("_OverlayScaleY", _overlayTexture_scale_y);

                RenderTexture _texture_rt_temp = RenderTexture.GetTemporary(texture.width, texture.height); //�������� ��������� ��������������
                Graphics.CopyTexture(texture, _texture_rt_temp);

                foreach (var _position in _positions)
                {
                    var _pos_x = (collision.bounds.size.x - (collision.bounds.max.x - _position.x)) / collision.bounds.size.x;
                    var _pos_y = (collision.bounds.size.y - (collision.bounds.max.y - _position.y)) / collision.bounds.size.y;

                    material_mixer.SetFloat("_PositionX", _pos_x - 0.5f * _overlayTexture_scale_x);
                    material_mixer.SetFloat("_PositionY", _pos_y - 0.5f * _overlayTexture_scale_y);
                    Graphics.Blit(texture, _texture_rt_temp, material_mixer);   //�������� �������� �������� �� ��������� ��������������, ��� ������ �������
                }

                Graphics.CopyTexture(_texture_rt_temp, texture); //������� ������������ ��� "texture.ReadPixels(); texture.Apply();"
                RenderTexture.ReleaseTemporary(_texture_rt_temp);           //����������� ������ �� ��������� ��������������. ������ �� ������ ������
            break;
            case ControlPers_BuildSettings.PlatformType.web_yandexGames_desktop:                
                var _rect = new Rect(0, 0, _overlayTexture.width, _overlayTexture.height);
                var _sprite = Sprite.Create(_overlayTexture, _rect, Vector2.one / 2); //������ ����� ������
                var _scale = new Vector3(1 / transform.localScale.x * _overlayTexture_scaleMultiplier_x, 1 / transform.localScale.y * _overlayTexture_scaleMultiplier_y, transform.localScale.z) * WEB_PAINTING_LOCALSCALE;

                foreach (var _position in _positions)
                {
                    var _gameObject = new GameObject();                                       //������ ����� ������
                    var __gameObject_sr = _gameObject.gameObject.AddComponent<SpriteRenderer>(); //��������� � ���� ��������������
                    __gameObject_sr.sprite = _sprite;                                          //��� � �������������� ����� ������
                    __gameObject_sr.sortingLayerName = WEB_PAINTING_SORTINGLAYERNAME;          //��������� ���� ���������� (��� ���� �� ���������)
                    _gameObject.transform.parent = transform;                                 //��������� ���� ������� ������������ ��������
                    _gameObject.transform.position = _position;                               //��������� ����� ������ ���, ��� ����                    
                    _gameObject.transform.localScale = _scale;                                //����� ������ ������
                    Destroy(_gameObject, WEB_PAINTING_TIMETOLIVE);                            //��������� �������������� �������
                }
            break;
        }
    }

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        material.color = Color.white; //����������� ��, ��� ������� ����� �������
    }
}
