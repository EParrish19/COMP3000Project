using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class unitHealth : MonoBehaviour
{

    public float health;

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
        gameObject.SetActive(false);
    }
}
