using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private Transform Camera;
    [SerializeField] private PlayerDataSaveLoad _playerDataSaveLoad;
    private void Start()
    {
        _playerDataSaveLoad.Initialize();
        Camera.position = Player.Transform.position;
        
    }
}