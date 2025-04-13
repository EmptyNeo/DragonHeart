using UnityEngine;

public enum Type
{
    HealHp,
    HealMana,
    Weapon,
    WeaponRanged,
    Armor,
    MagicAbility,
    Other
}

public enum TypeInInventory
{
    Weapon,
    ConsumableItem,
    Armor,
    Others
}

public enum TypeCoin
{
    bronze,
    silver,
    gold
}

public abstract class ItemScriptableObject : ScriptableObject
{
    [SerializeField] private string _name_item;
    [SerializeField] private int _maximum_amount;
    [Header("")] [SerializeField] private int _count;
    [SerializeField] private int _price;
    [SerializeField] private TypeCoin _coin;
    [Header("")] [SerializeField] private GameObject _item_prefab;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Type _type;
    [SerializeField] private TypeInInventory _type_in_inventory;
    [SerializeField] private MainItem _main_item;
    [SerializeField] private WeaponItem _weapon_item;
    [SerializeField] private ArmorItem _armor_item;
    public string NameItem => _name_item;
    public int MaximumAmount => _maximum_amount;
    public int Count => _count;
    public int Price => _price;
    public TypeCoin Coin => _coin;

    public GameObject ItemPrefab
    {
        get => _item_prefab;
        set => _item_prefab = value;
    }
    public Sprite Icon => _icon;
    public Type Type => _type;
    public TypeInInventory TypeInInvetory => _type_in_inventory;
    public MainItem MainItem => _main_item;
    public WeaponItem WeaponItem => _weapon_item;
    public ArmorItem ArmorItem => _armor_item;
    public abstract string GetDescription();
}