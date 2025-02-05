using System.Collections;
using UnityEngine;

public class RabbitEnemy : NeutralEnemy
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;  

    public override IEnumerator Attack()
    {
        _is_delay = true;
        _player.GetComponent<Characteristics>().TakeDamage(_damage);
        yield return new WaitForSeconds(_delay);
        _is_delay = false;
    }
}
