using UnityEngine;

public class World_MovingBackground_Bushes : World_MovingBackground_Parent
{
    private new void Awake()
    {
        base.Awake();
        Speed = 7.5f;
    }
}
