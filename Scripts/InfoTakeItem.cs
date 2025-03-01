using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InfoTakeItem : MonoBehaviour
{

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private List<ItemsInfo> _items = new();
    public bool WasAnimationPlayed;
    public void AddItemInList(ItemScriptableObject item, int amount)
    {
        try
        {
            ItemsInfo itemInfo = new ItemsInfo()
            {
                Item = item,
                Amount = amount
            };
            _items.Add(itemInfo);
        }
        catch
        {
            Debug.Log("Ones of the parameters doesn`t match type list");
        }
      
    }

    public void UpdateInfoInAnimator()
    {
        if(_items.Count > 0){
            UpdateInfo();
        }
        else {
            gameObject.SetActive(false);
            _items.Clear();
            WasAnimationPlayed = false;
        }
    }
    public void UpdateInfo()
    {
        _icon.sprite = _items[^1].Item.Icon;
        _name.text = _items[^1].Item.NameItem;
        _amount.text = "x" + _items[^1].Amount;
        _items.Remove(_items[^1]);
    }
    [Serializable]
    struct ItemsInfo : IEquatable<ItemsInfo>
    {
        public ItemScriptableObject Item;
        public int Amount;

        public bool Equals(ItemsInfo other)
        {
            return Equals(Item, other.Item) && Amount == other.Amount;
        }

        public override bool Equals(object obj)
        {
            return obj is ItemsInfo other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Item, Amount);
        }
    }
}
