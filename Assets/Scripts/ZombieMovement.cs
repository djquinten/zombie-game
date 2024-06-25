using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 3.0f;
    public float attackRange = 1.5f;
    public int damage = 1;
    public float attackCooldown = 1.0f;

    private Animator animator;
    private float lastAttackTime;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance <= attackRange)
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 lookDirection = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 move = direction * speed * Time.deltaTime;

        transform.LookAt(lookDirection);
        transform.position += new Vector3(move.x, 0, move.z);
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Invoke("DealDamage", 0.5f);
    }

    void DealDamage()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            target.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
