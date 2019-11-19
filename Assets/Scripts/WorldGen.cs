using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    public GameObject Station;

    float shortMinDisStation = Config.shortMinDisStation;
    float shortMaxDisStation = Config.shortMaxDisStation;

    float longMinDisStation = Config.longMinDisStation;
    float longMaxDisStation = Config.longMaxDisStation;


    int Style = 0;


    public void GenStation(int numberOfStations, int mode, Train train)
    {
        /*
        mode 1 = short
        mode 2 = long
        mode 3 = random short long
        */



        GameObject stationCon = new GameObject();
        stationCon.transform.SetParent(transform);

        List<EndTrainStation> stations = new List<EndTrainStation>();

        float loc = 0;
        for (int c = 0; c < numberOfStations; c++)
        {
            GameObject newStation = Instantiate(Station);
            newStation.transform.SetParent(stationCon.transform);

            Vector3 pos = newStation.transform.localPosition;
            pos.x = loc;

            newStation.transform.localPosition = pos;

            stations.Add(newStation.GetComponent<EndTrainStation>());

            if (mode == 1)
            {
                loc += Random.Range(shortMinDisStation, shortMaxDisStation);
            }

            switch (mode)
            {
                case 1:
                    loc += Random.Range(shortMinDisStation, shortMaxDisStation);
                    break;
                case 2:
                    loc += Random.Range(longMinDisStation, longMaxDisStation);
                    break;
                case 3:
                    loc += Random.Range(shortMinDisStation, longMaxDisStation);
                    break;
                default:
                    loc += Random.Range(shortMinDisStation, shortMaxDisStation);
                    break;
            }
        }

        train.SetupStations(stations);

    }

    public void SetStyle(int style)
    {
        Style = style; 
    }
}
