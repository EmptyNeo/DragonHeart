using UnityEngine;

public abstract class Movement : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rb;

    public Rigidbody2D Rigidbody2D { get => _rb; }
    public Animator Animator { get => _animator; }

    public abstract void Move();
    public abstract Vector2 Direction();
    public virtual void Rotate(Vector2 direction)
    {
        _animator.SetFloat("Horizontal", direction.x);
        _animator.SetFloat("Vertical", direction.y);
    }
}
