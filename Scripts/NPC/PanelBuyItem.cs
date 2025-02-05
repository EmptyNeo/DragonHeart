using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelBuyItem : MonoBehaviour
{
    [SerializeField] private Image _icon_item;
    [SerializeField] private TextMeshProUGUI _name_item;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private TextMeshProUGUI _total_price;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private string currency_code;
    private int _amount_buy;
    [HideInInspector] public ItemScriptableObject Item;
    public Image IconItem => _icon_item;
    public TextMeshProUGUI NameItem => _name_item;
    public TextMeshProUGUI Price => _price;
    public TextMeshProUGUI Amount => _amount;
    public TextMeshProUGUI TotalPrice => _total_price;
    public string CurrencyCode => currency_code;
    public void BuyItem()
    {
        int index_coin = Convert.ToInt32(Item.Coin);
        int total_price = Item.Price * _amount_buy;
        if (_wallet.Balance[index_coin] >= total_price)
        {
            Inventory.inventory.AddItem(Item, _amount_buy);
            _wallet.Reduction(index_coin, total_price);
            _amount.text = Item.Count.ToString();
            _amount_buy = Item.Count;
        }
        else
        {
            Notification.Instance.SetNotification($"Не хватает {total_price - _wallet.Balance[index_coin]} {currency_code[index_coin]}");
            Notification.Instance.TurnBackground(true);
            StartCoroutine(Notification.Instance.TurnOffBackgroundOverTime(3));
        }
       
    }
    public void AmountBuy(ItemScriptableObject item)
    {
        _amount_buy = item.Count;
    }
    public void Minus()
    {
        if (_amount_buy - 1 >= Item.Count)
        {
            _amount.text = (--_amount_buy).ToString();
            if (_amount_buy * Item.Price <= _wallet.Balance[Convert.ToInt32(Item.Coin)])
                _amount.color = Color.black;

        }
    }
    public void Plus()
    {
        _amount.text = (++_amount_buy).ToString();
        if (_amount_buy + 1 * Item.Price > _wallet.Balance[Convert.ToInt32(Item.Coin)])
            _amount.color = Color.red;
    }

}
