using UnityEngine;

[CreateAssetMenu(fileName = "Main Item", menuName = "Inventory/New Main Item")]
public class MainItem : ItemScriptableObject
{
    [SerializeField] private float _change_characteristics;
    public float ChangeCharacteristics => _change_characteristics;

    public override string GetDescription()
    {
        return NameItem;
    }
}
