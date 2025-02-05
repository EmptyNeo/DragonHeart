using UnityEngine;

public class UIInventory : UIControll
{
    [SerializeField] private GameObject _info_panel;
    public override void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            Panel.SetActive(!Panel.activeSelf);
            _info_panel.SetActive(false);
        }
    }
}
