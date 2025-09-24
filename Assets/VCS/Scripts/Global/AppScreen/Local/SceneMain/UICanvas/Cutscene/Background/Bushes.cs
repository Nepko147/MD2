using UnityEngine;

public class AppScreen_Local_SceneMain_UICanvas_Cutscene_Background_Bushes : MonoBehaviour
{
    public bool Active { get; set; }

    private float WIDTH = 720f;
    private float SPEED = 300f;

    private void Update()
    {
        if (Active)
        {
            transform.localPosition += Vector3.left * SPEED * Time.deltaTime;

            if (transform.localPosition.x <= -WIDTH)
            {
                transform.localPosition = new Vector2(transform.localPosition.x + (WIDTH * 2), transform.localPosition.y);
            }
        }
    }
}
