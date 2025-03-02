using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBuyNPC : MonoBehaviour
{
    [SerializeField] private Image _icon;
    public ItemScriptableObject Item;
    public PanelBuyItem PanelBuyItem;
    public Image Icon => _icon;
    public void ViewInfoProduct()
    {
        PanelBuyItem.Item = Item;
        PanelBuyItem.AmountBuy(Item);
        PanelBuyItem.IconItem.sprite = Item.Icon;
        PanelBuyItem.NameItem.text = Item.NameItem;
        PanelBuyItem.Price.text = $"Стоимость товара: {Item.Price} {PanelBuyItem.CurrencyCode[Convert.ToInt32(Item.Coin)]}";
        PanelBuyItem.Amount.text = Item.Count.ToString();
        PanelBuyItem.Amount.color = Color.black;
        PanelBuyItem.TotalPrice.text = $"Итоговая стоимость: {Item.Price} {PanelBuyItem.CurrencyCode[Convert.ToInt32(Item.Coin)]}"; 
        PanelBuyItem.gameObject.SetActive(true);
    }

}
