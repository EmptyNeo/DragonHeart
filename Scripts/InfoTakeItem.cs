using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InfoTakeItem : MonoBehaviour
{

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _amount;

    [SerializeField] private List<Sprite> _item_icon;
    [SerializeField] private List<string> _name_item;
    [SerializeField] private List<int> _item_amount;

    [SerializeField] private int _length_item;

    public void AddItemInList(Sprite icon, string name_item, int amount)
    {
        try
        {
            _item_icon.Add(icon);
            _name_item.Add(name_item);
            _item_amount.Add(amount);
            _length_item++;
            
        }
        catch
        {
            Debug.Log("Ones of the parameters doesn`t match type list");
        }
      
    }

    public void UpdateInfoInAnimator()
    {
        _length_item--;
        if(_length_item >= 0){
            UpdateInfo();
        }
        else {
            gameObject.SetActive(false);
            _item_icon.Clear();
            _name_item.Clear();
            _item_amount.Clear();
            _length_item = -1;
        }
    }
    public void UpdateInfo()
    {
        _icon.sprite = _item_icon[_length_item];
        _name.text = _name_item[_length_item];
        _amount.text = "x" + _item_amount[_length_item];
    }
}
