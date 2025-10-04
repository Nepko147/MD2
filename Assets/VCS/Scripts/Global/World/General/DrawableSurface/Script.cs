using UnityEngine;

public class World_General_DrawableSurface : MonoBehaviour
{
    private Material material;
    [SerializeField] Material material_mixer;
    [SerializeField] Texture2D additionalTexture;
    [SerializeField] private Collider collision;
    [SerializeField] float angle;

    private Texture2D texture;
    private RenderTexture texture_rt;
    [SerializeField] private TextureWrapMode texture_wrapMode;
    [SerializeField] private FilterMode texture_filterMode;

    private int texture_size_x;
    private int texture_size_y;

    //Если просто хотим обновить(очистить) тектуру
    public void Texture_Refresh()
    {
        Texture_Refresh(texture_size_x, texture_size_y);
    }

    //Если хотим поменять размер тектуры
    public void Texture_Refresh(int _newWidth, int _newHeight)
    {
        texture_size_x = _newWidth;
        texture_size_y = _newHeight;

        texture = new Texture2D(texture_size_x, texture_size_y); //Объявляем новую тектуру
        texture_rt = new RenderTexture(texture_size_x, texture_size_y, 32);

        Color[] _color_Clear = new Color[texture_size_x * texture_size_y]; //Создаём массив для пустых пикселей

        for (int _i = 0; _i < _color_Clear.Length; _i++)
        {
            _color_Clear[_i] = Color.clear; //Заполняем массив пустыми пикселями
        }

        texture.SetPixels(_color_Clear); //Заполняем текстуру пустыми пикселями из массива

        texture.wrapMode = texture_wrapMode;
        texture.filterMode = texture_filterMode;
        material.mainTexture = texture;
        texture.Apply();
    }

    //Если хотим отрисовать что-то в одном месте. НЕ ВЫЗЫВАТЬ В ЦИКЛЕ!!!
    public void Draw(Vector3 _position, Texture2D _overlayTexture, float _overlayTexture_scaleMultiplier_x = 1, float _overlayTexture_scaleMultiplier_y = 1)
    {
        Vector3[] _positions = new Vector3[1] { _position };
        Draw(_positions, _overlayTexture, _overlayTexture_scaleMultiplier_x, _overlayTexture_scaleMultiplier_y);
    }

    //Если хотим нарисовать что-то в нескльких местах. НЕ ВЫЗЫВАТЬ В ЦИКЛЕ!!! 
    public void Draw(Vector3[] _positions, Texture2D _overlayTexture, float _overlayTexture_scaleMultiplier_x = 1, float _overlayTexture_scaleMultiplier_y = 1)
    {
        material_mixer.SetTexture("_OverlayTex", _overlayTexture);
        material_mixer.SetFloat("_OverlayRotation", angle);
        var _overlayTexture_scale_x = ((float)_overlayTexture.width / texture.width) * _overlayTexture_scaleMultiplier_x;
        var _overlayTex_scale_y = ((float)_overlayTexture.height / texture.height) * _overlayTexture_scaleMultiplier_y;
        material_mixer.SetFloat("_OverlayScaleX", _overlayTexture_scale_x);
        material_mixer.SetFloat("_OverlayScaleY", _overlayTex_scale_y);

        foreach (var _position in _positions)
        {
            var _pos_x = (collision.bounds.size.x - (collision.bounds.max.x - _position.x)) / collision.bounds.size.x;
            var _pos_y = (collision.bounds.size.y - (collision.bounds.max.y - _position.y)) / collision.bounds.size.y;

            material_mixer.SetFloat("_PositionX", _pos_x - 0.5f * _overlayTexture_scale_x);
            material_mixer.SetFloat("_PositionY", _pos_y - 0.5f * _overlayTex_scale_y);
            Graphics.Blit(texture, texture_rt, material_mixer);
        }

        texture.ReadPixels(new Rect(0, 0, texture_size_x, texture_size_y), 0, 0);   // Вызов больше одного раза за кадр - путь к 0 ФПС

        texture.Apply();// Применяем
    }

    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        material.color = Color.white; //Гарантируем то, что сурфейс будет видимым        
    }
}
