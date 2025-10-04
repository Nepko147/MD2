using UnityEngine;

public class World_Local_SceneMain_DriftSection_Point_Parent : MonoBehaviour
{
    #if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.025f);
    }

    #endif
}
