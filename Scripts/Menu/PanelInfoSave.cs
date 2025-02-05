using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PanelInfoSave : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _class;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private int _amount_save;
    private SaveCharacteristic _save;
    
    public void Initialization(string name_class, int level, int amount_save, SaveCharacteristic save)
    {
        _class.text = "Класс: " + name_class;
        _level.text = "Уровень: " + level.ToString();
        _amount_save = amount_save;
        _save = save;
    }
    public void LoadGame()
    {
        BinarySavingSystem.SaveListAmountChoice(_amount_save);
        SceneManager.LoadScene("Game");
    }
    
}