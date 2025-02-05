using System.Collections;
using UnityEngine;

public class Club : ComboAttacked, ISpecialAttack
{
    [SerializeField] private GameObject _prefab_area;
    [SerializeField] private Transform _transform_crack;
    
    private void Start()
    {
        if(transform.parent != null)
        {
            _transform_crack = transform.parent.transform.GetChild(0);
        }
    }
    public IEnumerator SpecialAttack()
    {
       IsDelay = true;
        Collider2D[] enemy_collider = Physics2D.OverlapCircleAll(Circle.position, Radius);
        if (enemy_collider != null)
        {
            for (int i = 0; i < enemy_collider.Length; i++)
            {
                if (enemy_collider[i].gameObject.TryGetComponent(out DamageHandlerEnemy enemy))
                {
                    enemy.TakeDamage(Damage);
                    Debug.Log("Take from " + enemy.name);
                }
            }
            Instantiate(_prefab_area, _transform_crack.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(Delay);
        IsDelay = false;
    }
    public override IEnumerator IAttack()
    {
        IsDelay = true;
        Collider2D[] enemy_collider = Physics2D.OverlapCircleAll(Circle.position, Radius);
        if (enemy_collider != null)
        {
            for (int i = 0; i < enemy_collider.Length; i++)
            {
                if (enemy_collider[i].gameObject.TryGetComponent(out DamageHandlerEnemy enemy))
                {
                    enemy.TakeDamage(Damage);
                    Debug.Log("Take from " + enemy.name);
                }
            }
        }
        yield return new WaitForSeconds(Delay);
        IsDelay = false;
    }

}
