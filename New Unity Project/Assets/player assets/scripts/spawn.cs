using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject spearPreFab;
    public float spearSpeed; 


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
            checkSpearSpawn();
            spawnTime += timeBetween;
            
            
        }
        //spawnSpear();
    }

    void checkSpearSpawn()
    {
        int randInt = Random.Range(0 , spawnPoints.Length);

        if (spawnPoints[randInt].position.x > this.gameObject.transform.position.x) //find out if the position of the next spawn point is further along the x axis than the p
        {
            Quaternion spearRotation = Quaternion.Euler (0, 0, 90);
            GameObject spear = Instantiate(spearPreFab, spawnPoints[randInt].position, spearRotation)as GameObject;
            spear.GetComponent<Rigidbody>().AddForce(-spearSpeed,0,0, ForceMode.Impulse);
        }
        else
        {
            Quaternion spearRotation = Quaternion.Euler (90, 0, 90);
            GameObject spear = Instantiate(spearPreFab, spawnPoints[randInt].position, spearRotation)as GameObject;
            spear.GetComponent<Rigidbody>().AddForce(spearSpeed,0,0, ForceMode.Impulse);
        }

        dustinsLimit++;
    
    }
}
