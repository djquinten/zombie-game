using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 20.0f;
    public float lifeTime = 2.0f;

    private void Start()
    {
        // when spawned, destroy after a certain amount of time
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
