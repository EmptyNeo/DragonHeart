using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum TypeEquipment { Default, Weapon }
public class DragAndDropItemEquipment : DragAndDropItem
{
    [SerializeField] private PlayerEquipmentHandler _player_equipment_handler;
    [SerializeField] private InventorySlot _slot_equipment;
    [SerializeField] private TypeEquipment _type_equipment;
    public TypeEquipment TypeEquipment => _type_equipment;
    public override void Update()
    {
        if (_on_pointer_enter == true && _oldSlot.Item != null)
        {
            for (int i = 0; i < _quick_slot_panel.childCount; i++)
            {

                if (_quick_slot_panel.GetChild(i).GetComponent<InventorySlot>().Item != null && i + 1 < 10 && Input.GetKeyDown((i + 1).ToString()))
                {
                    NullifyByType();
                    if (_quick_slot_panel.GetChild(i).GetComponent<InventorySlot>().Item.Type == _slot_equipment.Item.Type)
                    {
                        ExchangeSlotData(_quick_slot_panel.GetChild(i).GetComponent<InventorySlot>());
                        _player_equipment_handler.EquipmentByType(_slot_equipment);
                    }
                    break;
                }
                else if(_quick_slot_panel.GetChild(i).GetComponent<InventorySlot>().Item == null && i + 1 < 10 && Input.GetKeyDown((i + 1).ToString()))
                {
                    NullifyByType();
                    ExchangeSlotData(_quick_slot_panel.GetChild(i).GetComponent<InventorySlot>());
                }
            }

        }
    }

    private void NullifyByType()
    {
        switch (_slot_equipment.Item.Type)
        {
            case Type.Weapon:
                _player_equipment_handler.NullifyWeapon();
                break;
            case Type.Armor:
                _player_equipment_handler.NullifyArmor(_oldSlot.Item);
                break;
            case Type.MagicAbility:
                _player_equipment_handler.NullifyMagicAbility();
                break;
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {

        if (_oldSlot.isEmpty || _dont_is_dragging)
        {
            _dont_is_dragging = false;
            return;
        }

        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);

        GetComponentInChildren<Image>().raycastTarget = true;
        GetComponentInChildren<RectTransform>().localScale = new Vector2(.9f, .9f);
        transform.SetParent(_oldSlot.transform);
        transform.position = _oldSlot.transform.position;

        if (eventData.pointerCurrentRaycast.gameObject == UI_PANEL)
        {
            Quantity item = Instantiate(_oldSlot.Item.ItemPrefab).GetComponent<Quantity>();
            item.Initialization(_oldSlot.Amount);
            item.transform.position = _player.position + Vector3.up + _player.forward;

            switch (_slot_equipment.Item.Type)
            {
                case Type.Weapon:
                    _player_equipment_handler.NullifyWeapon();
                    break;
                case Type.Armor:
                    _player_equipment_handler.NullifyArmor(_oldSlot.Item);
                    break;
                case Type.MagicAbility:
                    _player_equipment_handler.NullifyMagicAbility();
                    break;
            }
            NullifySlotData();

        }
        if(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.TryGetComponent(out InventorySlot inventory_slot))
        {
            if (inventory_slot.Item != null)
            {
                if (inventory_slot.Item != null && inventory_slot.Item.Type == _slot_equipment.Item.Type)
                {
                    NullifyByType();

                    ExchangeSlotData(inventory_slot);
                    _player_equipment_handler.EquipmentByType(_slot_equipment);
                }
            }
            else
            {
                NullifyByType();
                ExchangeSlotData(inventory_slot);
            }
        }
    }
}
