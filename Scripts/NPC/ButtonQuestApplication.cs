using UnityEngine;

public class ButtonQuestApplication : MonoBehaviour
{
    
    [SerializeField] private RectTransform _content;
    
    [SerializeField] private float _total_height;

    [SerializeField] private MenuQuest _menu_quest;
    public int ChildCount;
    public float TotalHeight => _total_height;
    public RectTransform Content => _content; 
    public NPC_QUEST Npc;
    public void ApplicationQuest()
    {
        int x = 0;
        switch (Npc.quest[Npc.index_quest].type)
        {
            case Type_Quests.KilledEnemy:
                for(int i = 0; i < _content.childCount; i++)
                    if (Npc.quest[Npc.index_quest].QuestName == _content.GetChild(i).name)
                        if (Npc.quest[Npc.index_quest].QuestItemAmount != _content.GetChild(i).GetComponent<QuestInfo>().Amount)
                            return;
                break;
            case Type_Quests.CollectionsItem:
                
               Npc.CheckItemInventory(Inventory.inventory, Npc.quest[Npc.index_quest].QuestItem.GetComponent<Item>().ItemObject, Npc.quest[Npc.index_quest].QuestItemAmount, ref x);
                if (Npc.CheckAmount(ref x, Npc.quest[Npc.index_quest].QuestItemAmount) == false)
                    return;
                for (int i = 0; i < Npc.quest[Npc.index_quest].QuestItemAmount; i++)
                {
                    Npc.RemoveSlotItem(Inventory.inventory, Npc.quest[Npc.index_quest].QuestItem.GetComponent<Item>().ItemObject);
                }
                break;

        }
        DeleteQuestInList();

        for (int i = 0; i < Npc.quest[Npc.index_quest].AmountReward; i++)
        {
            Inventory.inventory.AddItem(Npc.quest[Npc.index_quest].Reward.GetComponent<Item>().ItemObject, 1);
        }
        LevelUpgrade.Instance.TakeXp(Npc.quest[Npc.index_quest].Xp);
        Npc.index_quest++;
        Npc.AddQuest(_menu_quest);

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        
        Npc.apply_quest = false;
         if(ChildCount == 0)
            _menu_quest.NotQuest.SetActive(true);
    }
    private void DeleteQuestInList()
    {
        foreach (Transform content in _content)
        {
            if (content.gameObject.name == Npc.quest[Npc.index_quest].QuestName)
            {
                Destroy(content.gameObject);
                ChildCount--;
                _content.sizeDelta = new Vector2(_content.sizeDelta.x, _content.sizeDelta.y - _total_height);
                break;
            }
        }
    }
    public void AcceptQuest()
    {
        if (Npc.index_quest < Npc.quest.Length)
        {
            _menu_quest.NotQuest.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            QuestInfo quest = Instantiate(_menu_quest.PrefabInfo, _menu_quest.Content).GetComponent<QuestInfo>();
            ChildCount++;
            quest.gameObject.name = Npc.quest[Npc.index_quest].QuestName;
            _content.sizeDelta = new Vector2(_content.sizeDelta.x, _content.sizeDelta.y + _total_height);

            quest.Initialization(Npc.quest[Npc.index_quest].type,
                                       Npc.quest[Npc.index_quest].QuestItemAmount,
                                       Npc.quest[Npc.index_quest].QuestItem, 
                                       Npc.quest[Npc.index_quest].Reward.name);

            quest.ViewInfo(Npc.quest[Npc.index_quest].DescriptionForPlayer,
                           Npc.quest[Npc.index_quest].QuestName,
                           Npc.quest[Npc.index_quest].Reward.GetComponent<Item>().ItemObject.Icon,
                           "0" + "/" + Npc.quest[Npc.index_quest].QuestItemAmount,
                           Npc.quest[Npc.index_quest].AmountReward.ToString(),
                           "Опыт: " + Npc.quest[Npc.index_quest].Xp.ToString());

            Npc.apply_quest = true; 
        }

    }
}
