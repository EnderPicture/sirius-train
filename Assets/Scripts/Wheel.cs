using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public Transform locator;
    // Start is called before the first frame update
    float circumference;
    void Start()
    {
        circumference =  2*Mathf.PI*(locator.position-transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0,0,-(transform.position.x/circumference)*360);
    }
}
