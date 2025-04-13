using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// IPointerDownHandler - Следит за нажатиями мышки по объекту на котором висит этот скрипт
/// IPointerUpHandler - Следит за отпусканием мышки по объекту на котором висит этот скрипт
/// IDragHandler - Следит за тем не водим ли мы нажатую мышку по объекту 
/// IPointerEnterHandler - Следит за тем, не водим ли мы мышку по объекту
/// IPointerExitHandler - Следит за тем, не вышли ли мы за границы объекта
public class DragAndDropItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler,
    IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject UIPanel;
    [SerializeField] protected InfoItem _info_item;
    [SerializeField] private Sprite _default_sprite;
    [SerializeField] private Sprite _change_sprite;
    [SerializeField] private Transform _inventory_panel;
    protected Transform _quick_slot_panel;
    protected InventorySlot _oldSlot;
    protected Transform _player;
    protected bool _dont_is_dragging;
    protected bool _on_pointer_enter;

    public GameObject UI_PANEL
    {
        get => UIPanel;
    }

    public InventorySlot OldSlot
    {
        get => _oldSlot;
    }

    public Transform TPlayer => _player;

    private void Start()
    {
        _player = Player.Transform;
        _inventory_panel = transform.parent.parent;
        _quick_slot_panel = QuickSlotsInventory.Transform;
        if (_oldSlot == null)
            _oldSlot = GetComponentInParent<InventorySlot>();
    }

    public virtual void Update()
    {
        if (_on_pointer_enter == true && _oldSlot.Item != null)
        {
            for (int i = 0; i < _quick_slot_panel.childCount; i++)
            {
                if (i + 1 < 10 && Input.GetKeyDown((i + 1).ToString()))
                {
                    ExchangeSlotData(_quick_slot_panel.GetChild(i).GetComponent<InventorySlot>());
                    _info_item.Panel.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_oldSlot.isEmpty || Input.GetMouseButton(1))
        {
            _dont_is_dragging = true;
            return;
        }

        Vector2 mousePositionPixels = Input.mousePosition;
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionPixels);
        transform.position = mousePositionWorld;

        if (eventData.pointerCurrentRaycast.gameObject == UIPanel ||
            eventData.pointerCurrentRaycast.gameObject == _quick_slot_panel.gameObject ||
            eventData.pointerCurrentRaycast.gameObject == _inventory_panel)
            gameObject.transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _on_pointer_enter = true;
        if (_oldSlot.slotType == TypeSlot.Default)
            _oldSlot.GetComponent<Image>().sprite = _change_sprite;
        if (_oldSlot.Item != null)
        {
            _info_item.Panel.gameObject.SetActive(true);
            _info_item.ShowItemInfo(_oldSlot.Item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_oldSlot.slotType == TypeSlot.Default)
            _oldSlot.GetComponent<Image>().sprite = _default_sprite;

        _info_item.Panel.gameObject.SetActive(false);
        _on_pointer_enter = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_oldSlot.isEmpty || Input.GetMouseButton(1))
            return;
        GetComponentInChildren<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
        GetComponentInChildren<Image>().raycastTarget = false;
        transform.SetParent(transform.parent.parent);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if (_oldSlot.isEmpty || _dont_is_dragging)
        {
            _dont_is_dragging = false;
            return;
        }

        // Делаем картинку опять не прозрачной
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
        // И чтобы мышка опять могла ее засечь
        GetComponentInChildren<Image>().raycastTarget = true;
        GetComponentInChildren<RectTransform>().localScale = new Vector2(.9f, .9f);
        transform.SetParent(_oldSlot.transform);
        transform.position = _oldSlot.transform.position;
        //Если мышка отпущена над объектом по имени UIPanel, то...
        if (eventData.pointerCurrentRaycast.gameObject == UIPanel)
        {
            Item item = Instantiate(_oldSlot.Item.ItemPrefab).GetComponent<Item>();
            item.ItemObject = _oldSlot.Item;
            item.Initialization(_oldSlot.Amount);
            item.transform.position = _player.position + Vector3.up + _player.forward;
            NullifySlotData();
        }
        else
        {
            InventorySlot inventory_slot = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent
                .GetComponent<InventorySlot>();
            switch (inventory_slot.slotType)
            {
                case TypeSlot.Default:
                    ExchangeSlotData(inventory_slot);
                    break;
                case TypeSlot.Equipment:
                    _player.GetComponent<PlayerEquipmentHandler>().EquipmentByType(_oldSlot);
                    break;
            }
        }
    }

    public void NullifySlotData()
    {
        _oldSlot.Item = null;
        _oldSlot.Amount = 0;
        _oldSlot.isEmpty = true;
        _oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        _oldSlot.iconGO.GetComponent<Image>().sprite = null;
        _oldSlot.itemAmountText.text = "";
    }

    public void ExchangeSlotData(InventorySlot newSlot)
    {
        ItemScriptableObject item = newSlot.Item;
        int amount = newSlot.Amount;
        bool isEmpty = newSlot.isEmpty;
        newSlot.Item = _oldSlot.Item;

        newSlot.Amount = _oldSlot.Amount;
        transform.parent.GetComponent<Image>().sprite = _default_sprite;
        if (_oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(newSlot.Item.Icon);
            if (_oldSlot.Amount != 1) newSlot.itemAmountText.text = _oldSlot.Amount.ToString();
        }
        else
        {
            newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.iconGO.GetComponent<Image>().sprite = null;
            newSlot.itemAmountText.text = "";
        }

        newSlot.isEmpty = _oldSlot.isEmpty;
        _oldSlot.Item = item;
        _oldSlot.Amount = amount;
        if (isEmpty == false)
        {
            _oldSlot.SetIcon(_oldSlot.Item.Icon);
            if (_oldSlot.Amount != 1) _oldSlot.itemAmountText.text = amount.ToString();
        }
        else
        {
            _oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            _oldSlot.iconGO.GetComponent<Image>().sprite = null;
            _oldSlot.itemAmountText.text = "";
        }

        _oldSlot.isEmpty = isEmpty;
    }

    public void ExchangeSlotEquipmentArmor(Characteristics characteristics, InventorySlot newSlot)
    {
        if (newSlot.Item != null)
        {
            int physical_protection =
                characteristics.TotalPhysicalProtection - newSlot.Item.ArmorItem.PhysicalProtection;
            int magic_protection = characteristics.TotalMagicProtection - newSlot.Item.ArmorItem.MagicProtection;
            characteristics.Initialization(physical_protection, magic_protection);
        }

        ItemScriptableObject item = newSlot.Item;
        int amount = newSlot.Amount;
        bool isEmpty = newSlot.isEmpty;

        newSlot.Item = _oldSlot.Item;
        newSlot.Amount = _oldSlot.Amount;
        transform.parent.GetComponent<Image>().sprite = _default_sprite;

        if (_oldSlot.isEmpty == false)
        {
            newSlot.SetIcon(newSlot.Item.Icon);
            if (_oldSlot.Amount != 1) newSlot.itemAmountText.text = _oldSlot.Amount.ToString();
        }
        else
        {
            newSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            newSlot.iconGO.GetComponent<Image>().sprite = null;
            newSlot.itemAmountText.text = "";
        }

        newSlot.isEmpty = _oldSlot.isEmpty;

        _oldSlot.Item = item;
        _oldSlot.Amount = amount;
        if (isEmpty == false)
        {
            _oldSlot.SetIcon(_oldSlot.Item.Icon);
            if (_oldSlot.Amount != 1) _oldSlot.itemAmountText.text = amount.ToString();
        }
        else
        {
            _oldSlot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            _oldSlot.iconGO.GetComponent<Image>().sprite = null;
            _oldSlot.itemAmountText.text = "";
        }

        _oldSlot.isEmpty = isEmpty;
    }
}