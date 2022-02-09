using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class clickMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject selectedCharacter = GameObject.Find("Player");
        AIDestinationSetter characterPathfinding = selectedCharacter.GetComponent<AIDestinationSetter>();
        Seeker seeker = selectedCharacter.GetComponent<Seeker>();
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
                
            }
        }
    }
}
