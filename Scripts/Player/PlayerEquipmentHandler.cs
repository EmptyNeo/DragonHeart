using System;
using UnityEngine;

public class PlayerEquipmentHandler : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _parent_weapon;
    [SerializeField] private TransformPoint[] _parent_weapon_equipment;
    [SerializeField] private InventorySlot _slot_weapon;
    [SerializeField] private InventorySlot _slot_helmet;
    [SerializeField] private InventorySlot _slot_breastplate;
    [SerializeField] private InventorySlot _slot_boots;
    [SerializeField] private InventorySlot _slot_magic_ability;
    [SerializeField] private QuickSlotsInventory _quick_slot_inventory;
    [SerializeField] private GameObject _panel_amount_arrow;
    [SerializeField] private SpriteRenderer _consumable_item;
    [SerializeField] private SpriteRenderer _trace_weapon;
    [Header("Default Sprite Player Head")]
    [SerializeField] private Sprite HeadReplacedDown;
    [SerializeField] private Sprite HeadReplacedUp;
    [SerializeField] private Sprite HeadReplacedRight;
    [SerializeField] private Sprite HeadReplacedLeft;

    [Header("Default Sprite Player Body")]
    [SerializeField] private Sprite BodyReplaced;

    [Header("Default Sprite Player Leg")]
    [SerializeField] private Sprite LegReplaced;
    private Characteristics _characteristics => GetComponent<Characteristics>();
    private PlayerReplacedSprite _player_replaced_sprite => GetComponent<PlayerReplacedSprite>();
    private PlayerAttack _player_attack => GetComponent<PlayerAttack>();
    public SpriteRenderer ConsumableItem => _consumable_item;
    public SpriteRenderer TraceWeapon => _trace_weapon;
    public GameObject ParentAmountArrow => _panel_amount_arrow;
    public InventorySlot SlotWeapon  => _slot_weapon; 
    public InventorySlot SlotHelmet => _slot_helmet; 
    public InventorySlot SlotBreastplate => _slot_breastplate;
    public InventorySlot SlotBoots => _slot_boots; 
    public InventorySlot SlotMagicAbility => _slot_magic_ability; 

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            DragAndDropItem drag = _quick_slot_inventory.transform.GetChild(_quick_slot_inventory.CurrentQuickSlotId).GetChild(0).GetComponent<DragAndDropItem>();
            if (drag.OldSlot.Item != null)
            {
                EquipmentByType(drag.OldSlot);
            }
        }
    }

    public void EquipmentByType(InventorySlot slot)
    {
        switch (slot.Item.Type)
        {
            case Type.Weapon:
                TryEquipmentWeapon(slot);
                break;
            case Type.Armor:
                TryEquipmentArmor(slot);
                break;
            case Type.MagicAbility:
                if (slot.Item is MagicAbilityItem magic_item)
                    EquipmentMagicAbility(slot,magic_item);
                break;

        }
    }
    private void TryEquipmentWeapon(InventorySlot slot)
    {
        LevelUpgrade level_upgrade = GetComponent<LevelUpgrade>();
        string[] characters = level_upgrade.GetCharactersString();
        int[] necessary = new int[Enum.GetNames(typeof(TypeNecessaryCharacteristic)).Length];
        for (int i = 1; i < characters.Length - 1; i++)
        {
            int.TryParse(characters[i], out necessary[i - 1]);
        }

        if (necessary[slot.Item.WeaponItem.IndexNecessaryCharacteristic] >= slot.Item.WeaponItem.NecessaryCharacteristic)
        {
            if (_slot_weapon.Item != null)
            {
                NullifyWeapon();
                BackInQuickSlot(slot);
            }

            EquipmentWeapon(slot);
        }
    }
    public void EquipmentWeapon(InventorySlot slot)
    {
        _slot_weapon.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        Attacked weapon = Instantiate(slot.Item.ItemPrefab).GetComponent<Attacked>();
        if (weapon is Equipped equipped)
        { 
            equipped.Action(_parent_weapon_equipment[equipped.GetIndex()].transform, _player_attack, this);
        }
        else equipped = null;

        _parent_weapon = weapon.transform.parent.GetComponent<SpriteRenderer>();
        _parent_weapon.sprite = slot.Item.ItemPrefab.GetComponent<SpriteRenderer>().sprite;

        if (equipped != null)
            weapon.gameObject.transform.position = _parent_weapon_equipment[equipped.GetIndex()].Point.position;

        weapon.Initialization(slot.Item.WeaponItem.Delay, slot.Item.WeaponItem.Damage + _player_attack.Damage);
        _characteristics.InitializationText(weapon.Damage);
        _player_attack.Weapon = weapon;

        weapon.enabled = true;
        Destroy(weapon.gameObject.GetComponent<SpriteRenderer>());
        weapon.gameObject.GetComponent<Item>().Shadow.SetActive(false);
        Destroy(weapon.gameObject.GetComponent<Item>());
        Destroy(weapon.gameObject.GetComponent<CircleCollider2D>());
        slot.transform.GetChild(0).GetComponent<DragAndDropItem>().ExchangeSlotData(_slot_weapon);
    }

    public void NullifyWeapon()
    {
        if (_player_attack.Weapon != _player_attack)
        {
            _player_attack.AttackAnimOff();
            Destroy(_player_attack.Weapon.gameObject);
            _characteristics.InitializationText(_player_attack.Damage);
            _slot_weapon.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            _parent_weapon.sprite = null;
            _player_attack.Weapon = _player_attack;
            _player_attack.IsDelay = false;
        }
    }

    public void BackInQuickSlot(InventorySlot slot)
    {
        if (slot.Item == null)
            _slot_weapon.transform.GetChild(0).GetComponent<DragAndDropItem>().ExchangeSlotData(slot);
    }
    private void TryEquipmentArmor(InventorySlot slot)
    {
        if (slot.Item.ArmorItem != null)
        {
            int physical_protection = _characteristics.TotalPhysicalProtection + slot.Item.ArmorItem.PhysicalProtection;
            int magic_protection = _characteristics.TotalMagicProtection + slot.Item.ArmorItem.MagicProtection;

            _characteristics.Initialization(physical_protection, magic_protection);

            EquipmentArmor(slot, slot.transform.GetChild(0).GetComponent<DragAndDropItem>());
        }
    }
    public void EquipmentArmor(InventorySlot slot, DragAndDropItem drag_and_drop_item)
    {
        switch (slot.Item.ArmorItem.TypeArmor)
        {
            case TypeArmor.Helmet:
                _slot_helmet.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                _player_replaced_sprite.HeadReplacedDown = slot.Item.ArmorItem.Armor[0];
                _player_replaced_sprite.HeadReplacedUp = slot.Item.ArmorItem.Armor[1];
                _player_replaced_sprite.HeadReplacedRight = slot.Item.ArmorItem.Armor[2];
                _player_replaced_sprite.HeadReplacedLeft = slot.Item.ArmorItem.Armor[3];

                drag_and_drop_item.ExchangeSlotEquipmentArmor(_characteristics, _slot_helmet);
                break;
            case TypeArmor.Breastplate:
                _slot_breastplate.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                _player_replaced_sprite.BodyReplaced = slot.Item.ArmorItem.Armor[0];
                drag_and_drop_item.ExchangeSlotEquipmentArmor(_characteristics, _slot_breastplate);
                break;
            case TypeArmor.Boots:
                _slot_boots.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                _player_replaced_sprite.LegReplaced = slot.Item.ArmorItem.Armor[0];
                drag_and_drop_item.ExchangeSlotEquipmentArmor(_characteristics, _slot_boots);
                break;
        }
    }
    public void NullifyArmor(ItemScriptableObject item)
    {
        int physical_protection = _characteristics.TotalPhysicalProtection - item.ArmorItem.PhysicalProtection;
        int magic_protection = _characteristics.TotalMagicProtection - item.ArmorItem.MagicProtection;

        _characteristics.Initialization(physical_protection, magic_protection);

        switch (item.ArmorItem.TypeArmor)
        {
            case TypeArmor.Helmet:
                _slot_helmet.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                _player_replaced_sprite.HeadReplacedDown = HeadReplacedDown;
                _player_replaced_sprite.HeadReplacedUp = HeadReplacedUp;
                _player_replaced_sprite.HeadReplacedRight = HeadReplacedRight;
                _player_replaced_sprite.HeadReplacedLeft = HeadReplacedLeft;
                break;
            case TypeArmor.Breastplate:
                _slot_breastplate.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                _player_replaced_sprite.BodyReplaced = BodyReplaced;
                break;
            case TypeArmor.Boots:
                _slot_boots.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                _player_replaced_sprite.LegReplaced = LegReplaced;
                break;
        }
    }

    public void StartGameEquipmentByType(ItemScriptableObject item)
    {
        switch (item.Type)
        {
            case Type.Weapon:
                StartGameEquipmentWeapon(item);
                break;
            case Type.Armor:
                int physical_protection = _characteristics.TotalPhysicalProtection + item.ArmorItem.PhysicalProtection;
                int magic_protection = _characteristics.TotalMagicProtection + item.ArmorItem.MagicProtection;

                _characteristics.Initialization(physical_protection, magic_protection);
                StartGameEquipmentArmor(item);
                break;
            case Type.MagicAbility:
                if (item is MagicAbilityItem magic_item)
                    StartEquipmentMagicAbility(magic_item);
                break;
        }
    }

    public void StartGameEquipmentWeapon(ItemScriptableObject item)
    {
        _slot_weapon.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        Attacked weapon = Instantiate(item.ItemPrefab).GetComponent<Attacked>();
        if (weapon is Equipped equipped)
            equipped.Action(_parent_weapon_equipment[equipped.GetIndex()].transform, _player_attack, this);
        else
            equipped = null;

        _parent_weapon = weapon.transform.parent.GetComponent<SpriteRenderer>();
        _parent_weapon.sprite = item.ItemPrefab.GetComponent<SpriteRenderer>().sprite;

        if (equipped != null)
            weapon.gameObject.transform.position = _parent_weapon_equipment[equipped.GetIndex()].Point.position;

        _player_attack.Weapon = weapon;
        
        weapon.Initialization(item.WeaponItem.Delay, item.WeaponItem.Damage + _player_attack.Damage);

        Destroy(weapon.gameObject.GetComponent<SpriteRenderer>());
        weapon.gameObject.GetComponent<Item>().Shadow.SetActive(false);
        Destroy(weapon.gameObject.GetComponent<Item>());
        Destroy(weapon.gameObject.GetComponent<CircleCollider2D>());
    }

    public void StartGameEquipmentArmor(ItemScriptableObject item)
    {
        switch (item.ArmorItem.TypeArmor)
        {
            case TypeArmor.Helmet:
                _slot_helmet.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                _player_replaced_sprite.HeadReplacedDown = item.ArmorItem.Armor[0];
                _player_replaced_sprite.HeadReplacedUp = item.ArmorItem.Armor[1];
                _player_replaced_sprite.HeadReplacedRight = item.ArmorItem.Armor[2];
                _player_replaced_sprite.HeadReplacedLeft = item.ArmorItem.Armor[3];
                break;
            case TypeArmor.Breastplate:
                _slot_breastplate.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                _player_replaced_sprite.BodyReplaced = item.ArmorItem.Armor[0];
                break;
            case TypeArmor.Boots:
                _slot_boots.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                _player_replaced_sprite.LegReplaced = item.ArmorItem.Armor[0];
                break;
        }
    }
    public void EquipmentMagicAbility(InventorySlot slot,MagicAbilityItem item)
    {
        if (_player_attack.Weapon != null && _player_attack.Weapon is MagicStaff magic_staff)
        {
            magic_staff.MagicAbility = item.MagicAbility;
        }
        slot.transform.GetChild(0).GetComponent<DragAndDropItem>().ExchangeSlotData(_slot_magic_ability);
        _slot_magic_ability.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }
    public void StartEquipmentMagicAbility(MagicAbilityItem item)
    {
        if (_player_attack.Weapon != null && _player_attack.Weapon is MagicStaff magic_staff)
        {
            magic_staff.MagicAbility = item.MagicAbility;
        }
        _slot_magic_ability.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }
    public void NullifyMagicAbility()
    {
        if (_player_attack.Weapon != null && _player_attack.Weapon is MagicStaff magic_staff)
        {
            magic_staff.MagicAbility = null;
        }
        _slot_magic_ability.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }
}
