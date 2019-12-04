using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static readonly float[] BULLET_TIMEING_1 = {
        1,2,3
    };


    public static readonly int CoalToUseP1 = 30;
    public static readonly int CoalToUseP2 = 50;
    public static readonly int CoalToUseP3 = 80;

    public static readonly float FuelToUseP1 = 50;
    public static readonly float FuelToUseP2 = 100;
    public static readonly float FuelToUseP3 = 300;


    public static readonly int SpeedLimitBullet = 200;
    public static readonly int SpeedLimitDiesel = 100;
    public static readonly int SpeedLimitSteam = 60;


    public static readonly float ShortMinDisStation = 200;
    public static readonly float ShortMaxDisStation = 400;
    public static readonly float LongMinDisStation = 600;
    public static readonly float LongMaxDisStation = 800;


    // responsible for timing and when should the train arrive at the next station
    // smaller the number shorter the time the plyer will be given to complete.
    public static readonly float TrainTimingMultiplierStarting = 2; // the first one, no station to station
    public static readonly float TrainTimingMultiplier = .75f; // long hops from station to station


    // DO NOT CHANGE VALUES UNDER HERE ---------------------------------------------
    public static readonly int BULLET = 1;
    public static readonly int DIESEL = 2;
    public static readonly int STEAM = 3;
}
