using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour
{

    private List<GameObject> friendlyUnits = new List<GameObject>();

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

    // Update is called once per frame
    void UpdateTargets()
    {
        foreach (GameObject friendly in GameObject.FindGameObjectsWithTag("FriendlyUnit"))
        {
            friendlyUnits.Add(friendly);
            Debug.Log("friendly unit added: " + friendly.name);
        }

        foreach(GameObject friendly in friendlyUnits)
        {
            List<GameObject> enemyUnits = new List<GameObject>();

            sightScript = friendly.GetComponent<sight>();

            Debug.Log(friendly.name + " is searching for targets");
            foreach (Collider enemy in Physics.OverlapSphere(friendly.transform.position, 50f))
            { 

                if (enemy.gameObject.tag == "EnemyUnit")
                {
                    enemyUnits.Add(enemy.gameObject);
                    Debug.Log("enemy unit added: " + enemy.gameObject.name);
                }
            }

            foreach(GameObject enemy in enemyUnits)
            {
                RaycastHit visible;
                Vector3 direction = (enemy.transform.position - friendly.transform.position).normalized;

                if(Physics.Raycast(friendly.transform.position, direction, out visible))
                {
                    if(visible.transform.gameObject != enemy)
                    {
                        enemyUnits.Remove(enemy);
                        Debug.Log("enemy unit removed: " + enemy.name);
                    }
                }
            }

            GameObject closestEnemy = null;
            float closestEnemyDistance = 0.0f;

            foreach(GameObject enemy in enemyUnits)
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

    }
}