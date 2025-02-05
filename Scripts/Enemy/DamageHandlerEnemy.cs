using TMPro;
using UnityEngine;
using UnityEngine.UI;
using EnemyRewards;

public enum EnemyType { Default }

public class DamageHandlerEnemy : MonoBehaviour
{
    [SerializeField] private Image _bar_health;
    [SerializeField] private TextMeshProUGUI _text_health;
    [SerializeField] private float _health;
    [SerializeField] private float _max_health;
    [SerializeField] private float _xp;
    [SerializeField] private Rewards[] _rewards;
    
    [SerializeField] private EnemyType _type;
    public float Health => _health;
    public float MaxHealth => _max_health;
    public EnemyType Type { get => _type; }
    private void Start() 
    {
        _health = _max_health;
        _text_health.text = _health + "/" + _max_health;
    }
    public void TakeDamage(float damage) 
    {
        _health -= damage;
        _text_health.text = _health.ToString("N1") + "/" + _max_health;
        _bar_health.fillAmount = _health / _max_health;
        Debug.Log("Taken away " + damage);
        if (_health <= 0.001f) 
        {
            Destroy(gameObject);
            LevelUpgrade.Instance.TakeXp(_xp);
            Debug.Log("Take xp " + _xp);
            for(int i = 0; i < _rewards.Length; i++)
            {
                if(_rewards[i].Reward.TryGetComponent(out Quantity item))
                    Instantiate(_rewards[i].Reward, 
                    new Vector2(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(-1, 1)), 
                    Quaternion.identity).GetComponent<Quantity>().Initialization(_rewards[i].Amount);
                else 
                {
                    for(int j = 0; j < _rewards[i].Amount; j++)
                    {
                        Instantiate(_rewards[i].Reward, 
                        new Vector2(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(-1, 1)), 
                        Quaternion.identity);
                    }
                }
            
            }
        }
    }
}
namespace EnemyRewards
{
    using System;
    [Serializable]
    class Rewards 
    {
        [SerializeField] private int _amount;
        [SerializeField] private GameObject _reward;

        public int Amount => _amount;
        public GameObject Reward => _reward;
    }
}
