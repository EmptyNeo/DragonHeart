using UnityEngine;

[CreateAssetMenu(fileName = "Default Item", menuName = "Inventory/Default Item")]
public class DefaultItem : ItemScriptableObject
{
    [SerializeField] private string _description;
    public override string GetDescription()
    {
        return NameItem + "\n" +
               _description;
    }
}
