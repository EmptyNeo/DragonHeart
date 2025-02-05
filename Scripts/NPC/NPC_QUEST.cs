using System;
using UnityEngine;
using UnityEngine.UI;

public class NPC_QUEST : NPC
{
    [SerializeField] private QUESTS[] _quest;
    [SerializeField] private int _index_quest;
    private bool _apply_quest;
    public bool apply_quest { get { return _apply_quest; } set { _apply_quest = value; } }
    public QUESTS[] quest => _quest;
    public int index_quest { get { return _index_quest; } set { _index_quest = value; } }

    public void AddQuest(MenuQuest menu_quest)
    {
       if(_index_quest < _quest.Length)
        {
            menu_quest.Name.text = _quest[_index_quest].QuestName;
            menu_quest.Description.text = _quest[_index_quest].QuestDescription;
            menu_quest.Icon.sprite = _quest[_index_quest].Reward.GetComponent<Item>().ItemObject.Icon;
            menu_quest.AmountReward.text = _quest[_index_quest].AmountReward.ToString();
            menu_quest.NameItem.text = _quest[_index_quest].Reward.GetComponent<Item>().ItemObject.NameItem;
            menu_quest.Xp.text = "Опыт: " + _quest[_index_quest].Xp.ToString();
        }
        else
        {
            menu_quest.NotQuestToNpc.SetActive(true);
            menu_quest.Description.text = "На этом мои просьбы закончились. Спасибо тебе, добрый путник!";
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInteraction player))
        {
            MenuQuest menu_quest = player.Quest;
            AddQuest(menu_quest);
            if (menu_quest.ButtonQuestApplication.Npc == null)
            {
                menu_quest.ButtonQuestApplication.Npc = GetComponent<NPC_QUEST>();
            }
           
            if (_apply_quest == false)
            {
                menu_quest.ApplicationQuest.SetActive(false);
                menu_quest.AcceptQuest.SetActive(true);
            }
            else
            {
                menu_quest.ApplicationQuest.SetActive(true);
                menu_quest.AcceptQuest.SetActive(false);
            }

        }
    }
    public void CheckItemInventory(Inventory inventory, ItemScriptableObject item_quest, int amount, ref int x)
    {

        foreach(InventorySlot slot in inventory.Slots)
        {
            if(item_quest == slot.Item)
                x+=slot.Amount;
        }
    }
    public bool CheckAmount(ref int x, int amount)
    {
        if (x < amount)
            return false;
        else if (x >= amount)
            return true;
        return false;
    }
    public void RemoveSlotItem(Inventory inventory,ItemScriptableObject item_quest)
    {
        foreach (InventorySlot slot in inventory.Slots)
        {
            if (slot.Item == item_quest)
            {
                if(slot.Amount > 1)
                {
                    slot.Amount--;
                    if (slot.Amount != 1) slot.itemAmountText.text = slot.Amount.ToString();
                    else slot.itemAmountText.text = " ";
                    break;
                }
                else
                {
                    slot.Item = null;
                    slot.isEmpty = true;
                    slot.Amount = 0;
                    slot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    slot.iconGO.GetComponent<Image>().sprite = null;
                    slot.itemAmountText.text = " ";
                }
                    
            }
        }    
    }
    [Serializable]
    public class QUESTS
    {
        [Header("Квест")]
        [SerializeField] private GameObject _quest_item;
        [SerializeField] private int _quest_item_amount;
        [SerializeField] private GameObject _reward;
        [SerializeField] private int _amount_reward;
        [SerializeField] private float _xp;
        [Header("Информация Квеста")]
        [SerializeField] private string _quest_name;

        [SerializeField] private string _quest_description;
        [SerializeField] private string _description_for_player;

        [Header("Тип Квеста")]
        [SerializeField] private Type_Quests _type;
        public int QuestItemAmount => _quest_item_amount;
        public GameObject QuestItem => _quest_item; 
        public GameObject Reward => _reward;
        public int AmountReward => _amount_reward; 
        public float Xp => _xp;

        public string QuestName => _quest_name;
        public string QuestDescription => _quest_description;
        public string DescriptionForPlayer => _description_for_player;
        public Type_Quests type => _type;

      
    }
}
public enum Type_Quests
{
    KilledEnemy, //Убийство мобов
    CollectionsItem, //Сбор предметов
};
