using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PolearmScript : MonoBehaviour
{

 //   float lifeSpan = 2.0f;
 
  // Start is called before the first frame update
  void Start()
  {
    //StartCoroutine(ExecuteAfterTime(10));
  }

  // Update is called once per frame
  void Update()
  {
    /*  lifeSpan -= Time.deltaTime;

      if (lifeSpan < 0)
          Destroy(gameObject);
    */
  }

  void OnCollisionEnter()
  {
    Destroy(gameObject);      
  }
  
}
