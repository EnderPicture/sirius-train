using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public Train train;
    public Transform trainStart;
    public Transform trainEnd;

    public GameObject mainCamera;

    public GameObject animal;

    float start;
    float end;

    public int amountToSpawn;
    List<float> spawnLocations = new List<float>();
    List<GameObject> animals = new List<GameObject>();

    float trainRangePadding = 100;
    float spawnInnerPadding = 0;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.InverseTransformPoint(trainStart.position).x;
        start += trainRangePadding;

        end = transform.InverseTransformPoint(trainEnd.position).x;
        end -= trainRangePadding;
    }

    // Update is called once per frame
    void Update()
    {
        for (int c = 0; c < spawnLocations.Count; c++)
        {
            float location = spawnLocations[c];
            if (location < transform.position.x)
            {
                GameObject newAnimal = GameObject.Instantiate(animal);
                newAnimal.transform.position = transform.TransformPoint(new Vector3(start, 0, 0));
                animals.Add(newAnimal);

                Animal animalScript = newAnimal.GetComponent<Animal>();
                animalScript.mainCamera = mainCamera;
                animalScript.train = train;
                animalScript.baseSpeed = train.GetSpeed();

                spawnLocations.Remove(location);
                // lost an item
                c--;
            }
        }
        for (int c = 0; c < animals.Count; c++) {
            GameObject animal = animals[c];
            float xPos = transform.InverseTransformPoint(animal.transform.position).x;
            if (xPos - spawnInnerPadding < end || xPos > start + spawnInnerPadding) {
                animals.Remove(animal);
                Object.Destroy(animal);
                c--;
            }
        }
    }

    public void ActivateSpawn(Vector3 start, Vector3 end)
    {
        for (int c = 0; c < amountToSpawn; c++)
        {
            spawnLocations.Add(Random.Range(start.x + spawnInnerPadding, end.x - spawnInnerPadding));
        }
    }
}
