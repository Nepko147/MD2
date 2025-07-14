using UnityEngine;

public class World_Local_SceneMain_Cops_Lights_Sprite : MonoBehaviour
{
    [SerializeField] GameObject[] light_blue;
    Light[] light_blue_components = new Light[3];
    private void Light_Blue_On()
    {
        for (var _i = 0; _i < light_blue_components.Length; ++_i)
        {
            light_blue_components[_i].enabled = true;
        }

        for (var _i = 0; _i < light_red_components.Length; ++_i)
        {
            light_red_components[_i].enabled = false;
        }
    }

    [SerializeField] GameObject[] light_red;
    Light[] light_red_components = new Light[3];
    private void Light_Red_On()
    {
        for (var _i = 0; _i < light_blue_components.Length; ++_i)
        {
            light_blue_components[_i].enabled = false;
        }

        for (var _i = 0; _i < light_red_components.Length; ++_i)
        {
            light_red_components[_i].enabled = true;
        }
    }

    private void Awake()
    {
        for (var _i = 0; _i < light_blue.Length; ++_i)
        {
            light_blue_components[_i] = light_blue[_i].GetComponent<Light>();
        }

        for (var _i = 0; _i < light_red.Length; ++_i)
        {
            light_red_components[_i] = light_red[_i].GetComponent<Light>();
        }
    }
}
