using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject spearPreFab; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float timeBetween = 100f; 
        float spawnTime = 2f; 
        if (Time.time >= spawnTime)
        {
            spawnSpear();
            spawnTime += Time.time + timeBetween;
        }
        spawnSpear();
    }

    void spawnSpear()
    {
        int randInt = Random.Range(0 , spawnPoints.Length);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(spearPreFab, spawnPoints[i].position, Quaternion.identity);
        }
    }
}
