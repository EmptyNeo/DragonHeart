using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Bow : Distance
{
    [SerializeField] private KeyCode _key_recharge => KeyCode.R;
    [SerializeField] private GameObject _prefab_arrow;
    [SerializeField] private ItemScriptableObject _item_arrow;
    [SerializeField] private Sprite _arrow;
    [SerializeField] private Transform _point;
    [SerializeField] private int _amount_arrow;
    [SerializeField] private int _max_amount_arrow;
    private TextMeshProUGUI _amount;
    private float _speed;
    private bool _is_recharge;
    public Sprite Arrow => _arrow;

    private void Start()
    {
        if (transform.parent != null)
        {
            _point = transform.parent.transform.GetChild(0);
        }
        _speed = (LevelUpgrade.Instance.Strength * 1.25f) + 6;
    }
    public override void OnUpdate()
    {
        if (_is_recharge == false && Input.GetKeyDown(_key_recharge))
            StartCoroutine(QuiverRecharge());
        if (Input.GetMouseButtonDown(0) && IsDelay == false)
        {
            OnAttack();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            AttackOff();
        }
    }
    public override void Action(Transform equipment_object, PlayerAttack _player_attack, PlayerEquipmentHandler player_equipment_handler)
    {
        player_equipment_handler.ConsumableItem.sprite = Arrow;
        transform.parent = equipment_object;
        player_equipment_handler.ParentAmountArrow.SetActive(true);

        if (player_equipment_handler.ParentAmountArrow.transform.GetChild(0).TryGetComponent(out TextMeshProUGUI text))
            _amount = text;
    }
    public override void OnAttack()
    {
        if (_amount_arrow - 1 < 0)
        {
            Notification.Instance.SetNotification("Нет стрел в колчане");
            Notification.Instance.TurnBackground(true);
            StartCoroutine(Notification.Instance.TurnOffBackgroundOverTime(3));
        }
        else
            base.OnAttack();
    }
    public override IEnumerator IAttack()
    {

        IsDelay = true;

        _amount.text = (--_amount_arrow).ToString();
        try
        {
            GameObject arrow = Instantiate(_prefab_arrow);
            Vector2 direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)_point.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrow.transform.SetPositionAndRotation(_point.position, Quaternion.Euler(0, 0, angle - 45));
            arrow.GetComponent<Rigidbody2D>().linearVelocity = _speed * direction;
            arrow.GetComponent<Arrow>().Damage = Damage;

        }
        catch (Exception e)
        {
            Debug.Log("Arrow not instantiate, because " + e.Message);
        }
        yield return new WaitForSeconds(Delay);
        IsDelay = false;

    }
    private IEnumerator QuiverRecharge()
    {
        _is_recharge = true;
        _amount_arrow = Inventory.inventory.AmountBullet(_item_arrow, _amount_arrow, _max_amount_arrow);
        _amount.text = _amount_arrow.ToString();
        yield return new WaitForSeconds(Delay);
        _is_recharge = false;
    }
}
