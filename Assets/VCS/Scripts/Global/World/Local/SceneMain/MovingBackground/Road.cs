using UnityEngine;
using Utils;

public class World_Local_SceneMain_MovingBackground_Road : World_Local_SceneMain_MovingBackground_Parent
{    
    [SerializeField] private Texture2D normalMap;
    [SerializeField] private World_General_DrawableSurface drawableSurface;

    public void DrawableSurface_Refresh()
    {
        drawableSurface.Texture_Refresh();
    }

    private SpriteRenderer spriteRenderer;

    public const float SPEED = 500f;

    protected override void Awake()
    {
        base.Awake();

        Speed = SPEED;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, normalMap);
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, normalMap);

        var _size_x = (int)spriteRenderer.size.x;
        var _size_y = (int)spriteRenderer.size.y;
        var _textureWidth = spriteRenderer.sprite.texture.width * _size_x;
        var _textureHeight = spriteRenderer.sprite.texture.height * _size_y;
        
        drawableSurface.Texture_Refresh(_textureWidth, _textureHeight);
    }

    protected override void Update()
    {
        base.Update();

        var _player = World_Local_SceneMain_Player_Entity.SingleOnScene;
        
        if (_player != null)
        {
            if (spriteRenderer.bounds.Intersects(_player.SpriteRenderer.bounds)) //Нужно для того, чтобы рисование происходило только когда игрок на поврхности
            {
                if (_player.Tyres_IsDrawing)
                {
                    drawableSurface.Draw(_player.Tyres_Position, _player.Tyres_Texture, 2);
                }
            }
        }
    }
}
