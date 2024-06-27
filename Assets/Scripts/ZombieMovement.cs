using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 3.0f;
    public float attackRange = 1.5f;
    public int damage = 1;
    public float attackCooldown = 1.0f;

    private Animator _animator;
    private float _lastAttackTime;
    private GameManager _gameManager;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (! target || !_gameManager.isGameStarted)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attackRange)
        {
            if (Time.time >= _lastAttackTime + attackCooldown)
            {
                Attack();
                _lastAttackTime = Time.time;
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private void OnDestroy()
    {
        _gameManager.ZombieKilled();
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 lookDirection = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 move = direction * speed * Time.deltaTime;

        transform.LookAt(lookDirection);
        transform.position += new Vector3(move.x, 0, move.z);
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
        Invoke("DealDamage", 0.5f);
    }

    private void DealDamage()
    {
        if (target && Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            target.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
