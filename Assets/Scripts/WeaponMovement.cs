using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // weapon object
    public GameObject weapon;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move the weapon with the player object
        weapon.transform.position = transform.position;
        
        // rotate the weapon with the player object
        weapon.transform.rotation = transform.rotation;
    }
}
