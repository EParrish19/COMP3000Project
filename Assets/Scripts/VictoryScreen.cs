using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{   

    //loads the player back to main menu
    public void goToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        
    }

}
