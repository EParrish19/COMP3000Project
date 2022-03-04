using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void exitProgram()
    {
        Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene("Standard", LoadSceneMode.Single);
    }
}
