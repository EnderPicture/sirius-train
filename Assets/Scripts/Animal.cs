using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    Renderer Renderer;

    public GameObject mainCamera;

    Camera cameraComponent;
    CameraControl cameraControl;
    // Start is called before the first frame update
    void Start()
    {
        cameraComponent = mainCamera.GetComponent<Camera>();
        cameraControl = mainCamera.GetComponent<CameraControl>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
