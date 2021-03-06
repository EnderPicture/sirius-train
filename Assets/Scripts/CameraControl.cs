﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Camera controls")]
    public Transform target;
    public float smoothFactor = 10f;
    public Vector3 offset;
    public Vector3 regularPosition;
    public static readonly int MODE_SHOOT = 1;
    public static readonly int MODE_CAMERA = 0;
    public Train train;

    [Header("Shoot Feature")]
    public Animation AnimationController;
    public GameObject pointer;
    public Transform topLeft;
    public Transform bottomRight;


    float zoom = 1;
    float MinZoom = 30f;
    float maxZoom = 3.46f;
    Camera cameraComp;



    int CameraMode = MODE_CAMERA;
    private void Start() {
        cameraComp = GetComponent<Camera>();
        regularPosition = transform.position;
    }
    private void LateUpdate()
    {

        Vector3 screenPos1 = cameraComp.WorldToScreenPoint(topLeft.position);
        Vector3 screenPos2 = cameraComp.WorldToScreenPoint(bottomRight.position);

        zoom += Input.GetAxisRaw("VerticalArrow") * Time.deltaTime * .5f;
        if (zoom > 1)
        {
            zoom = 1;
        }
        if (zoom < 0)
        {
            zoom = 0;
        }
        if (zoom < .5)
        {
            CameraMode = MODE_SHOOT;
        }
        else
        {
            CameraMode = MODE_CAMERA;
        }

        if (CameraMode == MODE_SHOOT)
        {
            offset.x += Input.GetAxisRaw("HorizontalArrow") * Time.deltaTime * 2;
            offset.x = Mathf.Clamp(offset.x, -28f,7f);

            Vector3 position = pointer.transform.localPosition;
            position.x += Input.GetAxisRaw("Horizontal") * Time.deltaTime * 2f;
            position.y += Input.GetAxisRaw("Vertical") * Time.deltaTime * 2f;

            position.x = Mathf.Clamp(position.x, -1.26f, 1.26f);
            position.y = Mathf.Clamp(position.y, -0.716f, 0.716f);

            pointer.transform.localPosition = position;

            AnimationController.Play("FadeIn");
        }
        else
        {
            offset.x = 0;
            AnimationController.Play("FadeOut");
        }

        offset.z = Mathf.Lerp(-MinZoom, -maxZoom, zoom);
        offset.y = Mathf.Lerp(1.5f, 0, zoom);

        Vector3 shake = new Vector3();
        float deltaToSpeedLimit = train.GetSpeedLimit() - train.GetVisualSpeed();
        if ( deltaToSpeedLimit < 0 )
        {
            shake.x = (Mathf.PerlinNoise(Time.realtimeSinceStartup*15,0)-.5f);
            shake.y = (Mathf.PerlinNoise(0,Time.realtimeSinceStartup*15)-.5f);
            shake *= Mathf.Clamp(.003f * -deltaToSpeedLimit, 0, .1f);
        }
        Vector3 smoothedPos = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
        transform.position = smoothedPos + shake;
    }

    public Vector2 cursorTopRight() {
        return cameraComp.WorldToScreenPoint(topLeft.position); 
    }
    public Vector2 cursorBottomRight() {
        return cameraComp.WorldToScreenPoint(bottomRight.position); 
    }

    public int getCameraMode()
    {
        return CameraMode;
    }
}
