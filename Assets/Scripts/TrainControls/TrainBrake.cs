using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainBrake : Module
{
    Transform Lever;
    TextMeshPro StateText;

    float MaxBrake = .5f;
    float MinBrake = 0;
    float Brake = 0;

    // Start is called before the first frame update
    new void Start()
    {
        Animate = 1;
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
        StateText.SetText(Mathf.Round(Ratio() * 100) + "%");
    }

    void Interaction()
    {
        if (base.Active)
        {
            Brake += Input.GetAxisRaw("HorizontalArrow") * Time.deltaTime * .4f;
            Brake = Mathf.Clamp(Brake, MinBrake, MaxBrake);
        }
    }

    float Ratio()
    {
        return Brake / MaxBrake;
    }

    public float GetBrakeValue()
    {
        return Brake;
    }

    public override float GetStatus()
    {
        return Ratio();
    }
}
