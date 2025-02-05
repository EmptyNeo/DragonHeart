using UnityEngine;

public class UIControll : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private KeyCode _key;

    public GameObject Panel { get => _panel;  }
    public KeyCode Key { get => _key;  }

    public virtual void Update()
    {
        if (Input.GetKeyDown(Key))
            Panel.SetActive(!Panel.activeSelf);

    }
}
