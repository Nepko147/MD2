using UnityEngine;

public class World_MovingBackground_Road : World_MovingBackground_Parent
{
    private new void Awake()
    {
        base.Awake();
        Speed = 10;
    }
}
