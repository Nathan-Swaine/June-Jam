using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject spearPreFab, spear;
    public float spearSpeed , spawnLifespan = 0.0f, spawnTime = 2f;
    public bool spawnThings = false;
    public int spearLimit = 0, timeBetween = 2;
    

    // Start is called before the first frame update
    void Start()
    {
        spawnThings = true;
        StartCoroutine(CheckSpearSpawnTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spearSpawn()
    {
        int randInt = Random.Range(0 , spawnPoints.Length);

        if (spawnPoints[randInt].position.x > this.gameObject.transform.position.x) //find out if the position of the next spawn point is further along the x axis than the player
        {
            Quaternion spearRotation = Quaternion.Euler (0, 0, 90);
            spear = Instantiate(spearPreFab, spawnPoints[randInt].position, spearRotation)as GameObject;
            spear.GetComponent<Rigidbody>().AddForce(-spearSpeed,0,0, ForceMode.Impulse);
        }
        else
        {
            Quaternion spearRotation = Quaternion.Euler (0, 0, -90);
            spear = Instantiate(spearPreFab, spawnPoints[randInt].position, spearRotation)as GameObject;
            spear.GetComponent<Rigidbody>().AddForce(spearSpeed,0,0, ForceMode.Impulse);
        }

    }

    IEnumerator CheckSpearSpawnTime()
    {
        while (spawnThings)
        {
            yield return new WaitForSeconds(timeBetween);
         
            spearSpawn();
            
        }
    }

  
}
