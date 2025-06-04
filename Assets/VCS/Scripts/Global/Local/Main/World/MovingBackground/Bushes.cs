using UnityEngine;

public class World_MovingBackground_Bushes : World_MovingBackground_Parent
{
    protected override void Awake()
    {
        base.Awake();
        Speed = 7.5f;
    }
}
