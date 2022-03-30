using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTarget : MonoBehaviour
{

    private GameObject selectedCharacter;

    private sight unitSight;

    // Start is called before the first frame update
    void Start()
    {
        selectedCharacter = GameObject.Find("Player");
        unitSight = selectedCharacter.GetComponent<sight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit targetRay;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out targetRay))
            {
                if (targetRay.transform.gameObject.tag == "EnemyUnit")
                {
                    unitSight.SetTarget(targetRay.transform.gameObject);
                }
            }
        }
    }
}
