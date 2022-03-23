using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{

    private GameObject playerUnit;
    private shootProjectile unitShootProjectile;
    // Start is called before the first frame update
    void Start()
    {
        playerUnit = GameObject.FindGameObjectWithTag("FriendlyUnit");
        unitShootProjectile = playerUnit.GetComponent<shootProjectile>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            unitShootProjectile.nextWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            unitShootProjectile.previousWeapon();
        }
    }
}
