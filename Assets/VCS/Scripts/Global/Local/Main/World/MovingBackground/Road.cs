using UnityEngine;

public class World_MovingBackground_Road : World_MovingBackground_Parent
{
    protected override void Awake()
    {
        base.Awake();
        Speed = 10;
    }
}
