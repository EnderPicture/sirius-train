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
    public float MaxTThrottle = .5f;
    float TBrake = 0;
    float LastTBrake = 0;
    public float MaxTBrake = .5f;
    float LastSpeed;
    float DeltaSpeed;

    // Heat
    float Heat;
    float HeatSmooth;
    float MaxHeat = 10;
    public GameObject HeatBar;


    float Pressure;
    float MaxPressure = 10;
    float BoilPoint = 2.5f;
    float PressureUsefulMax = 7.5f;
    float PressureUsefulMin = 2.5f;
    public GameObject PressureNeedle;

    GameObject BrakeText;
    GameObject ThrottleText;

    AudioManager2 AudioMan;

    bool TrainStartInStation = false;
    bool TrainEndInStation = false;

    float DistFromStationInit = 0;
    float DistFromStation = 0;

    int CoalUsed = 0;

    public ClipBoard Menu;

    public GameObject TrainIcon;

    public TextMeshPro PressureText;
    public TextMeshPro TempText;

    public TextMeshPro SpeedText;

    public TextMeshPro StationText;

    public bool Playing = false;

    int NextStationID = -1;
    public List<EndTrainStation> Stations;
    EndTrainStation nextStation = null;


    float MaxAcceleration;
    float TimeStarted;

    // Start is called before the first frame update
    void Start()
    {
        LastTThrottle = TThrottle;
        LastTBrake = TBrake;

        BrakeText = GameObject.Find("BadBrakeing");
        ThrottleText = GameObject.Find("BadThrottle");
        LastSpeed = Speed;
        AudioMan = GameObject.Find("AudioManager2").GetComponent<AudioManager2>();
        AudioMan.Play("ambient", 0);

        Heat = MaxHeat / 2;

        HeatSmooth = Heat;

        NextStation();
    }

    void NextStation()
    {
        NextStationID++;

        if (Stations[NextStationID] != null)
        {
            nextStation = Stations[NextStationID];
            float dist = nextStation.transform.position.x - transform.position.x;
            DistFromStationInit = dist;
            DistFromStation = dist;
            StationText.SetText("Station " + (NextStationID + 1) + "/" + Stations.Count);
        }
    }

    void StationCheck()
    {
        float dist = nextStation.transform.position.x - transform.position.x;
        DistFromStation = dist;

        Vector3 iconPos = TrainIcon.transform.localPosition;
        iconPos.x = Lerp(0.9836f, -1.1119f, DistFromStation / DistFromStationInit);
        iconPos.x = Mathf.Clamp(iconPos.x, -1.715f, 1.715f);
        TrainIcon.transform.localPosition = iconPos;

    }


    void WinCheck()
    {
        if (nextStation.GetTrainInStart() && nextStation.GetTrainInEnd() && Speed == 0)
        {
            if (NextStationID + 1 == Stations.Count)
            {
                Menu.ShowWin();
            }
            else
            {
                Menu.ShowMidWin();
                NextStation();
            }
            Playing = false;
        }
        if ((!nextStation.GetTrainInStart() || !nextStation.GetTrainInEnd()) && DistFromStation < 0)
        {
            Menu.ShowDieScreen();
            Playing = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StationCheck();
        WinCheck();

        if (Playing)
        {
            // Heat stuff
            Heat -= 0.15f * Time.deltaTime;
            Heat = Mathf.Clamp(Heat, 0, MaxHeat);
            Vector3 barScale = HeatBar.transform.localScale;
            barScale.y = Heat / MaxHeat;
            HeatBar.transform.localScale = barScale;

            HeatSmooth = Mathf.Lerp(HeatSmooth, Heat, Time.deltaTime);
            TempText.SetText("Temperature\n" + Mathf.Round(20 + (HeatSmooth * 19.5f * 1.2f)) + "°C");
            // 

            // Pressure stuff
            if (HeatSmooth > BoilPoint)
            {
                // add the additional heat
                Pressure += (HeatSmooth - BoilPoint) * 0.2f * Time.deltaTime;
            }
            else
            {
                // pressure cooling
                Pressure -= .03f * Time.deltaTime;
            }
            Pressure -= TThrottle * Time.deltaTime * 2;
            Pressure = Mathf.Clamp(Pressure, 0, MaxPressure);
            Vector3 needleAngle = new Vector3(0, 0, Mathf.Lerp(90, -90, Pressure / MaxPressure));
            PressureNeedle.transform.localEulerAngles = needleAngle;
            PressureText.SetText("Pressure\n" + Mathf.Round(Pressure * 131.0f) + " kpa");

            // if (DeltaSpeed > 0.01f)
            // {
            //     ThrottleText.SetActive(true);
            // }
            // else
            // {
            //     ThrottleText.SetActive(false);
            // }
            // if (DeltaSpeed < -0.01f)
            // {
            //     BrakeText.SetActive(true);
            // }
            // else
            // {
            //     BrakeText.SetActive(false);
            // }

            // throttle control
            float usefulValue = 0;
            if (Pressure > PressureUsefulMin && Pressure < PressureUsefulMin)
            {
                usefulValue = 1;
            }
            else if (Pressure <= PressureUsefulMin && Pressure > 0)
            {
                usefulValue = .2f;
            }
            else if (Pressure >= PressureUsefulMin)
            {
                usefulValue = 1.6f;
            }


            // speed calc
            Speed += (TThrottle * usefulValue) * Time.deltaTime;
            if (Speed > 0)
            {
                Speed -= TBrake * Time.deltaTime;
            }
            if (Speed < 0)
            {
                Speed = 0;
            }

            Speed -= ((DragCoefficient / 100) * Speed * Speed / 2) * Time.deltaTime;

            Vector3 position = transform.position;
            position.x += Speed * Time.deltaTime;
            transform.position = position;

            SpeedText.SetText("Speed\n" + Mathf.Round(Speed * 10) + " KM/H");
        }
    }

    private float Lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
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


    public void SetBrake(float tBrake)
    {
        TBrake = tBrake;
        if (TBrake > MaxTBrake)
        {
            TBrake = MaxTBrake;
            if (LastTBrake < MaxTBrake)
            {
                AudioMan.Play("pressure", 5);
            }
        }
        if (TBrake < 0)
        {
            TBrake = 0;
        }
        LastTBrake = TBrake;
    }
    public float GetBrake()
    {
        return TBrake;
    }
    public float GetBrakeRatio()
    {
        return TBrake / MaxTBrake;
    }
    public float GetMaxBrake()
    {
        return MaxTBrake;
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
    public void SetDistFromStationInit(float value)
    {
        DistFromStationInit = value;
    }
    public void ReleasePressure(float delta)
    {
        Pressure -= delta;
        Pressure = Mathf.Clamp(Pressure, 0, MaxPressure);
        AudioMan.Play("pressure", 2);
    }
    public void StartGame()
    {
        Playing = true;
        TimeStarted = Time.realtimeSinceStartup;
    }
    public void ResumeGame()
    {
        Playing = true;
    }
}
