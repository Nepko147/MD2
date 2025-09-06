using UnityEngine;

public class AppScreen_General_Camera_World_Entity_Zoom : MonoBehaviour    
{
    public static AppScreen_General_Camera_World_Entity_Zoom SingleOnScene { get; private set; }

    public bool Active { get; set; }

    private Camera camera_component;

    private bool zoom_swap = false;
    private float zoom_min;
    private float zoom_max;
    private const float ZOOM_OFS = 1f;
    private const float ZOOM_SPEED = 0.1f;
    private const float ZOOM_DELAY_INIT = 1f;    
    private float zoom_delay_current = ZOOM_DELAY_INIT;

    private void Awake()
    {
        SingleOnScene = this;

        Active = false;

        camera_component = GetComponent<Camera>();

        zoom_max = camera_component.fieldOfView;
        zoom_min = zoom_max - ZOOM_OFS;
    }

    private void Update()
    {       
        if (Active)
        {
            if (!zoom_swap)
            {
                if (zoom_delay_current > 0)
                {
                    zoom_delay_current -= Time.deltaTime;
                }
                else
                {
                    camera_component.fieldOfView -= ZOOM_SPEED * Time.deltaTime;

                    if (camera_component.fieldOfView <= zoom_min)
                    {
                        zoom_delay_current = ZOOM_DELAY_INIT;
                        zoom_swap = true;
                    }
                }
            }
            else
            {
                if (zoom_delay_current > 0)
                {
                    zoom_delay_current -= Time.deltaTime;
                }
                else
                {
                    camera_component.fieldOfView += ZOOM_SPEED * Time.deltaTime;

                    if (camera_component.fieldOfView >= zoom_max)
                    {
                        zoom_delay_current = ZOOM_DELAY_INIT;
                        zoom_swap = false;
                    }
                }
            }
        }
    }
}
