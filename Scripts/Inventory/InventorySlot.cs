
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum TypeSlot { Default, Equipment }
public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject Item; //определение какой предмет лежит в слоте
    public int Amount; //его количество, лежащее в слоте
    public bool isEmpty = true; //проверка пустой ли слот
    public Image iconGO; //иконка предмета лежащего в слоте
    public TextMeshProUGUI itemAmountText; //текст количества предметов
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
        //смена спрайта иконки
        iconGO.color = new Color(1, 1, 1, 1);
        iconGO.sprite = icon;
    }
}
