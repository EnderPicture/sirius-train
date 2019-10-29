using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Train : MonoBehaviour
{
    public float Speed = 0;

    public float DragCoefficient = 013f;

    float TThrottle = 0;
    float LastTThrottle = 0;
    float MaxTThrottle = .5f;
    float TBreak = .5f;
    float LastTBreak = 0;
    float MaxTBreak = .5f;
    float LastSpeed;
    float DeltaSpeed;

    // Heat
    float Heat;
    float MaxHeat = 10;
    public GameObject HeatBar;


    float Pressure;
    float MaxPressure = 10;
    float BoilPoint = 2.5f;
    float PressureUsefulMax = 7.5f;
    float PressureUsefulMin = 2.5f;
    public GameObject PressureNeedle;

    GameObject BreakText;
    GameObject ThrottleText;

    AudioManager2 AudioMan;

    bool TrainStartInStation = false;
    bool TrainEndInStation = false;

    float DistFromStation = 0;

    int CoalUsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        LastTThrottle = TThrottle;
        LastTBreak = TBreak;

        BreakText = GameObject.Find("BadBreaking");
        ThrottleText = GameObject.Find("BadThrottle");
        LastSpeed = Speed;
        AudioMan = GameObject.Find("AudioManager2").GetComponent<AudioManager2>();
        AudioMan.Play("ambient", 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Heat stuff
        Heat -= 0.03f * Time.deltaTime;
        Heat = Mathf.Clamp(Heat, 0, MaxHeat);
        Vector3 barScale = HeatBar.transform.localScale;
        barScale.y = Heat / MaxHeat;
        HeatBar.transform.localScale = barScale;
        // 


        // Pressure stuff
        if (Heat > BoilPoint)
        {
            // add the additional heat
            Pressure += (Heat - BoilPoint) * 0.2f * Time.deltaTime;
        }
        else
        {
            // pressure cooling
            Pressure -= .03f * Time.deltaTime;
        }
        Pressure -= TThrottle * Time.deltaTime;
        Pressure = Mathf.Clamp(Pressure, 0, MaxPressure);
        Vector3 needleAngle = new Vector3(0, 0, Mathf.Lerp(90, -90, Pressure / MaxPressure));
        PressureNeedle.transform.localEulerAngles = needleAngle;


        if (TrainStartInStation && TrainEndInStation && Speed == 0)
        {
            // TODO: win state
        }

        // Debug.Log(Heat);
        if (DeltaSpeed > 0.01f)
        {
            ThrottleText.SetActive(true);
        }
        else
        {
            ThrottleText.SetActive(false);
        }
        if (DeltaSpeed < -0.01f)
        {
            BreakText.SetActive(true);
        }
        else
        {
            BreakText.SetActive(false);
        }

        // throttle control
        float usefulValue = 0;
        if (Pressure > PressureUsefulMin && Pressure < PressureUsefulMin) {
            usefulValue = 1; 
        } else if (Pressure <= PressureUsefulMin && Pressure > 0) {
            usefulValue = .2f;
        } else if (Pressure >= PressureUsefulMin ) {
            usefulValue = 1.6f;
        }
        Speed += (TThrottle * usefulValue) * Time.deltaTime;
        if (Speed > 0)
        {
            Speed -= TBreak * Time.deltaTime;
        }
        if (Speed < 0)
        {
            Speed = 0;
        }

        Speed -= ((DragCoefficient / 1000) * Speed * Speed / 2) * Time.deltaTime;

        Vector3 position = transform.position;
        position.x += Speed * Time.deltaTime;
        transform.position = position;
    }

    private void FixedUpdate()
    {
        DeltaSpeed = Speed - LastSpeed;
        LastSpeed = Speed;
    }

    public void SetThrottle(float throttle)
    {
        TThrottle = throttle;
        if (TThrottle > MaxTThrottle)
        {
            TThrottle = MaxTThrottle;
        }
        if (TThrottle < 0)
        {
            TThrottle = 0;
        }
    }
    public float GetThrottle()
    {
        return TThrottle;
    }
    public float GetMaxThrottle()
    {
        return MaxTThrottle;
    }

    public float GetThrottleRatio()
    {
        return TThrottle / MaxTThrottle;
    }


    public void SetBreak(float tBreak)
    {
        TBreak = tBreak;
        if (TBreak > MaxTBreak)
        {
            TBreak = MaxTBreak;
            if (LastTBreak < MaxTBreak)
            {
                AudioMan.Play("pressure", 5);
            }
        }
        if (TBreak < 0)
        {
            TBreak = 0;
        }
        LastTBreak = TBreak;
    }
    public float GetBreak()
    {
        return TBreak;
    }
    public float GetBreakRatio()
    {
        return TBreak / MaxTBreak;
    }
    public float GetMaxBreak()
    {
        return MaxTBreak;
    }

    public void AddHeat(float AdditionalHeat)
    {
        Heat += AdditionalHeat;
        CoalUsed++;
    }

    public void SetTrainStartInStation(bool value)
    {
        TrainStartInStation = value;
    }
    public void SetTrainEndInStation(bool value)
    {
        TrainEndInStation = value;
    }
    public void SetDistFromStation(float value)
    {
        DistFromStation = value;
    }
}
