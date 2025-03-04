using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttributeHandler : MonoBehaviour
{
    [SerializeField] private QuickSlotsInventory _quick_slot_inventory;
    [SerializeField] private SpriteRenderer _consumable_item;
    private Player _player => GetComponent<Player>();
    private Characteristics _characteristics => GetComponent<Characteristics>();
    private bool IsPlayingAnimationUseConsumableItem;
    private InventorySlot _slot;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && IsMaxCharacteristic() == false && IsConsumableItem())
        {
            StartAnimationUseConsumableItem();
        }
    }
    private void StartAnimationUseConsumableItem()
    {
        if(IsPlayingAnimationUseConsumableItem == false)
        {
            _player.Animator.SetBool("UseItem", true);
            IsPlayingAnimationUseConsumableItem = true;
            ItemScriptableObject itemScriptableObject = _quick_slot_inventory.gameObject.transform
                .GetChild(_quick_slot_inventory.CurrentQuickSlotId).GetComponent<InventorySlot>().Item;
            if (itemScriptableObject != null)
            {
                if(itemScriptableObject.MainItem.AudioClip != null)
                    Sounds.Play(itemScriptableObject.MainItem.AudioClip);
                
                _consumable_item.sprite = itemScriptableObject.ItemPrefab.
                                          GetComponent<SpriteRenderer>().sprite;
            }
            else 
            {
                _consumable_item.sprite = null;
            }
        }
    }

    public void AnimationUseConsumableItemOff()
    {
        if (IsPlayingAnimationUseConsumableItem == true)
        {
            _player.Animator.SetBool("UseItem", false);
            IsPlayingAnimationUseConsumableItem = false;
            ChangeCharacteristic();
        }
    }
    public void NullifySpriteConsumableItem()
    {
        _consumable_item.sprite = null;
    }
    public void ChangeCharacteristic()
    {
        
         _slot = _quick_slot_inventory.gameObject.transform.GetChild(_quick_slot_inventory.CurrentQuickSlotId).GetComponent<InventorySlot>();
        if (_slot.Item != null)
        {
            switch (_slot.Item.Type)
            {

                case Type.HealHp:
                    _characteristics.Health = AddUpCharacteristics(_slot, _characteristics.Health, _characteristics.MaxHealth);
                    _characteristics.HealthBar.fillAmount = _characteristics.Health / _characteristics.MaxHealth;
                    _characteristics.TextHealth.text = _characteristics.Health.ToString("N0") + "/" + _characteristics.MaxHealth;
                    break;
                case Type.HealMana:
                    _characteristics.Mana = AddUpCharacteristics(_slot, _characteristics.Mana, _characteristics.MaxMana);
                    _characteristics.ManaBar.fillAmount = _characteristics.Mana / _characteristics.MaxMana;
                    _characteristics.TextMana.text = _characteristics.Mana.ToString("N0") + "/" + _characteristics.MaxMana;
                    break;
            }
        }
    }
    public bool IsMaxCharacteristic()
    {
        _slot = _quick_slot_inventory.gameObject.transform.GetChild(_quick_slot_inventory.CurrentQuickSlotId).GetComponent<InventorySlot>();
        if(_slot.Item != null)
        {
            switch (_slot.Item.Type)
            {
                case Type.HealHp:
                    if (_characteristics.Health.Equals(_characteristics.MaxHealth))
                    {
                        return true;
                    }
                    break;
                case Type.HealMana:
                    if (_characteristics.Mana.Equals(_characteristics.MaxMana))
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
            }

        }
        else if(_slot.Item == null)
            return true;

            return false;
    }
    public bool IsConsumableItem()
    {
        _slot = _quick_slot_inventory.gameObject.transform.GetChild(_quick_slot_inventory.CurrentQuickSlotId).GetComponent<InventorySlot>();
        if (_slot.Item != null)
        {
            return _slot.Item.TypeInInvetory switch
            {
                TypeInInventory.ConsumableItem => true,
                _ => false
            };
        }
        else 
            return false;
    }
    private float AddUpCharacteristics(InventorySlot slot, float character, float max_character)
    {
        if (character.Equals(max_character))
            return max_character;

        character += slot.Item.MainItem.ChangeCharacteristics;
        SubtractValueSlot(slot);

        if (character >= max_character)
            return max_character;

        return character;
    }
    private void SubtractValueSlot(InventorySlot slot)
    {
        if (slot.Amount <= 1)
        { 
            _quick_slot_inventory.gameObject.transform.GetChild(_quick_slot_inventory.CurrentQuickSlotId).GetComponentInChildren<DragAndDropItem>().NullifySlotData();
        }
        else
        {

            slot.Amount--;
            slot.itemAmountText.text = slot.Amount.ToString();
        }
    }

}
