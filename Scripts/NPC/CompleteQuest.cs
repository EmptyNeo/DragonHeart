using UnityEngine;
using System.Collections;

public class CompleteQuest : MonoBehaviour 
{
    [SerializeField] private GameObject _quest_complete;
    [SerializeField] private Transform quests_content;
    public void UpdateQuest(ItemScriptableObject _item, int _amount)
    {
        bool is_component_item;
        bool quest_complete = false;
        for (int i = 0; i < quests_content.childCount; i++)
        {
            is_component_item = quests_content
                                .GetChild(i).gameObject
                                .GetComponent<QuestInfo>().Item
                                .TryGetComponent(out Item item);
            if (is_component_item && item.ItemObject == _item)
            {
               quest_complete =
                quests_content.GetChild(i)
                .GetComponent<QuestInfo>()
                .NeedAmountUpdate(_amount);
                break;
            }
        }
        if(quest_complete)
        {
            StartCoroutine(QuestComplete());
        }
    }
    private IEnumerator QuestComplete()
    {
         _quest_complete.SetActive(true);
        yield return new WaitForSeconds(3);
        _quest_complete.SetActive(false);
    }
}