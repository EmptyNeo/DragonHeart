using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Movement
{
    [SerializeField] private Characteristics _characteristics;
    public static Transform Transform;
    private Vector2 _currentDirection = Vector2.right;
    private void Awake() => Transform = transform;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("Game");
        }
        Rotate(Round(Direction()));
      
    }
    private void FixedUpdate()
    {
        Move();
    }
    public override void Move()
    {
        Rigidbody2D.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * _characteristics.Speed, Input.GetAxis("Vertical") * _characteristics.Speed);
        if (Rigidbody2D.linearVelocity.x != 0 || Rigidbody2D.linearVelocity.y != 0)
            Animator.SetBool("IsMove", true);
        else
        {
            Animator.SetBool("IsMove", false);
        }


    }
    
    public override Vector2 Direction()
    {
        return ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;
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
