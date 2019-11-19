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

    int Mode = TRAIN_MODE_BULLET;


    public TrainThrottle Throttle;
    public TrainBrake Brake;
    public TrainTemperature Temperature;
    public TrainPressure Pressure;
    public CoalSpawn Coal;
    public TrainGearbox Gearbox;


    public float FuelAmount = 100;


    public float Speed = 0;
    public float DragCoefficient = 013f;


    AudioManager2 AudioMan;

    float DistFromStationInit = 0;
    float DistFromStation = 0;

    int CoalUsed = 0;

    public ClipBoard Menu;

    public GameObject TrainIcon;

    float NextStationTime;

    public TextMeshPro SpeedText;
    public TextMeshPro StationText;
    public TextMeshPro TimeText;

    public bool Playing = false;

    int NextStationID = -1;
    List<EndTrainStation> Stations;
    EndTrainStation nextStation = null;

    // score system values
    float MaxAcceleration;
    float TimeStarted;
    List<float> ParkingJobScore = new List<float>();

    bool GameDone = false;

    float SpeedLimit;

    public TextMeshPro SpeedLimitText;

    // Start is called before the first frame update
    void Start()
    {
        AudioMan = GameObject.Find("AudioManager2").GetComponent<AudioManager2>();
        AudioMan.Play("ambient", 0);

        switch (Mode)
        {
            case 1:
                Temperature.gameObject.SetActive(false);
                Pressure.gameObject.SetActive(false);
                Coal.gameObject.SetActive(false);
                SpeedLimit = Config.SpeedLimitBullet;
                break;
            case 2:
                Temperature.gameObject.SetActive(false);
                Pressure.gameObject.SetActive(false);
                Coal.gameObject.SetActive(false);
                SpeedLimit = Config.SpeedLimitDiesel;
                break;
            case 3:
                SpeedLimit = Config.SpeedLimitSteam;
                break;
        }
        SpeedLimitText.SetText(SpeedLimit + "");

    }

    public void SetupStations(List<EndTrainStation> stations)
    {
        Stations = stations;
        NextStation();
    }

    public void SetMode(int mode)
    {
        Mode = mode;
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
            // HINT THAT THEY SHOULD USE THE PARKING BREAK
        }
        if (nextStation.GetTrainInStart() && nextStation.GetTrainInEnd() && Speed == 0 && !GameDone && Gearbox.GetGear() == Gearbox.P)
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

            Throttle.enabled = true;
            Brake.enabled = true;
            Temperature.enabled = true;
            Pressure.enabled = true;
            Coal.enabled = true;
            Gearbox.enabled = true;
            // speed calc
            float acc = 0;


            if (Mode == Config.STEAM)
            {
                acc += (Throttle.GetThrottleValue() * Pressure.GetThrottleMultiplier()) * Time.deltaTime;
            }
            else if (Mode == Config.DIESEL)
            {
                FuelAmount -= Throttle.GetThrottleValue();
                if (FuelAmount < 0)
                {
                    FuelAmount = 0;
                }

                if (FuelAmount > 0)
                {
                    acc += Throttle.GetThrottleValue() * Time.deltaTime;
                }
            }
            else
            {
                acc += Throttle.GetThrottleValue() * Time.deltaTime;
            }



            if (Speed > 0)
            {
                acc -= Brake.GetBrakeValue() * Time.deltaTime;
            }
            if (Speed < 0)
            {
                Speed = 0;
            }

            acc -= ((DragCoefficient / 100) * Speed * Speed / 2) * Time.deltaTime;
            if (Gearbox.GetGear() != Gearbox.P)
            {
                Speed += acc;
            }

            float absAcc = Mathf.Abs(acc);
            if (absAcc > MaxAcceleration)
            {
                MaxAcceleration = absAcc;
            }

            Vector3 position = transform.position;

            if (Gearbox.GetGear() == Gearbox.R)
            {
                position.x -= Speed * Time.deltaTime;
            }
            else if (Gearbox.GetGear() == Gearbox.D)
            {
                position.x += Speed * Time.deltaTime;
            }
            transform.position = position;

            SpeedText.SetText("Speed\n" + Mathf.Round(Speed * 10) + " KM/H");

            TimeText.SetText((NextStationTime - Time.realtimeSinceStartup) + "");
        }
        else
        {
            TimeText.SetText("Ready To Go");
            Throttle.enabled = false;
            Brake.enabled = false;
            Temperature.enabled = false;
            Pressure.enabled = false;
            Coal.enabled = false;
            Gearbox.enabled = false;
        }

    }

    // get and set stuff


    public void AddHeat(float AdditionalHeat)
    {
        Temperature.AddHeat(AdditionalHeat);
        CoalUsed++;
    }

    public void StartGame()
    {
        Playing = true;
        TimeStarted = Time.realtimeSinceStartup;
        calcTime(true);
    }
    public void ResumeGame()
    {
        Playing = true;
        calcTime(false);
    }

    void calcTime(bool startingTrain)
    {
        if (startingTrain)
        {
            NextStationTime = Time.realtimeSinceStartup + (DistFromStationInit * Config.TrainTimingMultiplierStarting);
        }
        else
        {
            NextStationTime = Time.realtimeSinceStartup + (DistFromStationInit * Config.TrainTimingMultiplier);
        }
    }

    public float GetSpeed()
    {
        return Speed;
    }
}
