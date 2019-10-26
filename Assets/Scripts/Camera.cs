using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float smoothFactor = 10f;
    public Vector3 offset;
    private void LateUpdate() {
        Vector3 smoothedPos = Vector3.Lerp(transform.position,target.position+offset,smoothFactor*Time.deltaTime);
        transform.position = smoothedPos;
    }
}
