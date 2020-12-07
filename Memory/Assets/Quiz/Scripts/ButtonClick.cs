//Script created by IT17100076
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{

    //Declaring variable
    public string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {
        // In-built method in Unity
    }

    // Update is called once per frame
    void Update()
    {
        // In-built method in Unity
    }

    // Implementation of fucntion called NavigateToPage()
    public void NavigateToPage()
    {
        //Instead if else statement code has used switch statement
        switch (gameObject.tag)
        {
            case "English":  // If the menu selection is English this will redirect to to Starting page of the game in English 
                //Value for 'levelToLoad' was initialized in the  inspector panel of the UnityEgine
                Application.LoadLevel(levelToLoad);
                break;
            case "Sinhala":  // If the menu selection is Sinhala this will redirect to Starting page of the game in Sinhala
                //SceneManager.LoadScene("Sstarted"); // put scene name as the parameter aqnd which should navigate when press 
                //also this can be consider as the another method when moving through scene.
                Application.LoadLevel(levelToLoad);
                break;
            case "Estarted": // Get Started with English
                Application.LoadLevel(levelToLoad);
                break;
            case "Sstarted": // Get Started with Sinhala
                Application.LoadLevel(levelToLoad);
                break;
            case "L2Estarted": // Get Started with English
                Application.LoadLevel(levelToLoad);
                break;
            default: break;
        }
    } // End of the funtion NavigateToPage()
}
// End of the ButtonClick script