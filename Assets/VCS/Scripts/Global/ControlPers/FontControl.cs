using UnityEngine;

public class FontControl : MonoBehaviour
{
    void Start()
    {
        this.gameObject.GetComponent<UnityEngine.UI.Text>().font.material.mainTexture.filterMode = FilterMode.Point;
    }
}
