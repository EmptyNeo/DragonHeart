using UnityEngine;

public abstract class Quantity : MonoBehaviour
{
    [SerializeField] protected int _amount;
    public int Amount => _amount;
    public virtual void Initialization(int amount) => _amount = amount;
}