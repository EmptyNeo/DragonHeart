using System.Collections;
using UnityEngine;

public class PlayerAttack : ComboAttacked
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _inventory;
    private Characteristics _characteristics => GetComponent<Characteristics>();
    private Player _player => GetComponent<Player>();
    private bool _attack;
    public Attacked Weapon;
    public ISpecialAttack SpecialAttack;
    private ComboAttacked _combo_attacked 
    { 
        get 
        {
            if(Weapon is ComboAttacked comboAttacked) 
                return comboAttacked; 
            else 
                return null;
        }
    }

    public bool Attack => _attack;

    private void Update()
    {
        
        if(_inventory.activeSelf == false)
        {
            Weapon.OnUpdate();
            if (Input.GetMouseButtonDown(0))
            {
                if(_attack &&_combo_attacked != null)
                {       
                    _combo_attacked.AddAmountClick();
                }
            }

        }
    }
    
    public void StartSpecialAttack()
    {
        if(SpecialAttack != null)
            StartCoroutine(SpecialAttack.SpecialAttack());
    }
    public void StartAttack()
    {
        StartCoroutine(Weapon.IAttack());

    }
    public void TryContinueCombo(int i)
    {
        if(Weapon is ComboAttacked combo_attacked)
        {
            if(combo_attacked.AmountClick < i)
            {
                AttackAnimOff();
            }
        }
        
    }
    public override IEnumerator IAttack()
    {
        IsDelay = true;
        
        Collider2D[] enemy_collider = Physics2D.OverlapCircleAll(Circle.position, Radius);
        if(enemy_collider != null)
        {
            for(int i = 0; i < enemy_collider.Length; i++)
            {
                if (enemy_collider[i].gameObject.TryGetComponent(out DamageHandlerEnemy enemy))
                {
                    enemy.TakeDamage(Damage);
                    Debug.Log("Take from " + enemy.name);
                }
            }

        }
        yield return new WaitForSeconds(Delay);
        IsDelay = false;
    }
    public virtual void AttackAnimOn()
    {
        if (Weapon.NameAttack == "")
            Debug.Log("Weapon Name Attack is Null");
        else
        {
            if (!_attack)
                _characteristics.DivisionSpeed(2.5f);

            _attack = true;
            _player.Rigidbody2D.linearVelocity = Vector2.zero;
            _animator.SetBool("IsMove", false);
            _animator.SetBool(Weapon.NameAttack, true);
        }
    }
    public virtual void AttackAnimOff()
    {
        if (Weapon.NameAttack == "")
            Debug.Log("Weapon Name Attack is Null");
        else
        {
            if(_combo_attacked != null)
                _combo_attacked.NullifyAmountClick();
            if (_attack)
                _characteristics.MultipleSpeed(2.5f);
            _attack = false;
            _animator.SetBool(Weapon.NameAttack, false);
        }
    }
}
