using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpgrade : MonoBehaviour
{

    [SerializeField] private float _amount_xp;
    [SerializeField] private TextMeshProUGUI[] _character;
    [SerializeField] private TextMeshProUGUI count_ability;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private Image xp_image;
    [SerializeField] private TextMeshProUGUI _xp;
    [SerializeField] private float _max_xp;
    [SerializeField] private GameObject _updage;
    private PlayerAttack _player_attack => GetComponent<PlayerAttack>();
    private Characteristics _characteristics => GetComponent<Characteristics>();
    private string[] _character_before;
    private int[] _amount_character;
    public static int level = 1;
    [SerializeField]
    private int _ability,
                 _health,
                _strength,
                _mana,
                _endurance,
                _craft;
    public static LevelUpgrade Instance;
    public TextMeshProUGUI[] Character => _character;
    public int Health { get { return _health; } private set { _health = value; } }
    public int Strength { get { return _strength; } private set { _strength = value; } }
    public int Mana { get { return _mana; } private set { _mana = value; } }
    public int Endurance { get { return _endurance; } private set { _endurance = value; } }
    public int Craft { get { return _craft; } private set { _craft = value; } }
    public float Xp { get { return _amount_xp; } private set { _amount_xp = value; } }
    public float MaxXp { get { return _max_xp; } private set { _max_xp = value; } }
    public int Ability { get { return _ability; } private set { _ability = value; } }

    private void Awake()
    {
        Instance = this;
    }
    public void Initialization()
    {
        count_ability.text = _ability.ToString();
        _level.text = level.ToString();
        _character[0].text = Health.ToString();
        _character[1].text = Strength.ToString();
        _character[2].text = Mana.ToString();
        _character[3].text = Endurance.ToString();
        _character[4].text = Craft.ToString();
        _xp.text = _amount_xp.ToString() + "/" + _max_xp;
        xp_image.fillAmount = _amount_xp / _max_xp;
    }
    public void Initialization(int ability, float max_xp, float xp, int health, int strength, int mana, int endurance, int craft)
    {
        Ability = ability;
        MaxXp = max_xp;
        Xp = xp;
        Health = health;
        Strength = strength;
        Mana = mana;
        Endurance = endurance;
        Craft = craft;
    }
    public void StartGameInitialization(int health, int strength, int mana, int endurance, int craft)
    {
        Ability = 0;
        MaxXp = 1000;
        Xp = 0;
        Health = health;
        Strength = strength;
        Mana = mana;
        Endurance = endurance;
        Craft = craft;
    }
    public string[] GetCharactersString()
    {
        string[] strings = new string[_character.Length];
        for (int i = 0; i < strings.Length; i++)
        {
            strings[i] = _character[i].text;
        }
        return strings;
    }
    private int[] GetCharactestValue()
    {
        _amount_character = new int[_character.Length];
        for (int i = 0; i < _character.Length; i++)
        {
            if (int.TryParse(_character[i].text, out int character))
            {
                _amount_character[i] = character;
            }
        }
        return _amount_character;
    }

    private int _before_ability;
    public void PlusUpgrade(int index)
    {
        if (_updage.activeSelf == false)
        {
            _before_ability = _ability;
            _character_before = GetCharactersString();
            _amount_character = GetCharactestValue();
        }
        if (_ability > 0)
        {
            if (int.TryParse(_character[index].text, out int character))
            {

                _updage.SetActive(true);
                _amount_character[index] = character;
                _amount_character[index]++;
                _character[index].text = _amount_character[index].ToString();
                _ability--;
                count_ability.text = _ability.ToString();
                if (index < _character.Length - 1)
                {
                    _characteristics.TextStatus[index].text = StatusCharacter(index);
                }
            }
        }
    }
    public void MinusUpgrade(int index)
    {
        if (_ability >= 0 && (_character_before != null && int.TryParse(_character_before[index], out int character_before)))
        {
            if (character_before < _amount_character[index])
            {
                _amount_character[index]--;

                _character[index].text = _amount_character[index].ToString();
                _ability++;
                count_ability.text = _ability.ToString();
                if (index < _character.Length - 1)
                {
                    _characteristics.TextStatus[index].text = StatusCharacter(index);
                }
            }
        }
    }
    public void ApplyUpgrade()
    {

        if (int.TryParse(_character[0].text, out int health))
            Health = health;
        _characteristics.MaxHealth = (Health * 150 / 100) + 10;

        if (int.TryParse(_character[1].text, out int strength))
            Strength = strength;
        _characteristics.InitializationText(_player_attack.Weapon.Damage - _player_attack.Damage + _strength / 3f);
        _player_attack.Initialization(_player_attack.Delay, _strength / 3f);
        if (int.TryParse(_character[2].text, out int mana))
            Mana = mana;
        _characteristics.MaxMana = Mana * 150 / 100;

        if (int.TryParse(_character[3].text, out int endurance))
            Endurance = endurance;
        _characteristics.MaxEndurance = (Endurance * 50 / 100) + 5;
        _characteristics.Speed = (Endurance * 5 / 100) + _characteristics.BaseSpeed;
        _characteristics.SpeedBoost = (Endurance * 1.5f / 100) + _characteristics.Speed * 1.5f;

        if (int.TryParse(_character[4].text, out int craft))
            Craft = craft;

        _characteristics.InitializationText();
        Initialization();
        _updage.SetActive(false);

    }
    public void CancelUgrade()
    {
        for (int i = 0; i < _character.Length; i++)
        {
            _character[i].text = _character_before[i];
        }
        _updage.SetActive(false);
        _ability = _before_ability;
        count_ability.text = _ability.ToString();
        _character_before = null;
        ReturnStatusValue();
    }
    public void TakeXp(float xp)
    {
        _amount_xp += xp;
        xp_image.fillAmount = _amount_xp / _max_xp;
        _xp.text = _amount_xp.ToString("N0") + "/" + _max_xp.ToString("N0");
        if (_amount_xp >= _max_xp)
        {
            TakeXp(Instance.UpLevel());
        }

    }
    public float UpLevel()
    {
        level++;
        _ability++;
        count_ability.text = _ability.ToString();
        _level.text = level.ToString();
        float amount_xp = _amount_xp - _max_xp;
        _max_xp *= 1.75f;
        _amount_xp = 0;
        xp_image.fillAmount = _amount_xp / _max_xp;
        _xp.text = _amount_xp.ToString("N0") + "/" + _max_xp.ToString("N0");
        return amount_xp;
    }
    public string StatusCharacter(int index)
    {

        switch (index)
        {
            case 0:
                if(int.TryParse(_character[0].text, out int health))
                    health = (health * 150 / 100) + 10;
                return _characteristics.Health + "/" + health.ToString("N0");
            case 1:
                if (float.TryParse(_character[1].text, out float damage))
                    damage = _player_attack.Weapon.Damage - _player_attack.Damage + damage / 3f;
                return damage.ToString("N1");
            case 2:
                if (int.TryParse(_character[2].text, out int mana))
                    mana = mana * 150 / 100;
                return _characteristics.Mana + "/" + mana.ToString("N0");
            case 3:
                if (int.TryParse(_character[3].text, out int endurance))
                    endurance = (endurance * 50 / 100) + 5;
                return _characteristics.Endurance + "/" + endurance.ToString("N0");
        }
        return "";
    }

    private void ReturnStatusValue()
    {
        _characteristics.TextStatus[0].text = _characteristics.Health.ToString() + "/" + _characteristics.MaxHealth.ToString();
        _characteristics.TextStatus[1].text = _player_attack.Weapon.Damage.ToString("N1");
        _characteristics.TextStatus[2].text = _characteristics.Mana.ToString() + "/" + _characteristics.MaxMana.ToString();
        _characteristics.TextStatus[3].text = _characteristics.Endurance.ToString() + "/" + _characteristics.MaxEndurance.ToString();
    }
}
