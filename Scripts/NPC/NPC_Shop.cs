using UnityEngine;

public class NPC_Shop : NPCAdditionalPanel
{
    [SerializeField] private Transform content;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private ItemScriptableObject[] items;
    [SerializeField] private GameObject _button;
    private PanelBuyItem _panel_buy_item;

    private void Start()
    {
        Name.text = "Продавец " + NameValue;
    }
    private void AddButton()
    {
        if (PanelAdditional.TryGetComponent(out PanelBuyItem item))
            _panel_buy_item = item;
        for (int i = 0; i < items.Length; i++)
        {
            ButtonBuyNPC button = Instantiate(_button, content).GetComponent<ButtonBuyNPC>();
            button.Item = items[i];
            button.Icon.sprite = items[i].Icon;
            button.PanelBuyItem = _panel_buy_item; 
        }
       
    }
    private void DeleteButton()
    {
        foreach(Transform item in content)
            if(item != null)
                Destroy(item.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player _))
        {
            AddButton();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player _))
        {
            DeleteButton();
            ViewAdditionalPanel(false);
        }
    }
}
