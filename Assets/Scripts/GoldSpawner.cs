using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    public GameObject[] goldItemObjects;

    private float actualTime;
    // Update is called once per frame
    void Start()
    {
        InvokeRepeating("SpawnGoldPurse", 2.0f, 5.0f);
    }

    void SpawnGoldPurse(){
        int randomIndex = Random.Range(0, goldItemObjects.Length);
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-40, 40), 5, Random.Range(-40, 40));

        Instantiate(goldItemObjects[randomIndex], randomSpawnPosition, Quaternion.identity);
    }
}
