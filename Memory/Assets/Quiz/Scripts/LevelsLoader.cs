//Script created by IT17100076
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelsLoader : MonoBehaviour
{
    
    //Implementation of function called LevelsLoad(int level) which take input parameter as number of the level
    public void LevelsLoad(int level)
    {
        //Based on the number passes in as input parameter to the function, user can be able to continue with level of the game
        SceneManager.LoadScene(level);
    }
    // End of the LevelsLoad(int level) function
}
// End of the LevelsLoader script
