﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Brake : Module
{
    Train TrainScript;
    Transform Lever;

    TextMeshPro StateText;
    // Start is called before the first frame update
    new void Start()
    {
        StateText = transform.Find("Panel").Find("StateText").GetComponent<TextMeshPro>();

        TrainScript = GameObject.Find("Train").GetComponent<Train>();
        Lever = transform.Find("Lever");
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (base.Active) {
            TrainScript.SetBrake(TrainScript.GetBrake()+Input.GetAxisRaw("HorizontalArrow")*Time.deltaTime*.4f);
        }
        StateText.SetText(Mathf.Round(TrainScript.GetBrakeRatio()*100)+"%");
        float rotation = Mathf.Lerp(70,-70,TrainScript.GetBrakeRatio());
        Lever.transform.localRotation = Quaternion.Euler(0,0,rotation);
    }
}