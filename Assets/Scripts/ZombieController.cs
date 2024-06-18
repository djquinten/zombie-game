using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // move the zombie towards the player
        // Calculate the direction to the player
        Vector3 direction = (player.position - transform.position).normalized;
        
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        // Move the zombie towards the player
        transform.position += direction * speed * Time.deltaTime;

        // Make the zombie face the player
        transform.LookAt(player);
        
        
        

    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        } 
    }

}
