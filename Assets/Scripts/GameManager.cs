using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    /*

    level 1 = bullet
    level 1 = diesel
    level 1 = steam

    part 1 = 2 short station
    part 1 = 3 long station
    part 1 = 6 long and short station
    */
    [Range(1, 3)]
    public int level = 1;
    [Range(1, 3)]
    public int part = 3;

    public Train Train;
    public Player Player;

    WorldGen Gen;

    // Start is called before the first frame update
    void Start()
    {
        Gen = GetComponent<WorldGen>();
        Gen.SetStyle(level);



        if (level == Config.BULLET)
        {
            Train.SetMode(Config.BULLET);
            Player.SetMode(Config.BULLET);
            FindObjectOfType<AudioManager2>().setLevel(1);
        }
        else if (level == Config.DIESEL)
        {
            Train.SetMode(Config.DIESEL);
            Player.SetMode(Config.DIESEL);
            FindObjectOfType<AudioManager2>().setLevel(2);
        }
        else if (level == Config.STEAM)
        {
            Train.SetMode(Config.STEAM);
            Player.SetMode(Config.STEAM);
            FindObjectOfType<AudioManager2>().setLevel(3);
            if (part == 1)
            {
                Train.Coal.SetFuelAmount(Config.CoalToUseP1);
            }
            else if (part == 2)
            {
                Train.Coal.SetFuelAmount(Config.CoalToUseP2);
            }
            else if (part == 2)
            {
                Train.Coal.SetFuelAmount(Config.CoalToUseP3);
            }
        }



        // stations
        if (part == 1)
        {
            Gen.GenStation(2, 1, Train);
        }
        else if (part == 2)
        {
            Gen.GenStation(3, 2, Train);

        }
        else if (part == 3)
        {
            Gen.GenStation(6, 3, Train);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
