using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    [SerializeField] private GameObject panel;
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