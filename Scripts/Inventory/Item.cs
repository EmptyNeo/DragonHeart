using DG.Tweening;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Item : Quantity
{
    [SerializeField] private GameObject _shadow;
    [SerializeField] private ItemScriptableObject _item;
    private Vector3 _original_position;
    public ItemScriptableObject ItemObject
    {
        get => _item;
        set => _item = value;
    }

    public GameObject Shadow => _shadow;
    public void Start()
    {
        if(transform.parent != null)
            _shadow.SetActive(false);
        _original_position = transform.position;
        if(_amount > _item.MaximumAmount)
        {
            Instantiate(_item.ItemPrefab, new(transform.position.x - Random.Range(-1,2),transform.position.y - Random.Range(-1, 2)), Quaternion.identity).GetComponent<Item>()._amount = _amount - _item.MaximumAmount;
            _amount = _item.MaximumAmount;
        }
        transform.DOMoveY(_original_position.y + Vector3.up.y / 10, Random.Range(0.9f, 1)).SetLoops(-1, LoopType.Yoyo);
    }
}
