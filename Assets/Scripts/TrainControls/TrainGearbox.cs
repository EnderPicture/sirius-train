﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainGearbox : Module
{
    public readonly int P = 0;
    public readonly int R = 1;
    public readonly int D = 2;
    TextMeshPro StateText;

    bool LastPushed = false;

    public Train Train;

    Transform Lever;

    // gear 0 = park
    // gear 1 = reverse
    // gear 2 = forward

    float LeverRotation;



    int Gear;
    // Start is called before the first frame update
    new void Start()
    {
        Animate = 2;
        Gear = D;
        StateText = transform.Find("Panel").Find("StateText").GetComponent<TextMeshPro>();

        Lever = transform.Find("Lever");
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Interaction();
        LeverControl();
    }

    void Interaction()
    {
        if (base.Active)
        {
            float value = Input.GetAxisRaw("HorizontalArrow");
            bool pushed = false;
            if (Mathf.Abs(value) > 0)
            {
                pushed = true;
            }
            if (pushed && !LastPushed)
            {
                if (Train.GetSpeed() == 0)
                {
                    if (value > 0)
                    {
                        Gear++;
                    }
                    else if (value < 0)
                    {
                        Gear--;
                    }
                    Gear = Mathf.Clamp(Gear, 0, 2);
                }
                if (!Train.IsInStation() && (Gear == R || Gear == P))
                {
                    Gear = D;
                    // TODO say that something is wrong
                }

            }
            LastPushed = pushed;
        }
    }

    public float GetGear()
    {
        return Gear;
    }

    void LeverControl()
    {
        float rotation = 0;
        if (Gear == P)
        {
            StateText.SetText("P");
            rotation = Mathf.Lerp(20, -20, 1);

        }
        else if (Gear == R)
        {
            StateText.SetText("R");
            rotation = Mathf.Lerp(20, -20, .5f);

        }
        else if (Gear == D)
        {
            StateText.SetText("D");
            rotation = Mathf.Lerp(20, -20, 0);

        }

        LeverRotation = Mathf.Lerp(LeverRotation, rotation, Time.deltaTime * 10);
        Lever.transform.localRotation = Quaternion.Euler(0, 0, LeverRotation);

    }
    public override float GetStatus()
    {
        if (Gear == D)
        {
            return 1;
        }
        if (Gear == R)
        {
            return .5f;
        }
        if (Gear == P)
        {
            return 0;
        }
        return 0;
    }
}
