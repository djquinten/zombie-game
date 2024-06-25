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
        // Find the player in the scene by tag
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        // Ensure the attack trigger is reset
        animator.ResetTrigger("Attack");
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
        transform.LookAt(target);
        Vector3 move = direction * speed * Time.deltaTime;

        // Move while keeping the Y position at 0
        transform.position += new Vector3(move.x, 0, move.z);
    }

    void Attack()
    {
        animator.SetTrigger("Attack"); // Trigger the attack animation

        // Deal damage to the player after the animation delay
        Invoke("DealDamage", 0.5f); // Adjust delay as per animation length
    }

    void DealDamage()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            target.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
