using UnityEngine;

public abstract class ComboAttacked : Near
{

    [SerializeField] private int _amount_combo;
    [SerializeField] private int _amount_click;

    public int AmountClick 
    {
        get => _amount_click; 
        private set => _amount_click = value;   

    }
    public int AmountCombo  => _amount_combo; 
    public virtual void AddAmountClick()
    {
        if(_amount_click + 1 < _amount_combo) 
            AmountClick++;
    }
    public virtual void NullifyAmountClick() => AmountClick = 0;
    
}
    

