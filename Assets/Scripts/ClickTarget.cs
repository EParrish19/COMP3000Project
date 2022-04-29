using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTarget : MonoBehaviour
{

    private GameObject selectedCharacter;

    private sight unitSight;

    private GameObject markerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        selectedCharacter = GameObject.Find("Player");
        unitSight = selectedCharacter.GetComponent<sight>();
        markerPrefab = Resources.Load<GameObject>("Prefabs/Marker");
    }

    // target enemy can be changed with left click
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
                    GameObject targetMarker = Instantiate(markerPrefab, targetRay.transform);
                    targetMarker.transform.position = new Vector3(targetMarker.transform.position.x, targetMarker.transform.position.y + 2, targetMarker.transform.position.z);
                    
                }
            }
        }
    }
}
