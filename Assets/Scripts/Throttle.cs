using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throttle : Module
{
    Train train;
    // Start is called before the first frame update
    new void Start()
    {
        train = GameObject.Find("Train").GetComponent<Train>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (base.Active) {
            if (Input.GetAxis("Jump") > 0) {
                train.SetThrottle(.1f);
            }
        }
    }
}
