using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoalSpawn : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject coal;

    int FuelAmount = 100;

    int itemCount = 0;

    GameObject train;

    void Start()
    {
        train = GameObject.Find("Train");
    }

    private void FixedUpdate()
    {
        if (itemCount == 0 && FuelAmount > 0)
        {
            GameObject newCoal = Instantiate(coal, transform.position, Quaternion.identity);
            newCoal.GetComponent<Coal>().train = train;
            newCoal.transform.parent = transform;
            FuelAmount --;
        }
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
    public void SetFuelAmount(int amount) {
        FuelAmount = amount;
    }
    public int GetFuelAmount() {
        return FuelAmount;
    }
}
