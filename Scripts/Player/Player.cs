using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Movement
{
    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private PlayerAttack _player_attack;
    private AudioClip[] _stepSounds;
    public static Transform Transform;
    private Vector2 _currentDirection = Vector2.right;
    private float _step_interval = 0.5f;
    private float _step_iterval_attack = 1f;
    private float _last_step_time;
    private float _last_step_attack_time;

    private void Awake()
    {
        Transform = transform;
        _stepSounds = new AudioClip[]
        {
            GameResources.Sounds.StepPlayer1,
            GameResources.Sounds.StepPlayer2,
            GameResources.Sounds.StepPlayer3,
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
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
        Rigidbody2D.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * _characteristics.Speed,
            Input.GetAxis("Vertical") * _characteristics.Speed);
        if (Rigidbody2D.linearVelocity.x != 0 || Rigidbody2D.linearVelocity.y != 0)
        {
            Animator.SetBool("IsMove", true);
            if (Time.time - _last_step_time > _step_interval && !_player_attack.Attack)
            {
                PlaySoundStep();
                _last_step_time = Time.time;
            }
            else if (Time.time - _last_step_attack_time > _step_iterval_attack && _player_attack.Attack)
            {
                PlaySoundStep();
                _last_step_attack_time = Time.time;
            }
        }
        else
            Animator.SetBool("IsMove", false);
    }

    private void PlaySoundStep()
    {
        Sounds.Play(_stepSounds[Random.Range(0, _stepSounds.Length)], 0.05f);
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