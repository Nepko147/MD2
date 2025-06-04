using UnityEngine;

public class AppScreen_UICanvas_Button_Parent : AppScreen_UICanvas_Parent
{
    public bool Pressed { get; set; }

    private AudioSource audioSource;

    public void OnClick()
    {
        Pressed = true;

        audioSource.Play();
    }

    protected override void Awake()
    {
        base.Awake();

        Pressed = false;

        audioSource = GetComponent<AudioSource>();
    }
}
