using System;
[Serializable]
public class QuestsData 
{
    private int[] _index_quest_npc;
    private bool[] _apply_quest;
    private float[] _x;
    private float[] _y;
    private string[] _quest_names;
    private string[] _description;
    private string[] _item_reward_names;
    private string[] _item_need_names;
    private int[] _need_amount;
    private int[] _need_max_amount;
    private string[] _amount_xp;
    private string[] _amount_rewards;
    private int[] _type_quest;
    
    public int[] IndexQuestNpc => _index_quest_npc;
    public float[] X => _x;
    public float[] Y => _y;
    public bool[] ApplyQuest => _apply_quest;
    public string[] QuestNames => _quest_names;
    public string[] Description => _description;
    public string[] ItemRewardNames => _item_reward_names;
    public string[] ItemNeedNames => _item_need_names;
    public int[] NeedAmount => _need_amount;
    public int[] NeedMaxAmount => _need_max_amount;
    public string[] AmountXp => _amount_xp;
    public string[] AmountRewards => _amount_rewards;
    public int[] TypeQuest => _type_quest;

    public QuestsData(NPC_QUEST[] npc, QuestInfo[] quest_info)
    {
        _index_quest_npc = new int[npc.Length];
        _x = new float[npc.Length];
        _y = new float[npc.Length];
        _apply_quest = new bool[npc.Length];

        _quest_names = new string[quest_info.Length];
        _description = new string[quest_info.Length];
        _item_reward_names = new string[quest_info.Length];
        _item_need_names = new string[quest_info.Length];
        _need_amount = new int[quest_info.Length];
        _need_max_amount = new int[quest_info.Length];
        _amount_xp = new string[quest_info.Length];
        _amount_rewards = new string[quest_info.Length];
        _type_quest = new int[quest_info.Length];

        for(int i = 0; i < npc.Length; i++)
        {
            _index_quest_npc[i] = npc[i].index_quest;
            _x[i] = npc[i].transform.position.x;
            _y[i] = npc[i].transform.position.y;
            _apply_quest[i] = npc[i].apply_quest;
        }

        for(int i = 0; i < quest_info.Length; i++)
        {
            _quest_names[i] = quest_info[i].name;
            _description[i] = quest_info[i].DescriptionForPlayer.text;
            _item_reward_names[i] =  quest_info[i].NameReward;
            if(quest_info[i].Item.TryGetComponent(out Item item))
                _item_need_names[i] = item.name;
            _need_amount[i] = quest_info[i].Amount;
            _need_max_amount[i] = quest_info[i].MaxAmount;
            _amount_xp[i] = quest_info[i].AmountXp.text;
            _amount_rewards[i] = quest_info[i].AmountReward.text;
            _type_quest[i] = Convert.ToInt32(quest_info[i].Type);
        }

    }

}