using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour
{

    private List<GameObject> enemyUnits = new List<GameObject>();
    private GameObject currentEntity;
    private Vector3 currentEntityPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentEntity = gameObject;
        currentEntityPosition = currentEntity.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(Collider entity in Physics.OverlapSphere(currentEntityPosition, 50f))
        {
            if(entity.gameObject.tag == "EnemyUnits")
            {
                enemyUnits.Add(entity.gameObject);
            }
        }
    }
}
