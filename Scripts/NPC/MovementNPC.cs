using TMPro;
using UnityEngine;

public class MovementNPC : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform minX;
    [SerializeField] private Transform maxX;
    [SerializeField] private Transform minY;
    [SerializeField] private Transform maxY;
    private Rigidbody2D rb;
    private bool try_touch_player;
    private Vector2 targetPosition; 
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
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.MovePosition(targetPosition);

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
        if (other.TryGetComponent(out Player _)) try_touch_player = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _)) try_touch_player = false;
    }
}
  
