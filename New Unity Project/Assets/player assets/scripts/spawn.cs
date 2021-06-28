using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject spearPreFab;


    int dustinsLimit = 0;
    float spawnTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float timeBetween = 5f; 
        

        Debug.Log(spawnTime);
        if (Time.time >= spawnTime && dustinsLimit < 15)
        {
            spawnSpear();
            spawnTime += timeBetween;
            
            
        }
        //spawnSpear();
    }

    void spawnSpear()
    {
        int randInt = Random.Range(0 , spawnPoints.Length);

        Quaternion spearRotation = Quaternion.Euler (0, 0, -90);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(spearPreFab, spawnPoints[i].position, spearRotation);

            dustinsLimit++;
        }
    }
}
