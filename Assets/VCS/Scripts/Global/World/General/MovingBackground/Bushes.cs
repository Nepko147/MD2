using UnityEngine;

public class World_General_MovingBackground_Bushes : World_General_MovingBackground_Parent
{
    protected override void Awake()
    {
        base.Awake();
        Speed = 375f;
    }
}
