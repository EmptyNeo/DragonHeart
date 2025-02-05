using UnityEngine;

public class PlayerMoveBoost : MonoBehaviour
{
     [SerializeField] private KeyCode key;
     private Characteristics _characteristics => GetComponent<Characteristics>();
     private LevelUpgrade _level_upgrade => GetComponent<LevelUpgrade>();
     private Player _player => GetComponent<Player>();
    private void Update()
    {
        if (Input.GetKey(key) && _characteristics.Endurance > 0 && (_player.Rigidbody2D.linearVelocity.x != 0 || _player.Rigidbody2D.linearVelocity.y != 0))
        {
            _characteristics.Speed = _characteristics.SpeedBoost;
            _characteristics.TakeEndurance(Time.deltaTime);
        }
        else if (_characteristics.Endurance < _characteristics.MaxEndurance)
        {
            _characteristics.Speed = (_level_upgrade.Endurance * 5 / 100) + 5;
            if (_player.Rigidbody2D.linearVelocity.x == 0 && _player.Rigidbody2D.linearVelocity.y == 0)
                _characteristics.AddUpEndurance(Time.deltaTime);

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _characteristics.TakeDamage(1);
            _characteristics.TakeMana(1);
        }
    }
}
