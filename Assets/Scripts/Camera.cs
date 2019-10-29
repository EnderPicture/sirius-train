using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public float smoothFactor = 10f;
    float zoom = 1;
    float MinZoom = 30f;
    float maxZoom = 3.46f;
    public Vector3 offset;
    private void LateUpdate()
    {

        zoom += Input.GetAxis("VerticalArrow") * Time.deltaTime * .5f;
        if (zoom > 1)
        {
            zoom = 1;
        }
        if (zoom < 0)
        {
            zoom = 0;
        }
        offset.z = Mathf.Lerp(-MinZoom, -maxZoom, zoom);
        Vector3 smoothedPos = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
        transform.position = smoothedPos;

    }
}
