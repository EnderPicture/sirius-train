﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Animal : MonoBehaviour
{
    Renderer Renderer;

    public Train train;

    public GameObject mainCamera;

    Camera cameraComponent;
    CameraControl cameraControl;

    public Animator animator;

    public Transform topL;
    public Transform botR;

    int birdId;
    float speed = 0;
    float minSpeed = .5f;
    float height;

    public float baseSpeed;

    public SpriteRenderer sprite;
    public SpriteRenderer arrow;

    float randNumber;

    bool scanned = false;


    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        randNumber = Random.Range(0, 100);

        speed = Random.Range(-2.5f, -.5f);
        speed += baseSpeed;

        if (speed > 0 && speed < minSpeed)
        {
            speed = minSpeed;
        }
        else if (speed < 0 && speed > -minSpeed)
        {
            speed = -minSpeed;
        }

        if (speed > 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        birdId = Random.Range(1, 5);
        height = Random.Range(0.585f, 4.041f);
        cameraComponent = mainCamera.GetComponent<Camera>();
        cameraControl = mainCamera.GetComponent<CameraControl>();
    }


    // Update is called once per frame
    void Update()
    {
        animator.Play("Bird" + birdId);
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        pos.y = height + Mathf.Sin(Time.realtimeSinceStartup + randNumber) * .3f;
        transform.position = pos;

        Vector2 myTL = cameraComponent.WorldToScreenPoint(topL.position);
        Vector2 myBR = cameraComponent.WorldToScreenPoint(botR.position);

        Vector2 tarTL = cameraControl.cursorTopRight();
        Vector2 tarBR = cameraControl.cursorBottomRight();

        if (!scanned &&
            cameraControl.getCameraMode() == CameraControl.MODE_SHOOT &&
            tarTL.x < myBR.x && tarTL.y > myBR.y &&
            tarBR.x > myTL.x && tarBR.y < myTL.y)
        {
            if (Input.GetKeyDown("space"))
            {
                scanned = true;
                //player camera shutter sound
                FindObjectOfType<AudioManager2>().Play("player", 0);
                train.addScanScore();
                arrow.DOFade(0,.25f);
            }
            sprite.DOColor(new Color(.7f, 1f, .7f), .25f);
        }
        else
        {
            if (scanned)
            {
                sprite.DOColor(new Color(0, 1, 0), .25f);
            }
            else
            {
                sprite.DOColor(new Color(1, 1, 1), .25f);
            }
        }
    }
}
