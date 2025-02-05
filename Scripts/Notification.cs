using System.Collections;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _notification;
    [SerializeField] private GameObject _background;
    public static Notification Instance { get; private set; }
    private void Start()
    {
        Instance = GetComponent<Notification>();
        Debug.Log(Instance.name);
    }
    public void SetNotification(string notification)
    {
        _notification.text = notification;
    }
    public void TurnBackground(bool turn)
    {
        _background.SetActive(turn);
    }
    public IEnumerator TurnOffBackgroundOverTime(float time)
    {
        yield return new WaitForSeconds(time);
        _background.SetActive(false);
    }

}
