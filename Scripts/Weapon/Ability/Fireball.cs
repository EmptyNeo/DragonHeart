using System.Collections;
using UnityEngine;

public class Fireball : MagicAbility
{
    private void Start()
    {
        Destroy(gameObject, Time);
    }
    public override void Ability()
    {
        
    }
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
