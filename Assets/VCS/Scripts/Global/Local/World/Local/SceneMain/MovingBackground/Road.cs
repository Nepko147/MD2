using UnityEngine;

public class World_Local_SceneMain_MovingBackground_Road : World_Local_SceneMain_MovingBackground_Parent
{
    protected override void Awake()
    {
        base.Awake();
        Speed = 10;
    }
}
