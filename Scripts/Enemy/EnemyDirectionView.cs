using UnityEngine;

public class EnemyDirectionView : MonoBehaviour
{
    [SerializeField] private Sprite _down;
    [SerializeField] private Sprite _up;
    [SerializeField] private Sprite _right;
    [SerializeField] private Sprite _left;
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();
    private SpriteRenderer _sprite_renderer => GetComponent<SpriteRenderer>();

    private void Update()
    {
        float angle = Mathf.Atan2(_rb.linearVelocity.y, _rb.linearVelocity.x) * Mathf.Rad2Deg;

        if (angle >= -45 && angle < 45)
        {
            ReplaceSprites(_right);
        }
        else if (angle >= 45 && angle < 135)
        {
            ReplaceSprites(_up);
        }
        else if (angle >= 135 || angle < -135)
        {
            ReplaceSprites(_left);
        }
        else
        {
            ReplaceSprites(_down);
        }
    }

    private void ReplaceSprites(Sprite direction)
    {
        _sprite_renderer.sprite = direction;
    }
}
