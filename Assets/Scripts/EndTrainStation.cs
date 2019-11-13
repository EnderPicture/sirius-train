using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrainStation : MonoBehaviour
{

    bool TrainInEnd = false;
    bool TrainInStart = false;

    private void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject;
        if (collided.tag == "TrainStart")
        {
            TrainInStart = true;
        }
        if (collided.tag == "TrainEnd")
        {
            TrainInEnd = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject collided = other.gameObject;
        if (collided.tag == "TrainStart")
        {
            TrainInStart = false;
        }
        if (collided.tag == "TrainEnd")
        {
            TrainInEnd = false;
        }
    }
    public bool GetTrainInEnd()
    {
        return TrainInEnd;
    }
    public bool GetTrainInStart()
    {
        return TrainInStart;
    }
}
