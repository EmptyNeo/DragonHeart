using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float time;
    [HideInInspector] public float Damage;
    private void Start() => Destroy(gameObject, time);
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out DamageHandlerEnemy enemy))
        {
            enemy.TakeDamage(Damage);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
