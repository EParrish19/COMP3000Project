using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathTracker : MonoBehaviour
{

    [SerializeField]
    private GameObject victoryScreen;

    [SerializeField]
    private GameObject logMaker;

    private int enemyNumber;
    // Start is called before the first frame update
    void Start()
    {
        enemyNumber = GameObject.FindGameObjectsWithTag("EnemyUnit").Length;
        victoryScreen.SetActive(false);
    }

    //check if all enemy units are dead
    void reduceEnemyNumber()
    {
        enemyNumber--;

        if(enemyNumber < 1)
        {
            victoryScreen.SetActive(true);
            logMaker.SendMessage("saveFile");
        }
    }
}
