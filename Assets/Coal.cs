using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coal : MonoBehaviour
{
    public Transform target = null;
    public float smoothFactor = 10f;
    public Vector3 offset;

    Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    private void LateUpdate()
    {
        if (target != null) {
            rb.isKinematic = true;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
            transform.position = smoothedPos;
        } else{
            rb.isKinematic = false;
        }
    }
}
