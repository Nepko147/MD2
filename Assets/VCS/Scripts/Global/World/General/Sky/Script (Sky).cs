using UnityEngine;

public class World_General_Sky : MonoBehaviour
{
    public static World_General_Sky SingleOnScene { get; private set; }

    public bool Active { get; set; }

    private const float Y_MAX = 1f;
    private const float Y_MIN = 0.5f;
    private const float SPEED = 0.15f;

    private bool state = true;

    private MeshRenderer meshRenderer;

    public Color OverlayColor
    {
        get
        {
            return meshRenderer.material.GetColor("_OverlayColor");
        }
        set
        {
            meshRenderer.material.SetColor("_OverlayColor", value);
        }
    }

    public void OverlayColor_Ratio_Set(float _ratio)
    {
        _ratio = Mathf.Clamp(_ratio, 0.0f, 1.0f);
        meshRenderer.material.SetFloat("_OverlayColorRatio", _ratio);
    }

    private void Awake()
    {
        SingleOnScene = this;

        Active = false;

        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (Active)
        {
            if (state)
            {
                transform.position += Vector3.up * SPEED * Time.deltaTime;

                if (transform.position.y >= Y_MAX)
                {
                    state = false;
                }
            }
            else
            {
                transform.position -= Vector3.up * SPEED * Time.deltaTime;

                if (transform.position.y <= Y_MIN)
                {
                    state = true;
                }
            }
        }
    }
}
