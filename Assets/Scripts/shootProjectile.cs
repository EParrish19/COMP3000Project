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

    private unitWeapon autoRifle = new unitWeapon(10.0f, 0.2f, 2.5f, false);
    private unitWeapon rifle = new unitWeapon(33.0f, 1.0f, 0.5f, false);
    private unitWeapon burstRifle = new unitWeapon(20.0f, 0.5f, 1.0f, true);

    private unitWeapon[] unitWeapons = new unitWeapon[3];

    private unitWeapon currentWeapon;

    private int currentWeaponIndex = 0;

    private float damage;

    private float timer;

    private float timerStart;

    //Class and constructor for different unit weapons
     class unitWeapon
    {
        public float weaponDamage;
        public float fireTime;
        public float accuracyRange;
        public bool burstFire;
        public int burstLength;

        public unitWeapon(float newWeaponDamage, float newWeaponfireTime, float newWeaponAccuracyRange, bool isBurstFire)
        {
            weaponDamage = newWeaponDamage;
            fireTime = newWeaponfireTime;
            accuracyRange = newWeaponAccuracyRange;
            burstFire = isBurstFire;

            //if the weapon is set to be burst fire (3-shot then wait before firing) then set the burst length to reflect that
            if (burstFire)
            {
                burstLength = 3;
            }
            else
            {
                burstLength = 1;
            }
        }
    }

    //on start, stores the current gameobject as a variable
    private void Start()
    {
        thisEntity = gameObject;
        timerStart = timer;

        unitWeapons[0] = autoRifle;
        unitWeapons[1] = rifle;
        unitWeapons[2] = burstRifle;

        currentWeapon = unitWeapons[0];

    }

    public void setTargetSighted(bool targetInSight)
    {
        targetSighted = targetInSight;
    }

    public void setTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public void nextWeapon()
    {
        if (currentWeaponIndex == unitWeapons.Length - 1)
        {
            currentWeaponIndex = 0;
            currentWeapon = unitWeapons[currentWeaponIndex];
        }
        else
        {
            currentWeaponIndex += 1;
            currentWeapon = unitWeapons[currentWeaponIndex];
        }
    }

    public void previousWeapon()
    {
        if(currentWeaponIndex == 0)
        {
            currentWeaponIndex = unitWeapons.Length - 1;
            currentWeapon = unitWeapons[currentWeaponIndex];
        }
        else
        {
            currentWeaponIndex -= 1;
            currentWeapon = unitWeapons[currentWeaponIndex];
        }
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



