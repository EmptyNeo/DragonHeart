using System;
using TMPro;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{ 
    [SerializeField] private TMP_Text _name; 
    [SerializeField] private string _nameValue;
    [SerializeField] private GameObject panel;

    private void Start()
    {
        _name.text = _nameValue;
    }

    public virtual void ViewPanel(bool state = true)
    {
        panel.SetActive(!panel.activeSelf);
        if(state == false)
        {
            panel.SetActive(false);
           
        }
    }  
}
public abstract class NPCAdditionalPanel : NPC
{
    [SerializeField] private GameObject _panel_additional;
    public GameObject PanelAdditional => _panel_additional;
    public virtual void ViewAdditionalPanel(bool state = true)
    {
        _panel_additional.SetActive(state);
    }
}