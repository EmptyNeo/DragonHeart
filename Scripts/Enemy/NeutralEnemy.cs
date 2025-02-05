using System.Collections;
using TMPro;
using UnityEngine;

public abstract class NeutralEnemy : Movement
{
    [SerializeField] private DamageHandlerEnemy _damage_handler_enemy;
    [SerializeField] private string _name_enemy;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Vector2[] _point;
    [SerializeField] private float _speed;
    private Vector2 _target;
    protected Player _player;

    private int _count;
    protected bool _is_delay;
    public int Count
    {
        get { return _count; }
        private set
        {
            if (value >= _point.Length)
                _count = 0;
            else
                _count = value;
        }
    }
    private void Start()
    {
        if(_name != null)
        {
            _name.text = _name_enemy;
            _name.color = Color.yellow;
        }
    }
    private void Update()
    {
        Move();
        Rotate(Direction());
    }

    public override Vector2 Direction()
    {
        return Rigidbody2D.linearVelocity;
    }

    public override void Move()
    {
        if (_is_delay)
            return;
        if (_player == null)
        {
            _target = _point[Count];
            if (Mathf.Abs(transform.position.x - _target.x) < 0.5f && Mathf.Abs(transform.position.y - _target.y) < 0.5f)
            {
                Count++;
            }
        }
        else if(_player != null && _damage_handler_enemy.Health < _damage_handler_enemy.MaxHealth)
        {
            _target = _player.gameObject.transform.position;
            _name.color = Color.red;
            if (Mathf.Abs(((Vector2)transform.position - _target).magnitude) < 3f)
            {
                StartCoroutine(Attack());
                Rigidbody2D.linearVelocity = Vector2.zero;
            }
        }
        Rigidbody2D.linearVelocity = (_target - (Vector2)transform.position).normalized * _speed;
    }
    public abstract IEnumerator Attack();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
            _player = player;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _))
            _player = null;
    }
}
