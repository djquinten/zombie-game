using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private Transform target;
    public float speed = 3.0f;

    void Start()
    {
        // Find the player in the scene by tag
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.LookAt(target);
        transform.position += direction * speed * Time.deltaTime;
    }
}
