using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class clickMovement : MonoBehaviour
{
    private GameObject selectedCharacter;
    private AIDestinationSetter characterPathfinding;
    private Seeker seeker;

    [SerializeField]
    private GameObject moveTargetPrefab;


    // Start is called before the first frame update
    void Start()
    {
        selectedCharacter = GameObject.Find("Player");
        characterPathfinding = selectedCharacter.GetComponent<AIDestinationSetter>();
        seeker = selectedCharacter.GetComponent<Seeker>();
        moveTargetPrefab = Resources.Load<GameObject>("Prefabs/moveTarget");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit orderLocation;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out orderLocation))
            {
                GameObject oldMarker = GameObject.FindGameObjectWithTag("PlayerMoveMarker");

                if(oldMarker != null)
                {
                    Destroy(oldMarker);
                }


                GameObject newMoveTarget = Instantiate(moveTargetPrefab);
                newMoveTarget.tag = "PlayerMoveMarker";
                newMoveTarget.transform.position = orderLocation.point;
                characterPathfinding.target = newMoveTarget.transform;
                
            }
        }
    }
}
