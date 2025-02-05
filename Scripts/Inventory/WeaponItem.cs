using System;
using UnityEngine;
public enum TypeNecessaryCharacteristic { Strength, Mana, Endurance }
[CreateAssetMenu(fileName = "Weapon Item", menuName = "Inventory/New Weapon Item")]
public class WeaponItem : ItemScriptableObject
{
    public float damage;
    public float delay;
    [SerializeField] private TypeNecessaryCharacteristic _type_characteristic;
    [SerializeField] private int _necessary_characteristic;

    public float Damage => damage;
    public float Delay => delay;
    public int NecessaryCharacteristic => _necessary_characteristic;
    public int IndexNecessaryCharacteristic => Convert.ToInt32(_type_characteristic);
    public TypeNecessaryCharacteristic TypeCharacteristic => _type_characteristic;
    private string[] NameCharacteristic = { "Сила", "Интелект", "Выносливость" };
    public override string GetDescription()
    {
        float speed_attack = 1 / delay;
        return  NameItem + "\n" +
                "Когда надето: \n" +
                "Скорость атаки: " + speed_attack + "\n" +
                "Урон: " + damage + "\n" +
                "Необходимо: " + NameCharacteristic[IndexNecessaryCharacteristic] + " " + NecessaryCharacteristic;

    }
}
