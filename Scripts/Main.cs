using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private PlayerDataSaveLoad _playerDataSaveLoad;
    private void Start()
    {
        _playerDataSaveLoad.Initialize();
        
    }
}