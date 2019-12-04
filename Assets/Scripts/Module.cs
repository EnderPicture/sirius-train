using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    GameObject Panel;
    GameObject ToolTip;

    protected bool Active = false;

    public int Animate;

    // Start is called before the first frame update
    protected void Start()
    {
        Panel = transform.Find("Panel").gameObject;
        ToolTip = transform.Find("ToolTip").gameObject;
        ToolTip.SetActive(false);
    }

    public void SetActive(bool active)
    {
        Active = active;
        if (Active)
        {
            ToolTip.SetActive(true);
        }
        else
        {
            ToolTip.SetActive(false);
        }
    }

    public virtual float GetStatus()
    {
        Debug.Log("WRONG ");
        return 0;
    }
}
