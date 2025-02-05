using UnityEngine;

public class Item : Quantity
{
    [SerializeField] private ItemScriptableObject _item;
    public ItemScriptableObject ItemObject => _item;

    private void Start()
    {
        if(_amount > _item.MaximumAmount)
        {
            Instantiate(_item.ItemPrefab, new(transform.position.x - Random.Range(-1,2),transform.position.y - Random.Range(-1, 2)), Quaternion.identity).GetComponent<Item>()._amount = _amount - _item.MaximumAmount;
            _amount = _item.MaximumAmount;
        }
    }
}
