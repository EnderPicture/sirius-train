using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureRelease : Module
{
    // Start is called before the first frame update    Train TrainScript;
    public TrainPressure Pressure;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (base.Active) {
            if (Input.GetAxisRaw("HorizontalArrow") < 0) {
                Pressure.ReleasePressure(-Input.GetAxisRaw("HorizontalArrow")*Time.deltaTime*2);
            }
        }
    }
}
