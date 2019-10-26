using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throttle : Module
{
    Train TrainScript;
    Transform Lever;
    // Start is called before the first frame update
    new void Start()
    {
        TrainScript = GameObject.Find("Train").GetComponent<Train>();
        Lever = transform.Find("Lever");
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (base.Active) {
            TrainScript.SetThrottle(TrainScript.GetThrottle()+Input.GetAxis("HorizontalArrow")*Time.deltaTime*.1f);
        }
        float rotation = Mathf.Lerp(20,160,TrainScript.GetThrottleRatio());
        Lever.transform.localRotation = Quaternion.Euler(rotation,90,-90);
    }
}
