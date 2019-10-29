using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : Module
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
            TrainScript.SetBreak(TrainScript.GetBreak()+Input.GetAxis("HorizontalArrow")*Time.deltaTime*.1f);
        }
        float rotation = Mathf.Lerp(70,-70,TrainScript.GetBreakRatio());
        Lever.transform.localRotation = Quaternion.Euler(0,0,rotation);
    }
}
