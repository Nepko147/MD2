using UnityEngine;

public class AppScreen_Misc_FontControl : MonoBehaviour
{
    void Start()
    {
        this.gameObject.GetComponent<UnityEngine.UI.Text>().font.material.mainTexture.filterMode = FilterMode.Point;
    }
}
