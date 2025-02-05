using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class Coin : Quantity
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private int _coin_id;
    public int Id => _coin_id;
    private Wallet _wallet;
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Wallet wallet))
        {
            _wallet = wallet;
        }
    }
    private void Update()
    {
        if (_wallet != null)
        {
            _rb.linearVelocity = (Player.Transform.position - transform.position).normalized * _speed;
            if (Mathf.Abs((transform.position - Player.Transform.position).magnitude) < .1f)
            {
                _wallet.Addition(_coin_id, _amount);
                Destroy(gameObject);
            }
        }
    }
}

