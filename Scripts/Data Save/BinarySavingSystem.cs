using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class BinarySavingSystem
{
    public static void SavePlayer(Characteristics characteristics, LevelUpgrade level_upgrade, Inventory inventory, Wallet wallet, int amount_save)
    {
        BinaryFormatter formatter = new();

        if(File.Exists(Application.persistentDataPath + "/player_saves") == false)
            Directory.CreateDirectory(Application.persistentDataPath + "/player_saves");

        string path = Application.persistentDataPath + "/player_saves/player" + amount_save + ".a";
        FileStream stream = new(path, FileMode.Create);
        PlayerData data = new(characteristics, level_upgrade, inventory, wallet);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveCharacteristics(Class _class, int index, int amount_save)
    {
        BinaryFormatter formatter = new();

        if(File.Exists(Application.persistentDataPath + "/characteristics_saves") == false)
            Directory.CreateDirectory(Application.persistentDataPath + "/characteristics_saves");

        string path = Application.persistentDataPath + "/characteristics_saves/characteristic" + amount_save + ".a";
        FileStream stream = new(path, FileMode.Create);
        CharacteristicsData data = new(_class, index, amount_save);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveListsAmountChoice(int amount_save)
    {
        BinaryFormatter formatter = new();

        if(File.Exists(Application.persistentDataPath + "/lists") == false)
            Directory.CreateDirectory(Application.persistentDataPath + "/lists");

        string path = Application.persistentDataPath + "/lists/list" + amount_save + ".a";
        FileStream stream = new(path, FileMode.Create);
        ListSavesData list_save_data = new(amount_save);
        formatter.Serialize(stream, list_save_data);
        stream.Close();
    }
    public static void SaveListAmountChoice(int amount_save)
    {
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/list.a";
        FileStream stream = new(path, FileMode.Create);
        ListSavesData list_save_data = new(amount_save);
        formatter.Serialize(stream, list_save_data);
        stream.Close();
    }
    public static void SaveItems(Item[] items, Coin[] coins, int amount_save)
    {
        BinaryFormatter formatter = new();
        if(File.Exists(Application.persistentDataPath + "/save_items") == false)
            Directory.CreateDirectory(Application.persistentDataPath + "/save_items");
        
        string path = Application.persistentDataPath + "/save_items/save_item" + amount_save + ".a";
        FileStream stream = new(path, FileMode.Create);
        ItemsData items_data = new(items, coins);
        formatter.Serialize(stream, items_data);
        stream.Close();
    }
    public static void SaveQuestsData(QuestInfo[] quest_info, NPC_QUEST[] npc, int amount_save)
    {
        BinaryFormatter formatter = new();
        if(File.Exists(Application.persistentDataPath + "/save_quests_data") == false)
            Directory.CreateDirectory(Application.persistentDataPath + "/save_quests_data");
        
        string path = Application.persistentDataPath + "/save_quests_data/quest_data" + amount_save + ".a";
        FileStream stream = new(path, FileMode.Create);
        QuestsData quests_data = new(npc, quest_info);
        formatter.Serialize(stream, quests_data);
        stream.Close();
    }
    public static PlayerData LoadPlayer(int amount_save)
    {
        string path = Application.persistentDataPath + "/player_saves/player" + amount_save + ".a";
        Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    public static CharacteristicsData LoadCharacteristic(int amount_save)
    {
        string path = Application.persistentDataPath + "/characteristics_saves/characteristic" + amount_save + ".a";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);
            CharacteristicsData data = formatter.Deserialize(stream) as CharacteristicsData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    public static ListSavesData LoadListAmountChoice(int amount_save)
    {
        string path = Application.persistentDataPath + "/lists/list" + amount_save + ".a";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);
            ListSavesData data = formatter.Deserialize(stream) as ListSavesData;
            stream.Close();
            return data;
        }
        else 
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    public static ListSavesData[] LoadLists()
    {
        DirectoryInfo directory_info = new DirectoryInfo(Application.persistentDataPath + "/lists");
        FileInfo[] files = directory_info.GetFiles("*.a");
        string path;
        BinaryFormatter formatter = new();
        FileStream stream;
        ListSavesData[] data = new ListSavesData[files.Length];
        int amount_save;
        for(int i = 0; i < files.Length; i++)
        {
            amount_save = i + 1;
            path = Application.persistentDataPath + "/lists/list" + amount_save + ".a";
            stream = new(path, FileMode.Open);
            data[i] = formatter.Deserialize(stream) as ListSavesData;
            stream.Close();
        }
        return data;
    }
    public static ListSavesData LoadList()
    {
        string path = Application.persistentDataPath + "/list.a";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);
            ListSavesData data = formatter.Deserialize(stream) as ListSavesData;
            stream.Close();
            return data;
        }
        else 
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    public static ItemsData LoadItems(int amount_save)
    {
        string path = Application.persistentDataPath + "/save_items/save_item" + amount_save + ".a";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);
            ItemsData items_data = formatter.Deserialize(stream) as ItemsData;
            stream.Close();
            return items_data;
        }
        else 
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    public static QuestsData LoadQuestsData(int amount_save)
    {
        string path = Application.persistentDataPath + "/save_quests_data/quest_data" + amount_save + ".a";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);
            QuestsData data = formatter.Deserialize(stream) as QuestsData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
