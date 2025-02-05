using System.Collections;
using UnityEngine;

public class Sword : ComboAttacked, ISpecialAttack
{
    [SerializeField] private EnemyType _enemy_type;
   
    public  IEnumerator SpecialAttack()
    {
        throw new System.NotImplementedException();
    }
    public override IEnumerator IAttack()
    {
        IsDelay = true;
        Collider2D[] enemy_collider = Physics2D.OverlapCircleAll(Circle.position, Radius);
        if (enemy_collider != null)
        {
            for (int i = 0; i < enemy_collider.Length; i++)
            {
                if (enemy_collider[i].gameObject.TryGetComponent(out DamageHandlerEnemy enemy) && _enemy_type == enemy.Type)
                {
                    enemy.TakeDamage(Damage);
                    Debug.Log("Take from " + enemy.name);
                }
                else Debug.Log("This is Sword don`t attacked Enemy with this TypeEnemy: " + _enemy_type);
            } 
        }
        yield return new WaitForSeconds(Delay);
        IsDelay = false;
    }
}
