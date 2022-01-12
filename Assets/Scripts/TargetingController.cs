using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour
{

    private List<GameObject> enemyUnits = new List<GameObject>();
    private List<GameObject> friendlyUnits;
    private List<Vector3> friendlyPositions;
    private int[] enemyDistances;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(GameObject friendly in GameObject.FindGameObjectsWithTag("FriendlyUnits"))
        {
            friendlyUnits.Add(friendly);
        }

        foreach(GameObject unit in friendlyUnits)
        {
            friendlyPositions.Add(unit.transform.position);
        }

        foreach (Vector3 friendlyPosition in friendlyPositions)
        {
            foreach (Collider entity in Physics.OverlapSphere(friendlyPosition, 50f))
            {
                if (entity.gameObject.tag == "EnemyUnits")
                {
                    enemyUnits.Add(entity.gameObject);
                }
            }
        }
    }
}
