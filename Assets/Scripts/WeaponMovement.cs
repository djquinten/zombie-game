using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weapon;

    void Update()
    {
        // move the weapon with the player object
        weapon.transform.position = transform.position;
        
        // rotate the weapon with the player object
        weapon.transform.rotation = transform.rotation;
    }
}
