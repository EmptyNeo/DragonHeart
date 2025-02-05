using UnityEngine;
[System.Serializable] // дает возможность сохранить этот класс в файл
public class PlayerData
{
    private string[] _itemNames;
    private int[] _itemAmounts;
    private float _health;
    private float _max_health;
    private float _max_mana;
    private float _damage;
    private int _class_health;
    private int _class_strength;
    private int _class_mana;
    private int _class_endurance;
    private int _class_craft;
    private int[] _balance;
    private int _level;
    private float _xp;
    private float _max_xp;
    private int _ability;
    private float x,y;
    public string[] ItemNames { get => _itemNames; private set => _itemNames = value; }
    public int[] ItemAmounts { get => _itemAmounts; private set => _itemAmounts = value; }
    public float Health { get => _health; private set => _health = value; }
    public float MaxHealth { get => _max_health; private set => _max_health = value; }
    public float ManaMax { get => _max_mana; private set => _max_mana = value; }
    public float Damage { get => _damage; private set => _damage = value; }
    public int ClassHealth { get => _class_health; private set => _class_health = value; }
    public int ClassStrength { get => _class_strength; private set => _class_strength = value; }
    public int ClassMana { get => _class_mana; private set => _class_mana = value; }
    public int ClassEndurance { get => _class_endurance; private set => _class_endurance = value; }
    public int ClassCraft { get => _class_craft; private set => _class_craft = value; }
    public int[] Balance { get => _balance; private set => _balance = value; }
    public int Level { get => _level; private set => _level = value; }
    public float Xp { get => _xp; private set => _xp = value; }
    public float MaxXp { get => _max_xp; private set => _max_xp = value; }
    public int Ability { get => _ability; private set => _ability = value; }
    public float X { get => x; private set => x = value; }
    public float Y { get => y; private set => y = value; }

    public PlayerData(Characteristics characteristics, LevelUpgrade level_upgrade, Inventory inventory, Wallet wallet)
    {
        ItemNames = new string[inventory.Slots.Count];
        ItemAmounts = new int[inventory.Slots.Count];
        Health = characteristics.Health;
        MaxHealth = characteristics.MaxHealth;
        ManaMax = characteristics.MaxMana;
        Balance = new int[wallet.Balance.Length];
        ClassHealth = level_upgrade.Health;
        ClassStrength = level_upgrade.Strength;
        ClassMana = level_upgrade.Mana;
        ClassEndurance = level_upgrade.Endurance;
        ClassCraft = level_upgrade.Craft;
        Level = LevelUpgrade.level;
        Xp = level_upgrade.Xp;
        MaxXp = level_upgrade.MaxXp;
        Ability = level_upgrade.Ability;
        X = characteristics.gameObject.transform.position.x;
        Y = characteristics.gameObject.transform.position.y;
       
        for (int i = 0; i < inventory.Slots.Count; i++)
        {
            if (inventory.Slots[i] != null &&inventory.Slots[i].Item != null)
            {
                Debug.Log(inventory.Slots[i].Item);
                ItemNames[i] = inventory.Slots[i].Item.name;
                ItemAmounts[i] = inventory.Slots[i].Amount;
            }
        }
        for(int i = 0; i < wallet.Balance.Length; i++)
        {
            Balance[i] = wallet.Balance[i];
        }
        
    }
    public void Initialization(LevelUpgrade level_upgrade)
    {
        level_upgrade.Initialization(Ability,
                                    MaxXp,
                                    Xp,
                                    ClassHealth,
                                    ClassStrength,
                                    ClassMana,
                                    ClassEndurance,
                                    ClassCraft);
    }
}
