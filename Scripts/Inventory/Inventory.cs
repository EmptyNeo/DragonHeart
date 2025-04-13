using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private Transform QuickSlotPanel;
    [SerializeField] private Transform equipment;

    [SerializeField] private Player player;
    [SerializeField] private CompleteQuest _complete_quest;
    [SerializeField] private InfoTakeItem _info_take_item;
    [SerializeField] private List<InventorySlot> _slots = new();

    public static Inventory inventory;
    public List<InventorySlot> Slots => _slots;
    public Transform InventoryPanel => inventoryPanel;

    public void Initialization()
    {
        inventory = GetComponent<Inventory>();
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            _slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }

        for (int i = 0; i < QuickSlotPanel.childCount; i++)
        {
            _slots.Add(QuickSlotPanel.GetChild(i).GetComponent<InventorySlot>());
        }

        for (int i = 0; i < equipment.childCount; i++)
        {
            _slots.Add(equipment.GetChild(i).GetComponent<InventorySlot>());
        }
    }

    private void FixedUpdate() => TakeItem();

    public void TakeItem()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Collider2D[] colliders =
                Physics2D.OverlapBoxAll(player.gameObject.transform.position, new Vector2(1, 1), 0);
            List<Item> items = new();
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out Item item))
                {
                    items.Add(item);
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                AddItem(items[i].ItemObject, items[i].Amount);
                _complete_quest.UpdateQuest(items[i].ItemObject, items[i].Amount);
                _info_take_item.AddItemInList(items[i].ItemObject, items[i].Amount);
                Destroy(items[i].gameObject);
            }

            if (items.Count > 0)
            {
                Sounds.Play(GameResources.Sounds.TakeItem, 0.1f, 1.3f);
                if (_info_take_item.WasAnimationPlayed == false)
                {
                    _info_take_item.UpdateInfo();
                    _info_take_item.gameObject.SetActive(true);
                    _info_take_item.WasAnimationPlayed = true;
                }
            }
        }
    }

    public void AddItem(ItemScriptableObject _item, int _amount)
    {
        foreach (InventorySlot slot in _slots)
        {
            if (slot.Item == _item)
            {
                if (slot.Amount + _amount <= _item.MaximumAmount)
                {
                    slot.Amount += _amount;
                    slot.SetIcon(_item.Icon);
                    if (slot.Amount != 1) slot.itemAmountText.text = slot.Amount.ToString();
                    return;
                }

                _amount -= _item.MaximumAmount - slot.Amount;
                slot.Amount = _item.MaximumAmount;
                if (_item.MaximumAmount != 1) slot.itemAmountText.text = slot.Amount.ToString();
            }
        }

        bool allFull = true;
        foreach (InventorySlot slot in _slots)
        {
            if (slot.isEmpty)
            {
                allFull = false;
                slot.Item = Instantiate(_item);
                slot.Amount = _amount;
                slot.isEmpty = false;
                slot.SetIcon(_item.Icon);
                if (_amount <= _item.MaximumAmount)
                {
                    slot.Amount = _amount;
                    if (slot.Amount != 1) slot.itemAmountText.text = slot.Amount.ToString();
                    break;
                }

                slot.Amount = _item.MaximumAmount;
                _amount -= _item.MaximumAmount;
                if (_item.MaximumAmount != 1) slot.itemAmountText.text = slot.Amount.ToString();
            }
            else allFull = true;
        }

        if (allFull)
        {
            Notification.Instance.SetNotification(TextMessages.InventoryIsFull);
            Notification.Instance.TurnBackground(true);
            StartCoroutine(Notification.Instance.TurnOffBackgroundOverTime(3));
            Instantiate(_item.ItemPrefab, new Vector2(player.transform.position.x, player.transform.position.y),
                Quaternion.identity).GetComponent<Item>().Initialization(_amount);
        }
    }

    public void AddItemToSlot(ItemScriptableObject _item, int _amount, int slotId)
    {
        InventorySlot slot = _slots[slotId];
        slot.Item = Instantiate(_item);
        slot.isEmpty = false;
        slot.SetIcon(_item.Icon);
        if (_amount <= _item.MaximumAmount)
        {
            slot.Amount = _amount;
            if (slot.Amount != 1) slot.itemAmountText.text = slot.Amount.ToString();
        }
    }

    public void TakeItAwayAmountToSlot(int _amount, int slotId)
    {
        InventorySlot slot = _slots[slotId];
        slot.Amount -= _amount;
    }

    public void RemoveItemFromSlot(int slotId)
    {
        InventorySlot slot = _slots[slotId];
        slot.Item = null;
        slot.isEmpty = true;
        slot.Amount = 0;
        slot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        slot.iconGO.GetComponent<Image>().sprite = null;
        slot.itemAmountText.text = " ";
    }

    public int AmountBullet(ItemScriptableObject item, int amount_bullet, int max_bullet)
    {
        List<int> index = new();
        for (int i = 0; i < _slots.Count; i++)
        {
            if (_slots[i].Item != null && _slots[i].Item == item)
            {
                index.Add(i);
            }
        }

        for (int i = 0; i < index.Count; i++)
        {
            if (amount_bullet + _slots[index[i]].Amount < max_bullet)
            {
                amount_bullet += _slots[index[i]].Amount;
                RemoveItemFromSlot(index[i]);
            }
            else if (amount_bullet + _slots[i].Amount >= max_bullet)
            {
                TakeItAwayAmountToSlot(amount_bullet - max_bullet, index[i]);
            }
        }

        return amount_bullet;
    }
}