using UnityEngine;
using UnityEngine.UI;
public class Class : MonoBehaviour
{
    [SerializeField] private int[] _health;
    [SerializeField] private int[] _strength;
    [SerializeField] private int[] _mana;
    [SerializeField] private int[] _endurance;
    [SerializeField] private int[] _craft;
    [SerializeField] private GameObject[] _weapon_item;
    [SerializeField] private GameObject[] _boots_item;
    [SerializeField] private GameObject[] _breasplate_item;
    [SerializeField] private GameObject[] _helmet_item;
     
    [SerializeField] private string[] _name_class;
    [SerializeField] private Sprite[] _view_boots;
    [SerializeField] private Sprite[] _view_breastplate;
    [SerializeField] private Sprite[] _view_helmet;

   

    public int[] Health => _health; 
    public int[] Strength => _strength; 
    public int[] Mana => _mana; 
    public int[] Endurance => _endurance; 
    public int[] Craft => _craft;
    public string[] Name_Class => _name_class; 
    public Sprite[] ViewBoots => _view_boots;
    public Sprite[] ViewBreastplate => _view_breastplate;
    public Sprite[] ViewHelmet => _view_helmet;
    public WeaponItem[] WeaponItem 
    {
         get 
         {  
            WeaponItem[] new_weapon_item = new WeaponItem[_weapon_item.Length];
            for(int i = 0; i < _weapon_item.Length; i++)
            {
                if(_weapon_item[i] != null &&_weapon_item[i].TryGetComponent(out Item item))
                {
                    new_weapon_item[i] = item.ItemObject.WeaponItem;
                }
            }
            return new_weapon_item;
        }
    }
    public ArmorItem[] BootsItem
    {
        get
        {
            ArmorItem[] new_weapon_item = new ArmorItem[_boots_item.Length];
            for (int i = 0; i < _boots_item.Length; i++)
            {
                if (_boots_item[i] != null && _boots_item[i].TryGetComponent(out Item item))
                {
                    new_weapon_item[i] = item.ItemObject.ArmorItem;
                }
            }
            return new_weapon_item;
        }
    }
    public ArmorItem[] BreasplateItem
    {
        get
        {
            ArmorItem[] new_weapon_item = new ArmorItem[_breasplate_item.Length];
            for (int i = 0; i < _breasplate_item.Length; i++)
            {
                if (_breasplate_item[i] != null && _breasplate_item[i].TryGetComponent(out Item item))
                {
                    new_weapon_item[i] = item.ItemObject.ArmorItem;
                }
            }
            return new_weapon_item;
        }
    }
    public ArmorItem[] HelmetItem
    {
        get
        {
            ArmorItem[] new_weapon_item = new ArmorItem[_helmet_item.Length];
            for (int i = 0; i < _helmet_item.Length; i++)
            {
                if (_helmet_item[i] != null && _helmet_item[i].TryGetComponent(out Item item))
                {
                    new_weapon_item[i] = item.ItemObject.ArmorItem;
                }
            }
            return new_weapon_item;
        }
    }


}