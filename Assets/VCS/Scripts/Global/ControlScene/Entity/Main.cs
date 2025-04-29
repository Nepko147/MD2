using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class ControlScene_Entity_Main : MonoBehaviour
{
    private PostProcessVolume postProcessVoolume;
    private DepthOfField depthOfField;

    private void Start()
    {
        postProcessVoolume = AppScreen_Camera_MainCameraZoom.Singletone.GetComponent<PostProcessVolume>();
        postProcessVoolume.profile.TryGetSettings(out depthOfField);
        depthOfField.aperture.value = 3;
    }
}
