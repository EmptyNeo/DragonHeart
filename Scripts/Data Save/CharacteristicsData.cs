[System.Serializable]
public class CharacteristicsData
{
    private int _amount_save;
    private string _name_class;

    private int _class_health;
    private int _class_strength;
    private int _class_mana;
    private int _class_endurance;
    private int _class_craft;
    private string _weapon_name;
    private string _boots_name;
    private string _breastplate_name;
    private string _helmet_name;

    public int AmountSave { get => _amount_save; private set => _amount_save = value; }
    public string NameClass { get => _name_class; private set => _name_class = value; }

    public int ClassHealth { get => _class_health; private set => _class_health = value; }
    public int ClassStrength { get => _class_strength; private set => _class_strength = value; }
    public int ClassMana { get => _class_mana; private set => _class_mana = value; }
    public int ClassEndurance { get => _class_endurance; private set => _class_endurance = value; }
    public int ClassCraft { get => _class_craft; private set => _class_craft = value; }
    public string WeaponName { get => _weapon_name; private set => _weapon_name = value; }
    public string BootsName { get => _boots_name; private set => _boots_name = value; }
    public string BreastplateName { get => _breastplate_name; private set => _breastplate_name = value; }
    public string HelmetName { get => _helmet_name; private set => _helmet_name = value; }

    public CharacteristicsData(Class _class, int index, int amount_save)
    {
        AmountSave = amount_save;
        NameClass = _class.Name_Class[index];
        ClassHealth = _class.Health[index];
        ClassStrength = _class.Strength[index];
        ClassMana = _class.Mana[index];
        ClassEndurance = _class.Endurance[index];
        ClassCraft = _class.Craft[index];
        if (_class.WeaponItem[index] != null)
        {
            WeaponName = _class.WeaponItem[index].name;
            BootsName = _class.BootsItem[index].name;
            BreastplateName = _class.BreasplateItem[index].name;
            HelmetName = _class.HelmetItem[index].name;
        }
    }
    public void Initialization(LevelUpgrade level_upgrade)
    {
        level_upgrade.StartGameInitialization(ClassHealth,
                                               ClassStrength,
                                               ClassMana,
                                               ClassEndurance,
                                               ClassCraft);
    }
}