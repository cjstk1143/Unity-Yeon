using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankGizmo : MonoBehaviour
{
    public Color myColor = Color.green;
    public float _radius = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = myColor;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
