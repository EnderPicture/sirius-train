﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    bool Spawning = true;

    public GameObject coal;

    int itemCount = 0;

    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (itemCount == 0)
        {
            GameObject newCoal = Instantiate(coal, transform.position, Quaternion.identity);
            newCoal.transform.parent = transform;
        }
        Spawning = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coal")
        {
            itemCount++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Coal")
        {
            itemCount--;
        }
    }
}
