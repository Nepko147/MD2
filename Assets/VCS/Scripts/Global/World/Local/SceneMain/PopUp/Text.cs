using UnityEngine;
using UnityEngine.UI;

public class World_Local_SceneMain_PopUp_Text : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Text>().font.material.mainTexture.filterMode = FilterMode.Point;
    }
}
