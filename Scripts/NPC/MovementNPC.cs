using TMPro;
using UnityEngine;

public class MovementNPC : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform minX;
    [SerializeField] private Transform maxX;
    [SerializeField] private Transform minY;
    [SerializeField] private Transform maxY;
    [SerializeField] private Animator _animator;
    private Rigidbody2D rb;
    private bool try_touch_player;
    private Vector2 targetPosition;
    public Vector2 Velocity { get; private set; }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetNewRandomDirection();
    }

    private void FixedUpdate()
    {
        if (try_touch_player == false)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime); 
        rb.MovePosition(newPosition);
        Velocity = (newPosition - rb.position) / Time.deltaTime;
        
        Velocity = Round(Velocity);
        _animator.SetFloat("Horizontal", Velocity.x);
        _animator.SetFloat("Vertical", Velocity.y);
        _animator.SetBool("IsMove", true);
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewRandomDirection();
        }
    }

    private void SetNewRandomDirection()
    {
        float randomX = Random.Range(minX.position.x, maxX.position.x);
        float randomY = Random.Range(minY.position.y, maxY.position.y);
        Collider[] colliders = Physics.OverlapSphere(new Vector2(randomX, randomY), 3);
        if(colliders.Length > 0)
        {
            SetNewRandomDirection();
            return;
        }
        targetPosition = new Vector2(randomX, randomY);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _))
        {
            try_touch_player = true;
            rb.linearVelocity = Vector2.zero;
            _animator.SetBool("IsMove", false);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _))
        {
            try_touch_player = false;
        }
    }
    private Vector2 Round(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle >= -45 && angle < 45)
        {
            return Vector2.right;
        }
        else if (angle >= 45 && angle < 135)
        {
            return Vector2.up;
        }
        else if (angle >= 135 || angle < -135)
        {
            return Vector2.left;
        }
        else
        {
            return Vector2.down;
        }
    }
}
  
