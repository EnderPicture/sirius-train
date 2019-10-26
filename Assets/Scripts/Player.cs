using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedMultiplier = 10;


    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");
        Vector3 velocity = rb.velocity;
        velocity.x = input*speedMultiplier;
        rb.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other) {
        GameObject collided = other.gameObject;
        if (collided.tag == "Module") {
            collided.GetComponent<Module>().SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        GameObject collided = other.gameObject;
        if (collided.tag == "Module") {
            collided.GetComponent<Module>().SetActive(false);
        }
    }
}
