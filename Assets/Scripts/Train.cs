using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class Train : MonoBehaviour
{
    public static readonly int TRAIN_MODE_BULLET = 1; 
    public static readonly int TRAIN_MODE_DIESEL = 2; 
    public static readonly int TRAIN_MODE_STEAM = 3;

    public int mode = TRAIN_MODE_BULLET;


    public TrainThrottle Throttle;
    public TrainBrake Brake;
  

    public float Speed = 0;
    public float DragCoefficient = 013f;
    
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

    // score system values
    float MaxAcceleration;
    float TimeStarted;
    List<float> ParkingJobScore = new List<float>();

    bool GameDone = false;

    // Start is called before the first frame update
    void Start()
    {
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
        iconPos.x = Helper.Lerp(0.9836f, -1.1119f, DistFromStation / DistFromStationInit);
        iconPos.x = Mathf.Clamp(iconPos.x, -1.715f, 1.715f);
        TrainIcon.transform.localPosition = iconPos;

    }


    void WinCheck()
    {
        if (nextStation.GetTrainInStart() && nextStation.GetTrainInEnd() && Speed == 0 && !GameDone)
        {
            if (NextStationID + 1 == Stations.Count)
            {
                Debug.Log("InWinState");
                GameDone = true;
                float timeUsed = Time.realtimeSinceStartup - TimeStarted;
                Menu.ShowWin(timeUsed, MaxAcceleration, ParkingJobScore);
            }
            else
            {
                Menu.ShowMidWin();
                ParkingJobScore.Add(DistFromStation);
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
            HeatSmooth = Mathf.Lerp(HeatSmooth, Heat, Time.deltaTime);

            Vector3 barScale = HeatBar.transform.localScale;
            barScale.y = HeatSmooth / MaxHeat;
            HeatBar.transform.localScale = barScale;

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
            Pressure -= Throttle.GetThrottleValue() * Time.deltaTime * 2;
            Pressure = Mathf.Clamp(Pressure, 0, MaxPressure);
            Vector3 needleAngle = new Vector3(0, 0, Mathf.Lerp(90, -90, Pressure / MaxPressure));
            PressureNeedle.transform.localEulerAngles = needleAngle;
            PressureText.SetText("Pressure\n" + Mathf.Round(Pressure * 131.0f) + " kpa");

    

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
            float acc = 0;
            
            acc += (Throttle.GetThrottleValue() * usefulValue) * Time.deltaTime;
            if (Speed > 0)
            {
                acc -= Brake.GetBrakeValue() * Time.deltaTime;
            }
            if (Speed < 0)
            {
                Speed = 0;
            }

            acc -= ((DragCoefficient / 100) * Speed * Speed / 2) * Time.deltaTime;
            Speed += acc;

            float absAcc = Mathf.Abs(acc);
            if (absAcc > MaxAcceleration) {
                MaxAcceleration = absAcc;
            }
            
            Vector3 position = transform.position;
            position.x += Speed * Time.deltaTime;
            transform.position = position;

            SpeedText.SetText("Speed\n" + Mathf.Round(Speed * 10) + " KM/H");
        }
    }














    // get and set stuff





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
