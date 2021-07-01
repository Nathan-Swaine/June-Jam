using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    public AudioSource Song;
    //public
    // Start is called before the first frame update
    void Start()
    {
        Song.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDied()
    {
        SceneManager.LoadScene("GameOver");
    }
}
