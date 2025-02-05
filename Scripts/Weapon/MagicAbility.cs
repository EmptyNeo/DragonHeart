using UnityEngine;

public abstract class MagicAbility : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _circle_collider;
    [SerializeField] private float _time;
    [SerializeField] private Rigidbody2D _rb;
    [HideInInspector] public float Damage;
    public CircleCollider2D CircleCollider => _circle_collider;
    public float Time => _time;
    public Rigidbody2D Rigidbody2D => _rb;
    
    public abstract void Ability();
   
}
