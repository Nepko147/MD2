using UnityEngine;
using Utils;

public class World_Local_SceneMain_DriftSection_People : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] spriteArray;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int _number = Random.Range(0, spriteArray.Length);
        var _sprite = spriteArray[_number];
        spriteRenderer.sprite = _sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
