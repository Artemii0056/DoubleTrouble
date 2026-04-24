using DefaultNamespace;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 1;

    private Vector3 _direction;

    public void Launch(Vector3 direction)
    {
        _direction = direction;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += _direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Health health))
        {
            //health.DecreaseValue(damage);
            Destroy(gameObject);
        }
    }
}