using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    float Speed = 0;
    float MaxSpeed = 100;

    float TThrottle = 0;
    float MaxTThrottle = 100;
    float TBreak = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x += Speed*Time.deltaTime;
        transform.position = position;
    }

    public void SetThrottle(float throttle) {
        TThrottle = throttle;
        if (TThrottle > MaxTThrottle) {
            TThrottle = MaxTThrottle;
        }
        if (TThrottle < 0) {
            TThrottle = 0;
        }
    }
    public float GetThrottle() {
        return TThrottle;
    }
}
