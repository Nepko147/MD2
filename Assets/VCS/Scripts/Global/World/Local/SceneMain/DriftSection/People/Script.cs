using UnityEngine;
using System.Collections;

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

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        ControlScene_Main.SingleOnScene.Audio_Sound_Mental_Play();
        World_Local_SceneMain_Player_Entity.SingleOnScene.Collision_Hit_Soft(transform.position);

        IEnumerator Disappearance()
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            spriteRenderer.material.SetFloat("_ShakePower", 1);
            spriteRenderer.material.SetFloat("_ShakeRate", 1);

            var _currentAplpha = 1.0f;
            var _currentAplpha_step = 2.0f;

            while (true)
            {
                spriteRenderer.material.SetFloat("_Alpha", _currentAplpha);                

                if (_currentAplpha > 0)
                {
                    _currentAplpha -= _currentAplpha_step * Time.deltaTime;
                    yield return null;
                }
                else
                {                    
                    Destroy(gameObject);
                    break;
                }
            }
        }

        var _routine = Disappearance();
        StartCoroutine(_routine);
    }
}
