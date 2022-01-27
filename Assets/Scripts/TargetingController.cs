using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour
{

    private List<GameObject> friendlyUnits = new List<GameObject>();
    private List<GameObject> enemyUnits = new List<GameObject>();

    private sight sightScript;

    [SerializeField]
    float updateTargetTime = 2.0f;

    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (!IsInvoking("UpdateTargets"))
        {
            Invoke("UpdateTargets", updateTargetTime);
        }
    }

    //Updates targets for both friendly and enemy units
    void UpdateTargets()
    {
        //get every friendly unit in the scene
        foreach (GameObject friendly in GameObject.FindGameObjectsWithTag("FriendlyUnit"))
        {
            friendlyUnits.Add(friendly);
            Debug.Log("friendly unit added: " + friendly.name);
        }

        //get the enemies that are in range of each friendly in the scene, check for visibility and set the nearest enemy as the target
        foreach(GameObject friendly in friendlyUnits)
        {
            List<GameObject> enemyUnitsInRange = new List<GameObject>();

            sightScript = friendly.GetComponent<sight>();

            Debug.Log(friendly.name + " is searching for targets");
            foreach (Collider enemy in Physics.OverlapSphere(friendly.transform.position, 50f))
            { 

                if (enemy.gameObject.tag == "EnemyUnit")
                {
                    enemyUnitsInRange.Add(enemy.gameObject);
                    Debug.Log("enemy target added: " + enemy.gameObject.name);
                }
            }

            //if an enemy isn't directly visible by the friendly unit, the enemy unit is removed as a potential target
            foreach(GameObject enemy in enemyUnitsInRange)
            {
                RaycastHit visible;
                Vector3 direction = (enemy.transform.position - friendly.transform.position).normalized;

                if(Physics.Raycast(friendly.transform.position, direction, out visible))
                {
                    if(visible.transform.gameObject != enemy)
                    {
                        enemyUnitsInRange.Remove(enemy);
                        Debug.Log("enemy target removed: " + enemy.name);
                    }
                }
            }

            GameObject closestEnemy = null;
            float closestEnemyDistance = 0.0f;

            //determines the closest visible enemy by comparing distances
            foreach(GameObject enemy in enemyUnitsInRange)
            {
                if(closestEnemy == null)
                {
                    closestEnemy = enemy;
                    closestEnemyDistance = Vector3.Distance(friendly.transform.position, enemy.transform.position);
                }
                else
                {
                   float newDistance = Vector3.Distance(friendly.transform.position, enemy.transform.position);

                    if (closestEnemyDistance > newDistance)
                    {
                        closestEnemy = enemy;
                        closestEnemyDistance = newDistance;
                    }
                }

                Debug.Log("Closest enemy to " + friendly.name + " is " + closestEnemy.name);
            }

            sightScript.SetTarget(closestEnemy);

        }

        //gets every enemy unit in the scene
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("EnemyUnit"))
        {
            enemyUnits.Add(enemy);
            Debug.Log("enemy unit added: " + enemy.name);
        }

        //get the friendlies that are in range, check for visibility and set the closest friendly as the target
        foreach(GameObject enemy in enemyUnits)
        {
            List<GameObject> friendlyUnitsInRange = new List<GameObject>();

            sightScript = enemy.GetComponent<sight>();

            Debug.Log(enemy.name + " is searching for targets");

            foreach (Collider friendly in Physics.OverlapSphere(enemy.transform.position, 50f))
            {
                if(friendly.gameObject.tag == "FriendlyUnit")
                {
                    friendlyUnitsInRange.Add(friendly.gameObject);
                    Debug.Log("friendly target Added: " + friendly.gameObject.name);
                }
            }

            //if the friendly isn't directly visible by the enemy unit, remove the friendly as a potential target
            foreach(GameObject friendly in friendlyUnitsInRange)
            {
                RaycastHit visible;
                Vector3 direction = (friendly.transform.position - enemy.transform.position).normalized;

                if(Physics.Raycast(enemy.transform.position, direction, out visible))
                {
                    if(visible.transform.gameObject != friendly)
                    {
                        friendlyUnitsInRange.Remove(friendly);
                        Debug.Log("friendly target removed: " + friendly.name);
                    }
                }
            }
        }



    }
}