using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public static float Lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
    }
}