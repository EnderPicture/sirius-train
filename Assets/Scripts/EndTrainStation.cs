using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrainStation : MonoBehaviour
{
    GameObject Train;
    Train TrainScript;
    // Start is called before the first frame update
    void Start()
    {
        Train = GameObject.Find("Train");
        TrainScript = Train.GetComponent<Train>();
    }

    // Update is called once per frame
    void Update()
    {
        float DistFromTrain = transform.position.x-Train.transform.position.x;
        TrainScript.SetDistFromStation(DistFromTrain);
    }
    private void OnTriggerEnter(Collider other) {
        GameObject collided = other.gameObject;
        if (collided.tag == "TrainStart") {
            TrainScript.SetTrainStartInStation(true);
        }
        if (collided.tag == "TrainEnd") {
            TrainScript.SetTrainEndInStation(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        GameObject collided = other.gameObject;
        if (collided.tag == "TrainStart") {
            TrainScript.SetTrainStartInStation(false);
        }
        if (collided.tag == "TrainEnd") {
            TrainScript.SetTrainEndInStation(false);
        }
    }
}
