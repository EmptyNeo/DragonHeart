using UnityEngine;

public abstract class Equipped : Attacked
{
    public abstract void Action(Transform equipment_object, PlayerAttack _player_attack, PlayerEquipmentHandler player_equipment_handler);
    public abstract int GetIndex();
}
public abstract class Near : Equipped
{
    [SerializeField] private Color _color;
    public Color Color => _color;
    public override int GetIndex()
    {
        return 0;
    }
    public override void Action(Transform equipment_object, PlayerAttack _player_attack, PlayerEquipmentHandler player_equipment_handler) 
    {
        transform.parent = equipment_object;
        player_equipment_handler.TraceWeapon.color = _color;
        if (this is ISpecialAttack special_attack)
        {
            _player_attack.SpecialAttack = special_attack;
        }
    }
}
public abstract class Distance : Equipped
{
    public override int GetIndex()
    {
        return 1;
    }
    public override void Action(Transform equipment_object, PlayerAttack _player_attack, PlayerEquipmentHandler player_equipment_handler)
    {
        transform.parent = equipment_object;
    }
}
public abstract class Magic : Equipped
{
    public MagicAbility MagicAbility;
    public override int GetIndex()
    {
        return 2;
    }

    public override void Action(Transform equipment_object, PlayerAttack _player_attack, PlayerEquipmentHandler player_equipment_handler)
    {
        transform.parent = equipment_object;
        if (player_equipment_handler.SlotMagicAbility.Item != null && player_equipment_handler.SlotMagicAbility.Item is MagicAbilityItem item)
            MagicAbility = item.MagicAbility;
    }
}
