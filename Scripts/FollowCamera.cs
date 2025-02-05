using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private void FixedUpdate()
    {
        Vector2 lerp = Vector2.Lerp(transform.position, _player.position, 3 * Time.deltaTime);
        transform.position = new Vector3(lerp.x, lerp.y, -10);
    }
}
