using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    Renderer Renderer;

    public GameObject mainCamera;

    Camera cameraComponent;
    CameraControl cameraControl;

    public Animator animator;

    public Transform topL;
    public Transform botR;

    int birdId;

    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        birdId = Random.Range(1, 5);
        cameraComponent = mainCamera.GetComponent<Camera>();
        cameraControl = mainCamera.GetComponent<CameraControl>();
    }


    // Update is called once per frame
    void Update()
    {
        animator.Play("Bird" + birdId);
        Vector3 pos = transform.position;
        pos.x += -1 * Time.deltaTime;
        transform.position = pos;

        Vector2 myTL = cameraComponent.WorldToScreenPoint(topL.position);
        Vector2 myBR = cameraComponent.WorldToScreenPoint(botR.position);

        Vector2 tarTL = cameraControl.cursorTopRight();
        Vector2 tarBR = cameraControl.cursorBottomRight();

        if (tarTL.x < myTL.x && tarTL.y > myTL.y && tarBR.x > myBR.x && tarBR.y < myBR.y)
        {
            sprite.color = new Color(0, 1, 0);
        }
        else
        {
            sprite.color = new Color(1, 1, 1);
        }
    }
}
