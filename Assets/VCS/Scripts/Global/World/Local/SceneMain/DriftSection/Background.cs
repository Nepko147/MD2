using UnityEngine;
using Utils;

public class World_Local_SceneMain_DriftSection_Background : MonoBehaviour
{
    [SerializeField] private Texture2D normalMap;
    [SerializeField] private World_General_DrawableSurface drawableSurface;
    
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, normalMap);
        
        var _size_x = (int)spriteRenderer.size.x;
        var _size_y = (int)spriteRenderer.size.y;
        var _textureWidth = spriteRenderer.sprite.texture.width * _size_x;
        var _textureHeight = spriteRenderer.sprite.texture.height * _size_y;

        drawableSurface.transform.localScale = new Vector2(_size_x, _size_y);
        drawableSurface.transform.localPosition = Vector3.down * _size_y / 2; // Костыль, так как pivot у бэкграунда не по центру.

        drawableSurface.Texture_Refresh(_textureWidth, _textureHeight);
    }

    private void Update()
    {
        var _player = World_Local_SceneMain_Player_Entity.SingleOnScene;        

        if (spriteRenderer.bounds.Intersects(_player.SpriteRenderer.bounds)) //Нужно для того, чтобы рисование происходило только когда игрок на поврхности
        {
            if (_player.Tyres_IsDrawing)
            {
                drawableSurface.Draw(_player.Tyres_Position, _player.Tyres_Texture);
            }
        }
    }
}
