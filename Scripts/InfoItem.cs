using TMPro;
using UnityEngine;

public class InfoItem : MonoBehaviour
{
    [SerializeField] private RectTransform _info_panel;
    [SerializeField] private TextMeshProUGUI _info;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _offset;
    public RectTransform InfoPanel => _info_panel;
    private float width = 800;
    private void Start()
    {
        if (Screen.width > width)
        {
            _offset *= Screen.width / width;
        }
    }
    public void ShowItemInfo(ItemScriptableObject item)
    {
        _info.text = item.GetDescription();
        _info_panel.sizeDelta = new Vector2(_info.preferredWidth + 70, _info.preferredHeight + 20);
    }
    private void FixedUpdate()
    {
        if(_info_panel.gameObject.activeSelf)
        {
            Vector3 world_position = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (world_position.x +  (_offset / ( Screen.width / (3.5f * Screen.width / width))) > _camera.ScreenToWorldPoint(new Vector2(Screen.width - _offset, 0)).x)
            {
                _info_panel.position = (Vector2)_camera.ScreenToWorldPoint((Vector2)Input.mousePosition - new Vector2(_offset, 0));
            }
            else
            {
                _info_panel.position = (Vector2)_camera.ScreenToWorldPoint((Vector2)Input.mousePosition + new Vector2(_offset, 0));
            }
        }
    }
}
