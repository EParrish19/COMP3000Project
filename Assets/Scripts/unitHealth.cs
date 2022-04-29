using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class unitHealth : MonoBehaviour
{

    public float health;


    [SerializeField]
    private GameObject deathTrackerObject;
    

    //reduces enemy health by the player weapon's damage
    void takeDamage(float damage)
    {
        health -= damage;
        if(health <= 0.0f)
        {
            die();
        }
    }

    //if health is 0, remove the enemy from play
    void die()
    {
        deathTrackerObject.SendMessage("reduceEnemyNumber");
        GameObject player = GameObject.Find("Player");

        

        gameObject.SetActive(false);
        player.SendMessage("resetTarget");


    }
}
