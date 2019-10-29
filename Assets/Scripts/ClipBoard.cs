using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipBoard : MonoBehaviour
{
    public Transform target;
    public float smoothFactor = 10f;
    Vector3 offset;

    public float offRot = 50;
    public float onRot = 7.83f;

    public bool show = false;

    public GameObject WinScreen;
    public GameObject MenuScreen;


    private void Start() {
        offset = transform.position - target.transform.position;
    }
    private void LateUpdate()
    {
        Vector3 rot = transform.eulerAngles;
        float rotZ = transform.eulerAngles.z;
        if (show) {
            rotZ = Mathf.Lerp(rotZ, onRot, smoothFactor * Time.deltaTime * .5f);
        } else {
            rotZ = Mathf.Lerp(rotZ, offRot, smoothFactor * Time.deltaTime * .5f);
        }
        rot.z = rotZ;
        transform.eulerAngles = rot;

        Vector3 smoothedPos = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
        transform.position = smoothedPos;
    }

    public void ShowWin() {
        WinScreen.SetActive(true);
        MenuScreen.SetActive(false);
        show = true;
    }

    public void ClickedPlay() {
        show = false;
    }
}
