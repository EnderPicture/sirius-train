using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainTemperature : MonoBehaviour
{

    // Heat
    float TargetHeat;
    float Heat;
    float MaxHeat = 10;
    public GameObject HeatBar;
    public TextMeshPro TempText;

    // Start is called before the first frame update
    void Start()
    {
        TargetHeat = MaxHeat/2;
        Heat = TargetHeat;
    }

    // Update is called once per frame
    void Update()
    {
        // Heat stuff
        TargetHeat -= 0.15f * Time.deltaTime;
        TargetHeat = Mathf.Clamp(TargetHeat, 0, MaxHeat);

        Heat = Mathf.Lerp(Heat, TargetHeat, Time.deltaTime);

        Vector3 barScale = HeatBar.transform.localScale;
        barScale.y = Heat / MaxHeat;
        HeatBar.transform.localScale = barScale;

        TempText.SetText("Temperature\n" + Mathf.Round(20 + (Heat * 19.5f * 1.2f)) + "°C");
    }

    public float GetHeatValue()
    {
        return Heat;
    }

    public void AddHeat(float AdditionalHeat)
    {
        TargetHeat += AdditionalHeat;
    }
}
