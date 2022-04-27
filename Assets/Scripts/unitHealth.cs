using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class unitHealth : MonoBehaviour
{

    public float health;


    [SerializeField]
    private GameObject deathTrackerObject;
    


    void takeDamage(float damage)
    {
        health -= damage;
        if(health <= 0.0f)
        {
            die();
        }
    }

    void die()
    {
        deathTrackerObject.SendMessage("reduceEnemyNumber");
        GameObject player = GameObject.Find("Player");

        player.SendMessage("resetTarget");

        gameObject.SetActive(false);
        
        
    }
}
