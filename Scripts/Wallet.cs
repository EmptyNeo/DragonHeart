using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _amount;
    [SerializeField] private int[] _balance;

    public int[] Balance { get => _balance;  }
    public void Initialization(int[] balance)
    {
        _balance = balance;

        for (int i = 0; i < balance.Length; i++)
        {
            _amount[i].text = _balance[i].ToString();
        }
    }
    public void Addition(int id, int amount)
    {
        _balance[id] += amount;
        _amount[id].text = _balance[id].ToString();
    }
    public void Reduction(int id, int amount)
    {
        _balance[id] -= amount;
        _amount[id].text = _balance[id].ToString();
    }
}
