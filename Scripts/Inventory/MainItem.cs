using UnityEngine;

[CreateAssetMenu(fileName = "Main Item", menuName = "Inventory/New Main Item")]
public class MainItem : ItemScriptableObject
{
    [SerializeField] private float _change_characteristics;
    [SerializeField] private AudioClip _audioClip;
    public float ChangeCharacteristics => _change_characteristics;
    public AudioClip AudioClip => _audioClip;
    public override string GetDescription()
    {
        return NameItem;
    }
}
