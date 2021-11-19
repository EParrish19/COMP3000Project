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

    [SerializeField]
    private GameObject target;

    //on start, stores the current gameobject as a variable
    private void Start()
    {
        thisEntity = gameObject;
    }

    //setter method to set the current target for the entity
    public void setTarget(GameObject newTarget)
    {
        target = newTarget;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {

            //RaycastHit used to get the target name when 
            RaycastHit targetInSight;



            //ray used as an indicator of if the target can be seen
            Ray sight = new Ray(thisEntity.transform.position, target.transform.position);
            Debug.DrawRay(thisEntity.transform.position, target.transform.position);

            //does a raycast to stored target
            if (Physics.Raycast(sight, out targetInSight, 100000))
            {

                //if the raycast hits the right target, print to console the event
                if (targetInSight.transform.parent.name != null && targetInSight.transform.parent.name == target.name)
                {
                    Debug.Log(targetInSight.transform.parent.name + " sighted by: " + thisEntity.name);
                    targetSighted = true;
                }
                else if (targetInSight.transform.parent.name != null)
                {
                    Debug.Log("Target Obstructed");
                }
                else
                {
                    Debug.Log("Target not Found");
                }
            }

            //seperate ray for the actual projectile
            Ray projectile = new Ray(thisEntity.transform.position, target.transform.position);

            //only needs to run if the target is in sight
            if (targetSighted == true)
            {
                RaycastHit targetHit;

                //vector3 used to add randomness to each shot
                Vector3 randomAngle = Random.rotation.eulerAngles;

                projectile.direction = sight.direction;
                projectile.direction.Scale(randomAngle);

                if (Physics.Raycast(projectile, out targetHit))
                {
                    if(targetHit.transform.parent.name != null && targetHit.transform.parent.name == target.name)
                    {
                        Debug.Log("Target hit");
                    }
                    else
                    {
                        Debug.Log("Target Missed");
                    }

                    
                }


            }
        }
    }


}
