using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float SpeedMultiplier = .1f;

    List<GameObject> Modules = new List<GameObject>();
    List<GameObject> Coals = new List<GameObject>();

    Animator animator;


    Rigidbody rb;

    GameObject ObjectInHand = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = transform.Find("Graphics").GetComponent<Animator>(); ;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxisRaw("HorizontalArrow") == 0)
        {
            float input = Input.GetAxisRaw("Horizontal");
            Vector3 velocity = rb.velocity;
            velocity.x = input * SpeedMultiplier;
            rb.velocity = velocity;
            if (velocity.x > 0)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    animator.Play("Walk");
                }
                Vector3 scale = animator.gameObject.transform.localScale;
                scale.x = 1;
                animator.gameObject.transform.localScale = scale;
            }
            else if (velocity.x < 0)
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    animator.Play("Walk");
                }
                Vector3 scale = animator.gameObject.transform.localScale;
                scale.x = -1;
                animator.gameObject.transform.localScale = scale;
            }
            else
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    animator.Play("Idle");
                }
            }
        }
        else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                animator.Play("Idle");
            }
        }

        if (ObjectInHand == null)
        {
            float shortestDist = float.MaxValue;
            GameObject shortest = null;
            foreach (GameObject module in Modules)
            {
                module.GetComponent<Module>().SetActive(false);
                float moduleDist = Mathf.Abs(transform.position.x - module.transform.position.x);

                if (moduleDist < shortestDist)
                {
                    shortestDist = moduleDist;
                    shortest = module;
                }
            }
            if (shortest != null)
            {
                shortest.GetComponent<Module>().SetActive(true);
            }

            // pick up coal logic
            if (Input.GetKeyDown("space"))
            {
                // reset those values to be used again
                shortestDist = float.MaxValue;
                shortest = null;


                for (int c = 0; c < Coals.Count; c++)
                {
                    GameObject coal = Coals[c];
                    if (coal == null)
                    {
                        Coals.Remove(coal);
                        c--;
                    }

                }
                foreach (GameObject coal in Coals)
                {
                    float coalDist = Vector3.Distance(transform.position, coal.transform.position);
                    if (coalDist < shortestDist)
                    {
                        shortestDist = coalDist;
                        shortest = coal;
                    }
                }

                if (shortest != null)
                {
                    ObjectInHand = shortest;
                    shortest.GetComponent<Coal>().target = transform;
                }
            }

        }
        else
        {
            if (Input.GetKeyDown("space"))
            {
                if (ObjectInHand.tag == "Coal")
                {
                    ObjectInHand.GetComponent<Coal>().target = null;
                    ObjectInHand = null;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject;
        if (collided.tag == "Module")
        {
            Modules.Add(collided);
        }
        if (collided.tag == "Coal")
        {
            Coals.Add(collided.transform.parent.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject collided = other.gameObject;
        if (collided.tag == "Module")
        {
            collided.GetComponent<Module>().SetActive(false);
            Modules.Remove(collided);
        }
        if (collided.tag == "Coal")
        {
            Coals.Remove(collided.transform.parent.gameObject);
        }
    }
}
