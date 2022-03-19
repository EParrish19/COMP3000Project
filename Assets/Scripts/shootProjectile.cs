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
    private GameObject target;

    public float damage;

    [SerializeField]
    private float timer;

    private float timerStart;

    public unitWeapon(float damage, float rate, Vector3 accuracyRange, bool burst)
    {
        float gunDamage = damage;
        float fireRate = rate;
        Vector3 RandomAccuracy = accuracyRange;
        bool burstFire = burst;

        int burstLength;

        if (burst)
        {
            burstLength = 3;
        }
        else
        {
            burstLength = 1;
        }
    }

    //on start, stores the current gameobject as a variable
    private void Start()
    {
        thisEntity = gameObject;
        timerStart = timer;
    }

    public void setTargetSighted(bool targetInSight)
    {
        targetSighted = targetInSight;
    }

    public void setTarget(GameObject newTarget)
    {
        target = newTarget;
    }


    public void Shoot(GameObject target)
    {
        //seperate ray for the actual projectile
        Vector3 thisEntityPosition = thisEntity.transform.position;
        Vector3 targetPosition = target.transform.position;
        Vector3 direction = (targetPosition - thisEntityPosition).normalized;

        Vector3 randomAngle = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), Random.Range(0.0f, 7.0f));
        randomAngle = randomAngle.normalized;

        direction.x += randomAngle.x;
        direction.y += randomAngle.y;
        direction.z += randomAngle.z;

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
                    targetHit.transform.gameObject.SendMessage("takeDamage", damage);
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
        if (targetSighted)
        {
            
            
            if(timer <= 0.0f)
            {
                Shoot(target);
                timer = timerStart;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

           
    }
}



