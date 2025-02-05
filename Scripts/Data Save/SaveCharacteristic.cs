using UnityEngine;

public class SaveCharacteristic : MonoBehaviour
{
    [SerializeField] Class _class;
    [SerializeField] private ChangeClass _change_class;

    public void SaveCharacteristics(int amount_save) 
    {
        BinarySavingSystem.SaveCharacteristics(_class, _change_class.Index, amount_save);
    } 
}
