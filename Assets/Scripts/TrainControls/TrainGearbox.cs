using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainGearbox : Module
{
    TextMeshPro StateText;

    bool LastPushed = false;

    public Train Train;

    Transform Lever;

    // gear 0 = park
    // gear 1 = reverse
    // gear 2 = forward

    float LeverRotation;

    int Gear = 0;
    // Start is called before the first frame update
    new void Start()
    {
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

            }
            LastPushed = pushed;
        }
    }

    void LeverControl()
    {
        float rotation = 0;
        if (Gear == 0)
            rotation = Mathf.Lerp(70, -70, 1);
        else if (Gear == 1)
            rotation = Mathf.Lerp(70, -70, .5f);
        else if (Gear == 2)
            rotation = Mathf.Lerp(70, -70, 0);

        LeverRotation = Mathf.Lerp(LeverRotation, rotation, Time.deltaTime * 10);
        Lever.transform.localRotation = Quaternion.Euler(0, 0, LeverRotation);
    }
}
