using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Characteristics : MonoBehaviour
{
    private readonly string _format = "N0";
    private float _max_health,
                  _max_endurance,
                  _max_mana,
                  _endurance,
                  _speed,
                  _speed_boost,
                  _health,
                  _mana;

    [SerializeField]
    private TextMeshProUGUI _text_health,
                            _text_endurance,
                            _text_mana,
                             _text_status_health,
                             _text_status_endurance,
                             _text_status_mana,
                            _physical_protection,
                            _magic_protection,
                             _text_damage;

    [SerializeField] private TextMeshProUGUI[] _text_status;
    private TextMeshProUGUI[] _text_characters;

    [SerializeField]
    private Image _endurance_bar,
                  _health_bar,
                  _mana_bar;
    private int _max_physical_protection = 100,
                 _max_magic_protection = 100;

    private int _total_physical_protection,
               _total_magic_protection;

    public float MaxHealth { get => _max_health; set => _max_health = value; }
    public float MaxEndurance { get => _max_endurance; set => _max_endurance = value; }
    public float MaxMana { get => _max_mana; set => _max_mana = value; }
    public float Endurance { get => _endurance; private set => _endurance = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float SpeedBoost { get => _speed_boost; set => _speed_boost = value; }
    public int TotalPhysicalProtection => _total_physical_protection;
    public int TotalMagicProtection => _total_magic_protection;
    public float Health
    {
        get => _health;
        set
        {
            if (value < 0)
                _health = 0;
            else
                _health = value;
        }
    }
    public float Mana
    {
        get => _mana;
        set
        {
            if (value < 0)
                _mana = 0;
            else _mana = value;
        }
    }
    public TextMeshProUGUI TextHealth => _text_health;
    public TextMeshProUGUI TextEndurance => _text_endurance;
    public TextMeshProUGUI TextMana => _text_mana;
    public TextMeshProUGUI[] TextStatus
    {
        get
        {
            LevelUpgrade level_upgrade = LevelUpgrade.Instance;
            _text_status = new TextMeshProUGUI[level_upgrade.Character.Length - 1];
            _text_status[0] = _text_status_health;
            _text_status[1] = _text_damage;
            _text_status[2] = _text_status_mana;
            _text_status[3] = _text_status_endurance;
            return _text_status;
        }
    }
    public TextMeshProUGUI TextStatusHealth => _text_status_health;
    public TextMeshProUGUI TextStatusEndurance => _text_status_endurance;
    public TextMeshProUGUI TextStatusMana => _text_status_mana;
    public TextMeshProUGUI TextDamage => _text_damage;
    public Image EnduranceBar => _endurance_bar;
    public Image HealthBar => _health_bar;
    public Image ManaBar => _mana_bar;

    public void InitializationText(float damage_weapon)
    {
        _text_damage.text = damage_weapon.ToString("N1");
    }
    public void InitializationText()
    {
        TextEndurance.text = Endurance.ToString(_format) + "/" + MaxEndurance;
        TextHealth.text = Health.ToString(_format) + "/" + MaxHealth;
        TextMana.text = Mana.ToString(_format) + "/" + MaxMana;

        EnduranceBar.fillAmount = Endurance / MaxEndurance;
        HealthBar.fillAmount = Health / MaxHealth;
        ManaBar.fillAmount = Mana / MaxMana;
        _text_status_health.text = TextHealth.text;
        _text_status_mana.text = TextMana.text;
        _text_status_endurance.text = TextEndurance.text;
    }

    public void Initialization(int total_physical_protection, int total_magic_protection)
    {
        _total_physical_protection = total_physical_protection;
        _total_magic_protection = total_magic_protection;
        _physical_protection.text = _total_physical_protection + "%";
        _magic_protection.text = _total_magic_protection + "%";
    }
    public void Initialization(float max_health, float max_mana, float max_endurance, float speed, float speed_boost)
    {
        MaxHealth = max_health;
        MaxMana = max_mana;
        MaxEndurance = max_endurance;
        Speed = speed;
        SpeedBoost = speed_boost;
        Health = MaxHealth;
        Mana = MaxMana;
        Endurance = MaxEndurance;
    }
    public float AddUpCharacteristic(Image bar, TextMeshProUGUI text, float characteristic, float max_characteristic, float amount)
    {
        characteristic += amount;
        bar.fillAmount = characteristic / max_characteristic;
        text.text = characteristic.ToString(_format) + "/" + max_characteristic;
        return characteristic;
    }
    public void TakeDamage(float amount)
    {
        float percent_amount = amount / 100;
        amount -= percent_amount * _total_physical_protection;
        Health -= amount;
        HealthBar.fillAmount = Health / MaxHealth;
        TextHealth.text = Health.ToString(_format) + "/" + MaxHealth;
    }
    public void TakeHealth(float amount)
    {
        Health -= amount;
        HealthBar.fillAmount = Health / MaxHealth;
        TextHealth.text = Health.ToString(_format) + "/" + MaxHealth;
    }
    public void AddUpHealth(float amount)
    {
        Health += amount;
        HealthBar.fillAmount = Health / MaxHealth;
        TextHealth.text = Health.ToString(_format) + "/" + MaxHealth;
    }
    public void TakeMana(float amount)
    {
        Mana -= amount;
        ManaBar.fillAmount = Mana / MaxMana;
        TextMana.text = Mana.ToString(_format) + "/" + MaxMana;
    }
    public void AddUpMana(float amount)
    {
        Mana += amount;
        ManaBar.fillAmount = Mana / MaxMana;
        TextMana.text = Mana.ToString(_format) + "/" + MaxMana;
    }
    public void TakeEndurance(float amount)
    {
        Endurance -= amount;
        EnduranceBar.fillAmount = Endurance / MaxEndurance;
        TextEndurance.text = Endurance.ToString(_format) + "/" + MaxEndurance;
    }
    public void AddUpEndurance(float amount)
    {
        Endurance += amount;
        EnduranceBar.fillAmount = Endurance / MaxEndurance;
        TextEndurance.text = Endurance.ToString(_format) + "/" + MaxEndurance;
    }
    public void DivisionSpeed(float amount)
    {
        Speed /= amount;
    }
    public void MultipleSpeed(float amount)
    {
        Speed *= amount;
    }
}
