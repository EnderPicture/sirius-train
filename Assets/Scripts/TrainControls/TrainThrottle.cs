using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainThrottle : Module
{
    Transform Lever;
    TextMeshPro StateText;

    float MaxThrottle = .5f;
    float MinThrottle = 0;
    float Throttle = 0;

    public Train Train;

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
        StatusText();
        LeverControl();
    }

    void LeverControl()
    {
        float rotation = Mathf.Lerp(70, -70, Ratio());
        Lever.transform.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    void StatusText()
    {
        string text = Mathf.Round(Ratio() * 100) + "%";
        if (Throttle > 0)
        {
            if (Train != null)
            {
                if (Train.GetMode() == Config.DIESEL)
                {
                    text += "\n using " + Mathf.Round(Throttle) + " L/s";
                }
                else if (Train.GetMode() == Config.STEAM)
                {
                    text += "\n using " + Mathf.Round(Throttle * 2 * 131.0f) + " kpa/s";
                }
            }
        }
        StateText.SetText(text);
    }

    void Interaction()
    {
        if (base.Active)
        {
            Throttle += Input.GetAxisRaw("HorizontalArrow") * Time.deltaTime * .4f;
            Throttle = Mathf.Clamp(Throttle, MinThrottle, MaxThrottle);
        }
    }

    float Ratio()
    {
        return Throttle / MaxThrottle;
    }

    public float GetThrottleValue()
    {
        return Throttle;
    }
}
