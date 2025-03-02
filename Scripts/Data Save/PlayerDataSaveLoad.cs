using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataSaveLoad : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private LevelUpgrade _level_upgrade;
    [SerializeField] private PlayerEquipmentHandler _player_equipment_handler;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private int _amount_save;
    [SerializeField] private GameObject _prefab_quest;
    [SerializeField] private Transform _quests_content;
    [SerializeField] private MenuQuest _menu_quest;
    [SerializeField] private ButtonQuestApplication _button_quest_application;
    [SerializeField] private PlayerAttack _player_attack;
    private PlayerData _player_data;
    private CharacteristicsData _characteristics_data;
    
    public void Initialize()
    {
        _inventory.Initialization();
        LoadAmountSave();
        
         _player_data = BinarySavingSystem.LoadPlayer(_amount_save);
         if(_player_data == null)
        {
            StartGameLoaded();
        }
         else
         {
            LoadPlayer();
            LoadItems();
            InitializationCharacteristics();
            LoadEquipment();
            LoadQuestData();
         }
       

    }
    public void SavePlayer() 
    {
        BinarySavingSystem.SavePlayer(_characteristics, _level_upgrade, _inventory, _wallet, _amount_save);
        QuestInfo[] quests_info = new QuestInfo[_button_quest_application.Content.childCount];
        for(int i = 0; i < quests_info.Length; i++)
        {
            if (_button_quest_application.Content.GetChild(i).TryGetComponent(out QuestInfo quest_info))
                quests_info[i] = quest_info;
        }
        BinarySavingSystem.SaveQuestsData(quests_info, FindObjectsByType<NPC_QUEST>(FindObjectsSortMode.None), _amount_save);
        BinarySavingSystem.SaveItems(FindObjectsByType<Item>(FindObjectsSortMode.None), FindObjectsByType<Coin>(FindObjectsSortMode.None), _amount_save);
    }
    public void ExitMenu() => SceneManager.LoadScene("Menu");
  
    public void LoadPlayer()
    {
        LevelUpgrade.level = _player_data.Level;
        _player_data.Initialization(_level_upgrade);
        InitializationCharacteristics();
        transform.position = new Vector2(_player_data.X, _player_data.Y);
        
        _wallet.Initialization(_player_data.Balance);
        for (int i = 0; i < _inventory.Slots.Count; i++)
        {
            if (_player_data.ItemNames[i] != null)
            {
                ItemScriptableObject item = Resources.Load<ItemScriptableObject>($"ItemObject/{_player_data.ItemNames[i]}");
                int itemAmount = _player_data.ItemAmounts[i];
                _inventory.AddItemToSlot(item, itemAmount, i);
            }

        }
    }
    private void StartGameLoaded()
    {
        _characteristics_data = BinarySavingSystem.LoadCharacteristic(_amount_save);
        _characteristics_data.Initialization(_level_upgrade);
        InitializationCharacteristics();
        if (string.IsNullOrEmpty(_characteristics_data.WeaponName) == false)
        {
            ItemScriptableObject weapon = Resources.Load<ItemScriptableObject>($"ItemObject/{_characteristics_data.WeaponName}");
            ItemScriptableObject helmet = Resources.Load<ItemScriptableObject>($"ItemObject/{_characteristics_data.HelmetName}");
            ItemScriptableObject breastplate = Resources.Load<ItemScriptableObject>($"ItemObject/{_characteristics_data.BreastplateName}");
            ItemScriptableObject boots = Resources.Load<ItemScriptableObject>($"ItemObject/{_characteristics_data.BootsName}");

            _player_equipment_handler.SlotWeapon.SetValueSlot(weapon, 1, false, weapon.Icon);
            _player_equipment_handler.SlotHelmet.SetValueSlot(helmet, 1, false, helmet.Icon);
            _player_equipment_handler.SlotBreastplate.SetValueSlot(breastplate, 1, false, breastplate.Icon);
            _player_equipment_handler.SlotBoots.SetValueSlot(boots, 1, false, boots.Icon);

            _player_equipment_handler.StartGameEquipmentByType(weapon);
            _player_equipment_handler.StartGameEquipmentByType(helmet);
            _player_equipment_handler.StartGameEquipmentByType(breastplate);
            _player_equipment_handler.StartGameEquipmentByType(boots);

        }
        _characteristics.InitializationText(_player_attack.Weapon.Damage);
       
        SavePlayer();
    }
    private void InitializationCharacteristics()
    {
        float max_health = (_level_upgrade.Health * 150 / 100) + 10;
        float max_mana = _level_upgrade.Mana * 150 / 100;
        float max_endurance = (_level_upgrade.Endurance * 50 / 100) + 5;
        float speed = (_level_upgrade.Endurance * 5 / 100) + 5;
        float speed_boost = (_level_upgrade.Endurance * 1.5f / 100) + speed + 2.5f;

        _characteristics.Initialization(max_health, max_mana, max_endurance, speed, speed_boost);
        _characteristics.InitializationText();
         _level_upgrade.Initialization();
        _player_attack.Initialization(_player_attack.Delay, _level_upgrade.Strength / 3f);
    }
    private void LoadEquipment()
    {
        for(int i = 0; i < _inventory.Slots.Count; i++)
        {
            if(_inventory.Slots[i].Item != null)
            {
                if(_inventory.Slots[i].slotType == TypeSlot.Equipment)
                {
                    _player_equipment_handler.StartGameEquipmentByType(_inventory.Slots[i].Item);         
                }
            }
        }
        _characteristics.InitializationText(_player_attack.Weapon.Damage);
    }
    private void LoadItems()
    {
        
        ItemsData items_data = BinarySavingSystem.LoadItems(_amount_save);
        if(items_data != null)
        {
            Item[] items = FindObjectsByType<Item>(FindObjectsSortMode.None);
            Coin[] coins = FindObjectsByType<Coin>(FindObjectsSortMode.None);
            for(int i = 0; i < items.Length; i++)
                Destroy(items[i].gameObject);
            for(int i = 0; i < coins.Length; i++)
                Destroy(coins[i].gameObject);

            ItemScriptableObject item;
            Coin[] resources_coins = Resources.LoadAll<Coin>("Prefabs/Coins");
            for(int i = 0; i < items_data.ItemsName.Length; i++)
            {
                item = Resources.Load<ItemScriptableObject>($"ItemObject/{items_data.ItemsName[i]}");
                Instantiate(item.ItemPrefab, 
                            new Vector2(items_data.ItemsX[i], items_data.ItemsY[i]), 
                            Quaternion.identity).GetComponent<Quantity>().Initialization(items_data.ItemsAmount[i]);
            }
            for(int i = 0; i < items_data.CoinsId.Length; i++)
            {
                for(int j = 0; j < resources_coins.Length; j++)
                    if(items_data.CoinsId[i] == resources_coins[j].Id)
                        Instantiate(resources_coins[j].gameObject, 
                                    new Vector2(items_data.CoinsX[i], items_data.CoinsY[i]), 
                                    Quaternion.identity).GetComponent<Quantity>().Initialization(items_data.CoinsAmount[i]);
            }
        }
    }
    private void LoadAmountSave()
    {
        if(BinarySavingSystem.LoadList() != null)
            _amount_save = BinarySavingSystem.LoadList().AmountSave;
        else
            _amount_save = BinarySavingSystem.LoadListAmountChoice(BinarySavingSystem.LoadLists().Length).AmountSave;
    }
    private void LoadQuestData()
    {
        QuestsData quest_data = BinarySavingSystem.LoadQuestsData(_amount_save);
        if(quest_data != null)
        {
            RectTransform content = _button_quest_application.Content;

            for(int i = 0; i < quest_data.ItemRewardNames.Length; i++)
            {

                _menu_quest.NotQuest.SetActive(false);

                QuestInfo quest_info = Instantiate(_prefab_quest, _quests_content).GetComponent<QuestInfo>();
                quest_info.gameObject.name = quest_data.QuestNames[i];
                content.sizeDelta = new Vector2(content.sizeDelta.x, content.sizeDelta.y + _button_quest_application.TotalHeight);
                quest_info.Initialization(quest_data.QuestNames[i], quest_data.Description[i], quest_data.ItemRewardNames[i], quest_data.ItemNeedNames[i], quest_data.NeedAmount[i],
                                          quest_data.NeedMaxAmount[i], quest_data.AmountXp[i], quest_data.AmountRewards[i], quest_data.TypeQuest[i]);

            }
            _button_quest_application.ChildCount = quest_data.ItemRewardNames.Length;
            NPC_QUEST[] npc_quests = FindObjectsByType<NPC_QUEST>(FindObjectsSortMode.None);   
            for(int i = 0; i < quest_data.IndexQuestNpc.Length; i++)
            {

               npc_quests[i].index_quest = quest_data.IndexQuestNpc[i];
               npc_quests[i].apply_quest = quest_data.ApplyQuest[i];
               npc_quests[i].transform.position = new Vector2(quest_data.X[i], quest_data.Y[i]);
                
            }
        }
    }
}
