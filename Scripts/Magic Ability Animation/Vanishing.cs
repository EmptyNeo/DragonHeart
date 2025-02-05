using System.Collections;
using UnityEngine;

public class Vanishing : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite_renderer;
    [SerializeField] private Sprite[] _sprites;

    [SerializeField] private float _max_time;
    [SerializeField] private float _visibility = 1;

    [SerializeField] private float _animation_speed;

    private float _time;
    private int _sprite_index;
    private float _animation_timer;

    private void FixedUpdate()
    {
        _time += Time.deltaTime;
        if (_time > _max_time - 0.5f)
        {
            _sprite_renderer.color = new Color(1, 1, 1, _visibility -= 0.1f);
            _animation_timer += Time.deltaTime;
            if (_animation_timer >= _animation_speed / _sprites.Length)
            {
                _animation_timer = 0;
                _sprite_index = (_sprite_index + 1) % _sprites.Length;
                _sprite_renderer.sprite = _sprites[_sprite_index];
            }
        }
    }

}
