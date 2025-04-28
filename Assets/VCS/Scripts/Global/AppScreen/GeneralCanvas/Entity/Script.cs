using System;
using UnityEngine;
using UnityEngine.Rendering;

public class AppScreen_GeneralCanvas_Entity : MonoBehaviour
{
    public static AppScreen_GeneralCanvas_Entity Singleton { get; private set; }

    public Camera canvas_camera;
    
    private void Awake()
    {
        Singleton = this;
        
        canvas_camera = GetComponent<Canvas>().worldCamera;
    }
}
