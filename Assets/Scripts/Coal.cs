using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coal : MonoBehaviour
{
    public Transform target = null;
    public float smoothFactor = 10f;
    public Vector3 offset;

    SpriteRenderer rend;

    float targetAlpha = 1;
    bool die = false;

    public GameObject train = null;

    Rigidbody rb;
    private void Start()
    {
        rend = transform.Find("Graphics").Find("Coal").GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        rend.color = new Color(1, 1, 1, 0);
    }
    private void Update()
    {
        Color c = rend.color;
        float alpha = 0;
        if (die) {
            alpha = Mathf.Lerp(c.a, targetAlpha, .2f * Time.deltaTime);
        } else {
            alpha = Mathf.Lerp(c.a, targetAlpha, 10 * Time.deltaTime);
        }
        c.a = alpha;
        rend.color = c;

        if (die) {
            targetAlpha = 0;
            if (alpha <=.1) {
                Object.Destroy(gameObject);
            }
        }
    }
    private void LateUpdate()
    {
        if (target != null && !die)
        {
            rb.isKinematic = true;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, target.position + offset, smoothFactor * Time.deltaTime);
            transform.position = smoothedPos;
        }
        else
        {
            rb.isKinematic = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "InsideFurnace")
        {
            if (!die) {
                die = true;
                train.GetComponent<Train>().AddHeat(1);
            }
        }
    }
}
