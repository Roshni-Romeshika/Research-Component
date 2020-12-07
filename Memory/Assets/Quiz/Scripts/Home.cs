//Script created by IT17100076
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class Home : MonoBehaviour
{

    // Declare variables

    [SerializeField]
    private float time = 5f; //initialized value for time variable 

    [SerializeField]
    private string sceneName;

    private float timeTaken;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Creating a timer for homepage of the memory game
        timeTaken += Time.deltaTime; //Time.deltaTime is is the fulfillment time in seconds since the last frame.

        //Change the scene of the game when the actual display time is passed the initialized value for time variable 
        if (timeTaken > time) {
            //Change the scene
            SceneManager.LoadScene(sceneName);
        }
    }
}
// End of the Home script
