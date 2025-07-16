using UnityEngine;

public class World_Local_SceneMain_Cops_Lights : MonoBehaviour
{
    public static World_Local_SceneMain_Cops_Lights SingleOnScene { get; private set; }

    private Animator animation_animator;
    public void Animation_On()
    {
        animation_animator.speed = 1;
    }
    public void Animation_Off()
    {
        animation_animator.speed = 0;
    }

    private bool light_active = false;
    public void Light_On()
    {
        light_active = true;
    }
    public void Light_Off()
    {
        light_active = false;

        for (var _i = 0; _i < light_blue_components.Length; ++_i)
        {
            light_blue_components[_i].enabled = false;
        }

        for (var _i = 0; _i < light_red_components.Length; ++_i)
        {
            light_red_components[_i].enabled = false;
        }
    }

    [SerializeField] GameObject[] light_blue;
    Light[] light_blue_components = new Light[3];
    private void Light_Blue_On()
    {
        if (light_active)
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
    }

    [SerializeField] GameObject[] light_red;
    Light[] light_red_components = new Light[3];
    private void Light_Red_On()
    {
        if (light_active)
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
    }

    private void Awake()
    {
        SingleOnScene = this;

        animation_animator = GetComponent<Animator>();

        for (var _i = 0; _i < light_blue.Length; ++_i)
        {
            light_blue_components[_i] = light_blue[_i].GetComponent<Light>();
        }

        for (var _i = 0; _i < light_red.Length; ++_i)
        {
            light_red_components[_i] = light_red[_i].GetComponent<Light>();
        }

        Light_Off();
    }
}
