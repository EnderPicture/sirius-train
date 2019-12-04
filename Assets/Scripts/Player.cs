using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float SpeedMultiplier = .1f;
    public CameraControl Camera;

    List<GameObject> Modules = new List<GameObject>();
    List<GameObject> Coals = new List<GameObject>();

    Animator animator;

    int Mode;



    string WalkName;
    string IdleName;
    string FLName;
    string SLName;

    Rigidbody rb;

    GameObject ObjectInHand = null;

    // Start is called before the first frame update
    public void SetMode(int mode)
    {
        Mode = mode;
        if (Mode == Config.BULLET)
        {
            WalkName = "BWalk";
            IdleName = "BIdle";
            FLName = "BFlever";
            SLName = "BSlever";
        }
        else if (Mode == Config.DIESEL)
        {
            WalkName = "DWalk";
            IdleName = "DIdle";
            FLName = "DFlever";
            SLName = "DSlever";
        }
        else if (Mode == Config.STEAM)
        {
            WalkName = "SWalk";
            IdleName = "SIdle";
            FLName = "SFlever";
            SLName = "SSlever";
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = transform.Find("Graphics").GetComponent<Animator>(); ;
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxisRaw("HorizontalArrow") == 0 && Camera.getCameraMode() == CameraControl.MODE_CAMERA)
        {
            float input = Input.GetAxisRaw("Horizontal");
            Vector3 velocity = rb.velocity;
            velocity.x = input * SpeedMultiplier;
            rb.velocity = velocity;
            if (velocity.x > 0)
            {
                animator.Play(WalkName);
                animator.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (velocity.x < 0)
            {
                animator.Play(WalkName);
                animator.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                animator.Play(IdleName);
            }
        }
        else
        {
            animator.Play(IdleName);
        }

        if (ObjectInHand == null)
        {
            // if nothing in hand, check for possible actions

            // activate modules of possible
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
            if (shortest != null && Camera.getCameraMode() == CameraControl.MODE_CAMERA)
            {
                // set the closest module to active
                Module module = shortest.GetComponent<Module>();
                module.SetActive(true);
                if (Input.GetAxisRaw("HorizontalArrow") != 0)
                {
                    if (module.Animate != -1) {
                        animator.speed = 0f;
                        if (module.Animate == 1) {
                            animator.Play(FLName, 0, Mathf.Clamp(module.GetStatus(),0f,1f));
                            animator.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                        } else if (module.Animate == 2) {
                            animator.Play(SLName, 0, Mathf.Clamp(module.GetStatus(),0f,1f));
                            animator.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                        }
                    }
                }
                else
                {
                    animator.speed = 1f;
                }
            }

            // pick up coal logic
            if (Input.GetKeyDown("space") && Camera.getCameraMode() == CameraControl.MODE_CAMERA)
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
