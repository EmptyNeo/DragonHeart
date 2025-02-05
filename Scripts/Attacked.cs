using System.Collections;
using UnityEngine;
public abstract class Attacked : MonoBehaviour
{
    [SerializeField] protected float _delay;
    [SerializeField] protected float _damage;
    [SerializeField] private string _name_attack;
    [SerializeField] protected Transform _circle;
    [SerializeField] private float _radius;
    [SerializeField] private float _pos_y;
    public bool IsDelay;
    public float Damage { get => _damage; }
    public float Delay { get => _delay; }
    public string NameAttack { get => _name_attack; }
    public Transform Circle { get => _circle; }
    public float Radius { get => _radius; }
    public float PosY { get => _pos_y; }

    public void Initialization(float delay, float damage)
    {
        _delay = delay;
        _damage = damage;
    }
    public virtual void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0) && IsDelay == false)
        {
            OnAttack();
        }
    }
    public virtual void OnAttack()
    {
        Player.Transform.GetComponent<PlayerAttack>().AttackAnimOn();
    }
    public virtual void AttackOff()
    {
        Player.Transform.GetComponent<PlayerAttack>().AttackAnimOff();
    }
    public abstract IEnumerator IAttack();
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = Circle == null ? Vector3.zero : Circle.position;
        Gizmos.DrawWireSphere(position, Radius);
    }
}