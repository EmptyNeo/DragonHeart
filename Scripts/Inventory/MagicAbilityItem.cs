using UnityEngine;
[CreateAssetMenu(fileName = "Magic Ability", menuName = "Inventory/New Magic Ability Item")]
public class MagicAbilityItem : ItemScriptableObject
{
    [SerializeField] private string _ability_description;
    [SerializeField] private MagicAbility _magic_ability;
    public MagicAbility MagicAbility => _magic_ability;
    public override string GetDescription()
    {
        return NameItem + "\n" +
               _ability_description;

    }
}
