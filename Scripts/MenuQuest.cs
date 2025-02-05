using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuQuest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _amount_reward;
    [SerializeField] private TextMeshProUGUI _name_item;
    [SerializeField] private TextMeshProUGUI _xp;
    [SerializeField] private ButtonQuestApplication _button_quest_application;
    [SerializeField] private GameObject _application_quest;
    [SerializeField] private GameObject _accept_quest;
    [SerializeField] private GameObject _not_quest;
    [SerializeField] private GameObject _prefab_info;
    [SerializeField] private Transform _content;
    [SerializeField] private GameObject _not_quest_to_npc;

    public TextMeshProUGUI Name { get => _name; }
    public TextMeshProUGUI Description { get => _description;  }
    public Image Icon { get => _icon; set => _icon = value; }
    public TextMeshProUGUI AmountReward { get => _amount_reward; }
    public TextMeshProUGUI NameItem { get => _name_item; }
    public TextMeshProUGUI Xp { get => _xp; }
    public ButtonQuestApplication ButtonQuestApplication { get => _button_quest_application;  }
    public GameObject ApplicationQuest { get => _application_quest;}
    public GameObject AcceptQuest { get => _accept_quest;  }
    public GameObject NotQuest { get => _not_quest;  }
    public GameObject PrefabInfo { get => _prefab_info;  }
    public Transform Content { get => _content;  }
    public GameObject NotQuestToNpc { get => _not_quest_to_npc;  }
}
