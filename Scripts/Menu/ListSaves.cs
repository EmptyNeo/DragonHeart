using UnityEngine;

public class ListSaves : MonoBehaviour 
{
    [SerializeField] private GameObject _prefab_save;
    [SerializeField] private RectTransform _content;
    public GameObject PrefabSave => _prefab_save;
    public RectTransform Content => _content;
}