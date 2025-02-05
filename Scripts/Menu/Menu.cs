using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Menu : MonoBehaviour 
{

    [SerializeField] private GameObject _choice_class;
    [SerializeField] private GameObject _main_menu;
    [SerializeField] private SaveCharacteristic _save;
    [SerializeField] private ListSaves _list_saves;
    private bool _has_saved;
    private int _amount_save;
    public void StartGame()
    {
        _amount_save = 1;
        _has_saved = !File.Exists(path: Application.persistentDataPath + "/player_saves/player1.a")
            || !File.Exists(path: Application.persistentDataPath + "/characteristics_saves/characteristic1.a");

        if (_has_saved)
        {
            _choice_class.SetActive(true);
            _main_menu.SetActive(false);
        }   
        else
        {
            _list_saves.gameObject.SetActive(true);
            _main_menu.SetActive(false);
            if(_list_saves.Content.childCount == 0)
            {
                ListSavesData[] list_save_data = BinarySavingSystem.LoadLists();
                CharacteristicsData characteristics_data;
                PlayerData player_data;
                for (int i = 0; i < list_save_data.Length; i++)
                {
                    _amount_save++;
                    characteristics_data = BinarySavingSystem.LoadCharacteristic(list_save_data[i].AmountSave);
                    player_data = BinarySavingSystem.LoadPlayer(list_save_data[i].AmountSave);
                    Instantiate(_list_saves.PrefabSave, _list_saves.Content).GetComponent<PanelInfoSave>().Initialization(characteristics_data.NameClass, player_data.Level, list_save_data[i].AmountSave, _save);
                    _list_saves.Content.sizeDelta = new(_list_saves.Content.sizeDelta.x, _list_saves.Content.sizeDelta.y + 100);
                }
            }
        }
       
    }
    public void CreateNewGame()
    {
        if(File.Exists(path: Application.persistentDataPath + "/list.a"))
            File.Delete(Application.persistentDataPath + "/list.a");
        _list_saves.gameObject.SetActive(false);
        _choice_class.SetActive(true);
        _main_menu.SetActive(false);
    }
    public void ApplicationChoiceClass() 
    {
        BinarySavingSystem.SaveListsAmountChoice(_amount_save);
        _save.SaveCharacteristics(_amount_save);
        SceneManager.LoadScene("Game");
    }
    public void ExitGame() => Application.Quit();
    public void ExitStartGame()
    {
        _choice_class.SetActive(false);
        if(_has_saved)
            _main_menu.SetActive(true);
        else
        {
            _list_saves.gameObject.SetActive(true);
        }
    }
    public void ExitListSaves()
    {
        _list_saves.gameObject.SetActive(false);
        _main_menu.SetActive(true);
    }
}