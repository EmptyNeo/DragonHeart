
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum TypeSlot { Default, Equipment }
public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject Item; //����������� ����� ������� ����� � �����
    public int Amount; //��� ����������, ������� � �����
    public bool isEmpty = true; //�������� ������ �� ����
    public Image iconGO; //������ �������� �������� � �����
    public TextMeshProUGUI itemAmountText; //����� ���������� ���������
    public TypeSlot slotType;
    [SerializeField] private Sprite _default;
    [SerializeField] private Sprite _sprite_glow;

    public Sprite Default { get => _default; }

    public void SetValueSlot(ItemScriptableObject item, int amount, bool is_empty, Sprite icon)
    {
        Item = item;
        Amount = amount;
        isEmpty = is_empty;
        iconGO.color = new Color(1, 1, 1, 1);
        iconGO.sprite = icon;

    }
    public void SetIcon(Sprite icon)
    {
        //����� ������� ������
        iconGO.color = new Color(1, 1, 1, 1);
        iconGO.sprite = icon;
    }
}
