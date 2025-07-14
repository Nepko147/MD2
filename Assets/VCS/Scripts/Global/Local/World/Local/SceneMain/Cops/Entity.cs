using UnityEngine;

public class World_Local_SceneMain_Cops_Entity : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] Texture2D normalMap;

    //private float speed = 8f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material.SetTexture("_BumpMap", normalMap);

        transform.position += Vector3.right * 8f; //Отступ за пределы экрана
    }
}
