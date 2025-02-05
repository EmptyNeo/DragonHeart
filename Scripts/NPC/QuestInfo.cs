using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestInfo : MonoBehaviour
{
    [SerializeField] private Type_Quests _type;
    [SerializeField] private int _amount;
    [SerializeField] private int _max_amount;
    [SerializeField] private GameObject _item;
    [SerializeField] private TextMeshProUGUI _description_for_player;
    [SerializeField] private TextMeshProUGUI _quest_name;
    [SerializeField] private Image _item_reward;
    [SerializeField] private TextMeshProUGUI _need_item_amount;
    [SerializeField] private TextMeshProUGUI _amount_reward;
    [SerializeField] private TextMeshProUGUI _amount_xp;

    private string _name_reward;
    public Type_Quests Type =>  _type; 

    public int Amount => _amount; 
    public int MaxAmount => _max_amount; 
    public GameObject Item => _item;
    public TextMeshProUGUI DescriptionForPlayer => _description_for_player;
    public TextMeshProUGUI AmountReward => _amount_reward;
    public TextMeshProUGUI AmountXp => _amount_xp;
    public string NameReward => _name_reward;
    
    public void Initialization(Type_Quests type, int max_amount, GameObject item, string name_reward)
    {
        _type = type;
        _max_amount = max_amount;
        _item = item;
        _name_reward = name_reward;
    }
    public void Initialization(string quest_name, string description, string item_reward_names, string need_item, int amount, int max_amount, 
                               string amount_xp, string amount_reward, int type)
    {
        _quest_name.text = quest_name;
        _description_for_player.text = description;
        ItemScriptableObject item_reward = Resources.Load<ItemScriptableObject>($"ItemObject/{item_reward_names}");
        ItemScriptableObject item = Resources.Load<ItemScriptableObject>($"ItemObject/{need_item}");
        _name_reward = item_reward_names;
        _item = item.ItemPrefab;
        _item_reward.sprite = item_reward.Icon;
        _amount = amount;
        _max_amount = max_amount;
        _need_item_amount.text = _amount + "/" + _max_amount;
        _amount_xp.text = amount_xp;
        _amount_reward.text = amount_reward;
        _type = (Type_Quests)type;
    }
    public void ViewInfo(string description_for_player, string quest_name, Sprite sprite, string quest_item_amount, string amount_reward, string amount_xp)
    {
        _description_for_player.text = description_for_player;
        _quest_name.text = quest_name;
        _item_reward.sprite = sprite;
        _need_item_amount.text = quest_item_amount;
        _amount_reward.text = amount_reward;
        _amount_xp.text = amount_xp;
    }
    public bool NeedAmountUpdate(int i = 1)
    {
        if (_amount + i < _max_amount) 
        {
            _amount+=i;
            _need_item_amount.text = _amount + "/" + _max_amount;
            return false;
        }
        else 
        {
            _amount = _max_amount;
            _need_item_amount.text = _amount + "/" + _max_amount;
            _description_for_player.text = "Выполнено";
            return true;
        }

    }
}
