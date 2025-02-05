using UnityEngine;

public enum TypeArmor { Helmet, Breastplate, Boots };
[CreateAssetMenu(fileName = "Armor Item", menuName = "Inventory/New Armor Item")]
public class ArmorItem : ItemScriptableObject
{
    [SerializeField] private TypeArmor _type_armor;
    [SerializeField] private int _physical_protection;
    [SerializeField] private int _magic_protection;
    [SerializeField] private Sprite[] _armor;
    public int PhysicalProtection => _physical_protection;
    public int MagicProtection => _magic_protection;
    public TypeArmor TypeArmor => _type_armor;
    public Sprite[] Armor => _armor;

    public override string GetDescription()
    {
        return NameItem + "\n" +
                "Когда надето: \n" +
                "+" + PhysicalProtection + " Физ. Броня \n" +
                "+" + MagicProtection + " Маг. Броня";
    }
}
