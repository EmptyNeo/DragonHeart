using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private bool _touch;
    [SerializeField] private KeyCode _key;
    [SerializeField] private MenuQuest _quest;
    public MenuQuest Quest => _quest;
    private NPC _npc;
    public void Update()
    {
        if(Input.GetKeyDown(_key) && _touch)
        {
            _npc.ViewPanel();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out NPC npc))
        {
            _touch = true;
            _npc = npc;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out NPC npc))
        {
            npc.ViewPanel(false);
            _touch = false;
        }
    }



}
