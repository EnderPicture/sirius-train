using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Train : MonoBehaviour
{
    public float Speed = 0;

    public float DragCoefficient = 013f;

    float TThrottle = 0;
    float LastTThrottle = 0;
    float MaxTThrottle = .5f;
    float TBreak = .5f;
    float LastTBreak = 0;
    float MaxTBreak = .5f;
    float LastSpeed;
    float DeltaSpeed;

    GameObject BreakText;
    GameObject ThrottleText;

    AudioManager2 AudioMan;

    // Start is called before the first frame update
    void Start()
    {
        LastTThrottle = TThrottle;
        LastTBreak = TBreak;
        
        BreakText = GameObject.Find("BadBreaking");
        ThrottleText = GameObject.Find("BadThrottle");
        LastSpeed = Speed;
        AudioMan = GameObject.Find("AudioManager2").GetComponent<AudioManager2>();
        AudioMan.Play("ambient", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (DeltaSpeed > 0.01f)
        {
            ThrottleText.SetActive(true);
        }
        else
        {
            ThrottleText.SetActive(false);
        }
        if (DeltaSpeed < -0.01f)
        {
            BreakText.SetActive(true);
        }
        else
        {
            BreakText.SetActive(false);
        }
        Speed += TThrottle * Time.deltaTime;
        if (Speed > 0)
        {
            Speed -= TBreak * Time.deltaTime;
        }
        if (Speed < 0)
        {
            Speed = 0;
        }

        Speed -= ((DragCoefficient / 1000) * Speed * Speed / 2) * Time.deltaTime;

        Vector3 position = transform.position;
        position.x += Speed * Time.deltaTime;
        transform.position = position;
    }

    private void FixedUpdate()
    {
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

    public float GetThrottleRatio()
    {
        return TThrottle / MaxTThrottle;
    }


    public void SetBreak(float tBreak)
    {
        TBreak = tBreak;
        if (TBreak > MaxTBreak)
        {
            TBreak = MaxTBreak;
            if (LastTBreak < MaxTBreak)
            {
                AudioMan.Play("pressure", 5);
            }
        }
        if (TBreak < 0)
        {
            TBreak = 0;
        }
        LastTBreak = TBreak;
    }
    public float GetBreak()
    {
        return TBreak;
    }
    public float GetBreakRatio()
    {
        return TBreak / MaxTBreak;
    }
    public float GetMaxBreak()
    {
        return MaxTBreak;
    }
}
