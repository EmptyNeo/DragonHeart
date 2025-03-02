using DG.Tweening;
using TMPro;
using UnityEngine;

public class InfoItem : MonoBehaviour
{
    [SerializeField] private RectTransform _info_panel;
    [SerializeField] private TextMeshProUGUI _info;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _panel;
    public RectTransform Panel => _panel;

    public void ShowItemInfo(ItemScriptableObject item)
    {
        _info.text = item.GetDescription(); 
        _info_panel.sizeDelta = new Vector2(_info.preferredWidth + 70, _info.preferredHeight + 20);
    }
    private void FixedUpdate()
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                Input.mousePosition,
                _camera,
                out Vector2 localPosition))
        {
            _panel.anchoredPosition = localPosition;
        }
    }
}
