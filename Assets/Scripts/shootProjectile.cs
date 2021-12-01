using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEditor;

public class shootProjectile : MonoBehaviour
{
    //Is a target in sight of the entity
    bool targetSighted = false;

    private GameObject thisEntity;

    //on start, stores the current gameobject as a variable
    private void Start()
    {
        thisEntity = gameObject;
    }

    public void setTargetSighted(bool targetInSight)
    {
        targetSighted = targetInSight;
    }


    public void Shoot(GameObject target)
    {
        //seperate ray for the actual projectile
        Vector3 thisEntityPosition = thisEntity.transform.position;
        Vector3 targetPosition = target.transform.position;
        Vector3 direction = (targetPosition - thisEntityPosition).normalized;

        Vector3 randomAngle = Random.insideUnitSphere;

        direction.Scale(randomAngle);
        direction.Scale(new Vector3(Random.Range(0f, 2f), Random.Range(0f, 2f), Random.Range(0f, 2f)));

        Ray projectile = new Ray(thisEntityPosition, direction);

        //only needs to run if the target is in sight
        if (targetSighted == true)
        {
            RaycastHit targetHit;
            

            if (Physics.Raycast(projectile,out targetHit))
            {
                if (targetHit.transform.gameObject.name != null && targetHit.transform.gameObject.name == target.name)
                {
                    Debug.Log("Target hit");
                    Debug.DrawRay(thisEntityPosition, projectile.direction * 10, Color.yellow, 10f);
                }
                else
                {
                    Debug.Log("Target Missed");
                    Debug.DrawRay(thisEntityPosition, projectile.direction * 10, Color.blue, 10f);
                }


            }


        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

           
    }
}



