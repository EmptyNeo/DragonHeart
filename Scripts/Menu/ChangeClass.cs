using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeClass : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _characteristic;
    [SerializeField] private TextMeshProUGUI _name_class;
    [SerializeField] private TextMeshProUGUI _health_class;
    [SerializeField] private TextMeshProUGUI _strength_class;
    [SerializeField] private TextMeshProUGUI _mana_class;
    [SerializeField] private TextMeshProUGUI _endurance_class;
    [SerializeField] private TextMeshProUGUI _craft_class;
    [SerializeField] private Image _boots_left;
    [SerializeField] private Image _boots_right;
    [SerializeField] private Image _breastplate;
    [SerializeField] private Image _helmet;
    [SerializeField] private Sprite _default_boots;
    [SerializeField] private Sprite _default_breastplate;
    [SerializeField] private Sprite _default_helmet;
    [SerializeField] private Class _class;


    private int _index;
    public int Index 
    {
        get => _index;
        private set 
        { 
            _index = value;
            if (value < 0) 
                _index = 5; 
            else if (value > 5) 
                _index = 0;
        } 
    }

    private void Start()
    {
       ChangeNameCharacteristic();
    }

    public void ChangeClassRight()
    {
        Index++;
    }
    public void ChangeClassLeft()
    {
        Index--;
    }

    public void ChangeClassRightOff()
    {
        _animator.SetBool("changeRight", false);
    }

    
    public void ChangeClassLeftOff()
    {
        _animator.SetBool("changeLeft", false);
    }

    public void ButtonRight() => _animator.SetBool("changeRight", true); 

    public void ButtonLeft() => _animator.SetBool("changeLeft", true); 
 
    public void ChangeNameCharacteristic()
    {

        _name_class.text = _class.Name_Class[Index];
        if(_class.ViewBoots[_index] != null)
        {
            _boots_left.sprite = _class.ViewBoots[_index];
            _boots_right.sprite = _class.ViewBoots[_index];
            _breastplate.sprite = _class.ViewBreastplate[_index];
            _helmet.sprite = _class.ViewHelmet[_index];
        }
        else
        {
            _boots_left.sprite = _default_boots;
            _boots_right.sprite = _default_boots;
            _breastplate.sprite = _default_breastplate;
            _helmet.sprite = _default_helmet;
        }
        _health_class.text = " Здоровье - " + _class.Health[Index];
        _strength_class.text = " Сила - " + _class.Strength[Index];
        _mana_class.text = " Мана - " + _class.Mana[Index];
        _endurance_class.text = " Выносливость - " + _class.Endurance[Index];
        _craft_class.text = " Ремесло - " + _class.Craft[Index]; 
    }
    public void TurnPanelCharacteristic(bool enable)
    {
        _characteristic.SetActive(enable);
    }
}
