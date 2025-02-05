using UnityEngine;

public class Trees : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Player _))
             _sprite.color = new Color(1, 1, 1, 0.33f);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
         if(other.TryGetComponent(out Player _))
            _sprite.color = new Color(1, 1, 1, 1);
    }
}
