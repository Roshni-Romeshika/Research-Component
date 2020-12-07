//Script created by IT17100076
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDuration : MonoBehaviour
{
    //Declaring varibales
    public string levelToLoad;
    public float timer = 30f;
    public Text timerSeconds;
    
    // Start is called before the first frame update
    void Start()
    {
        timerSeconds = GetComponent<Text>();
    }


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerSeconds.text = timer.ToString("f3");
        if (timer <= 0)
        {
            Application.LoadLevel(levelToLoad);
        }
        if (timer <= 5)
        {
            timerSeconds.color = Color.red;
        }
    }


    
}
// End of the TimeDuration script




