using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSetter : MonoBehaviour
{

    public Text textObject;
    float scoreLifespan;
    public int spearsHit;
    float incrementValue = 1.0f;
    float Score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (incrementValue < scoreLifespan)
        {
            incrementValue += 1;
                AddScoreText(1.0f);
        }

        scoreLifespan += Time.deltaTime;
        textObject.text = Score.ToString(); 
    }

    void AddScoreText(float scoreAdd)
    {
        Score += scoreAdd;
    }
}
