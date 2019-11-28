using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Renderer Renderer;
    // Start is called before the first frame update
    void Start()
    {
        Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Renderer.isVisible)
        {
            Debug.Log("visible");
        }
        else
        {
            Debug.Log("visible");
        }
    }
}
