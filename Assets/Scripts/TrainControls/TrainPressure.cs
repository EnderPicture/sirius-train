using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainPressure : MonoBehaviour
{

    float Pressure;
    float MaxPressure = 10;
    float BoilPoint = 2.5f;
    float PressureUsefulMax = 7.5f;
    float PressureUsefulMin = 2.5f;
    public GameObject PressureNeedle;
    public TextMeshPro PressureText;

    public TrainTemperature Temperature;
    public TrainThrottle Throttle;


    float ThrottleMultiplier;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Temperature.GetHeatValue() > BoilPoint)
        {
            // add the additional heat
            Pressure += (Temperature.GetHeatValue() - BoilPoint) * 0.2f * Time.deltaTime;
        }
        else
        {
            // pressure cooling
            Pressure -= .03f * Time.deltaTime;
        }
        Pressure -= Throttle.GetThrottleValue() * Time.deltaTime * 2;
        Pressure = Mathf.Clamp(Pressure, 0, MaxPressure);
        Vector3 needleAngle = new Vector3(0, 0, Mathf.Lerp(90, -90, Pressure / MaxPressure));
        PressureNeedle.transform.localEulerAngles = needleAngle;
        PressureText.SetText("Pressure\n" + Mathf.Round(Pressure * 131.0f) + " kpa");



        // throttle control
        ThrottleMultiplier = 0;
        if (Pressure > PressureUsefulMin && Pressure < PressureUsefulMin)
        {
            ThrottleMultiplier = 1;
        }
        else if (Pressure <= PressureUsefulMin && Pressure > 0)
        {
            ThrottleMultiplier = .2f;
        }
        else if (Pressure >= PressureUsefulMin)
        {
            ThrottleMultiplier = 1.6f;
        }
    }
    public void ReleasePressure(float delta)
    {
        Pressure -= delta;
        Pressure = Mathf.Clamp(Pressure, 0, MaxPressure);
    }
    public float GetThrottleMultiplier() {
        return ThrottleMultiplier;
    }
}
