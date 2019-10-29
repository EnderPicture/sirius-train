using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public float Speed = 0;

    public float DragCoefficient = 013f;

    float TThrottle = 0;
    float MaxTThrottle = 1;
    float TBreak = 0;
    float MaxTBreak = 1;
    float LastSpeed;
    float DeltaSpeed;

    // Start is called before the first frame update
    void Start()
    {
        LastSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {   
        if (DeltaSpeed > 0.01f) {
            Debug.Log("start too fast");
        }
        if (DeltaSpeed < -0.01f) {
            Debug.Log("break too fast");
        }
        Speed += TThrottle * Time.deltaTime;
        if (Speed > 0) {
            Speed -= TBreak * Time.deltaTime;
        }
        if (Speed < 0) {
            Speed = 0;
        }

        Speed -= ((DragCoefficient/1000)*Speed*Speed/2)*Time.deltaTime;

        Vector3 position = transform.position;
        position.x += Speed * Time.deltaTime;
        transform.position = position;


    }

    private void FixedUpdate() {
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

    public float GetThrottleRatio() {
        return TThrottle/MaxTThrottle;
    }


    public void SetBreak(float tBreak)
    {
        TBreak = tBreak;
        if (TBreak > MaxTBreak)
        {
            TBreak = MaxTBreak;
        }
        if (TBreak < 0)
        {
            TBreak = 0;
        }
    }
    public float GetBreak()
    {
        return TBreak;
    }
    public float GetBreakRatio() {
        return TBreak/MaxTBreak;
    }
    public float GetMaxBreak()
    {
        return MaxTBreak;
    }
}
