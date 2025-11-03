using UnityEngine;
using Utils;

public class World_Local_SceneMain_DriftSection_Bush : MonoBehaviour
{
    [SerializeField] Texture2D normalMap;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().material.SetTexture(Constants.MATERIAL_BUMPMAP_U_BUMPMAP, normalMap);
    }
}
