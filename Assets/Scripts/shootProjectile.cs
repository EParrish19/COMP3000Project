using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEditor;
using UnityEngine.UI;

public class shootProjectile : MonoBehaviour
{
    //Is a target in sight of the entity
    bool targetSighted = false;

    private GameObject thisEntity;
    private GameObject target;

    private unitWeapon autoRifle = new unitWeapon("Auto Rifle", 10.0f, 0.2f, 2.5f, 1);
    private unitWeapon rifle = new unitWeapon("Semi-Auto Rifle", 33.0f, 1.0f, 0.5f, 1);
    private unitWeapon burstRifle = new unitWeapon("Burst Rifle" ,20.0f, 0.5f, 1.0f, 3);

    private unitWeapon[] unitWeapons = new unitWeapon[3];

    private unitWeapon currentWeapon;

    [SerializeField]
    private Text weaponIndicator;

    private int currentWeaponIndex = 0;

    private float timer;

    //Class and constructor for different unit weapons
     class unitWeapon
    {
        public string weaponName;
        public float weaponDamage;
        public float fireTime;
        public float accuracyRange;
        public int burstLength;

        public unitWeapon(string newWeaponName, float newWeaponDamage, float newWeaponfireTime, float newWeaponAccuracyRange, int newWeaponBurstLength)
        {
            weaponName = newWeaponName;
            weaponDamage = newWeaponDamage;
            fireTime = newWeaponfireTime;
            accuracyRange = newWeaponAccuracyRange;
            burstLength = newWeaponBurstLength;
        }
    }

    //on start, stores the current gameobject as a variable
    private void Start()
    {
        thisEntity = gameObject;

        unitWeapons[0] = autoRifle;
        unitWeapons[1] = rifle;
        unitWeapons[2] = burstRifle;

        currentWeapon = unitWeapons[0];

        timer = currentWeapon.fireTime;

        weaponIndicator = GameObject.Find("UIElements").GetComponentInChildren<Text>();

        weaponIndicator.text = "Current Weapon: " + currentWeapon.weaponName;

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
            weaponIndicator.text = "Current Weapon: " + currentWeapon.weaponName;
            Debug.Log(("Swapping weapon to {0}.", currentWeapon.weaponName));
        }
        else
        {
            currentWeaponIndex += 1;
            currentWeapon = unitWeapons[currentWeaponIndex];
            weaponIndicator.text = "Current Weapon: " + currentWeapon.weaponName;
            Debug.Log(("Swapping weapon to {0}.", currentWeapon.weaponName));
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

        Vector3 randomAngle = new Vector3(Random.Range(-currentWeapon.accuracyRange, currentWeapon.accuracyRange), Random.Range(-currentWeapon.accuracyRange, currentWeapon.accuracyRange), Random.Range(0.0f, currentWeapon.accuracyRange));
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
                    targetHit.transform.gameObject.SendMessage("takeDamage", currentWeapon.weaponDamage);
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
                for (int i = 0; i <= currentWeapon.burstLength; i++)
                {
                    Shoot(target);
                }


                timer = currentWeapon.fireTime;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

           
    }
}



