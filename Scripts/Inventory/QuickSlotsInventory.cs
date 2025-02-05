using UnityEngine;
using UnityEngine.UI;

public class QuickSlotsInventory : MonoBehaviour
{
    public static Transform Transform;
    [SerializeField] private Sprite _selected_sprite;
    [SerializeField] private Sprite _not_selected_sprite;
    [SerializeField] private GameObject _inventory;
    [SerializeField] private Transform _inventory_panel;
    [SerializeField] private float _default_size;
    [SerializeField] private float _change_size;

    private int _current_quickslot_id;
    public int CurrentQuickSlotId => _current_quickslot_id;
    private void Awake()
    {
        Transform = transform;
    }
    private void Update()
    {
        if (_inventory.activeSelf == false)
        {
            float mw = Input.GetAxis("Mouse ScrollWheel");
            if (mw > 0.1)
            {
                transform.GetChild(_current_quickslot_id).GetComponent<Image>().sprite = _not_selected_sprite;
                transform.GetChild(_current_quickslot_id).GetComponent<RectTransform>().localScale = new(_default_size, _default_size);

                if (_current_quickslot_id >= transform.childCount - 1)
                {
                    _current_quickslot_id = 0;
                }
                else
                {
                    _current_quickslot_id++;
                }

                transform.GetChild(_current_quickslot_id).GetComponent<Image>().sprite = _selected_sprite;
                transform.GetChild(_current_quickslot_id).GetComponent<RectTransform>().localScale = new(_change_size, _change_size);

            }
            if (mw < -0.1)
            {

                transform.GetChild(_current_quickslot_id).GetComponent<Image>().sprite = _not_selected_sprite;
                transform.GetChild(_current_quickslot_id).GetComponent<RectTransform>().localScale = new(_default_size, _default_size);

                if (_current_quickslot_id <= 0)
                {
                    _current_quickslot_id = transform.childCount - 1;
                }
                else
                {

                    _current_quickslot_id--;
                }
                transform.GetChild(_current_quickslot_id).GetComponent<Image>().sprite = _selected_sprite;
                transform.GetChild(_current_quickslot_id).GetComponent<RectTransform>().localScale = new(_change_size, _change_size);

            }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i + 1 < 10 && Input.GetKeyDown((i + 1).ToString()))
            {
                if (_current_quickslot_id == i)
                {

                    if (transform.GetChild(_current_quickslot_id).GetComponent<Image>().sprite == _not_selected_sprite)
                    {
                        transform.GetChild(_current_quickslot_id).GetComponent<Image>().sprite = _selected_sprite;
                        transform.GetChild(_current_quickslot_id).GetComponent<RectTransform>().localScale = new Vector2(_change_size, _change_size);

                    }
                    else
                    {
                        transform.GetChild(_current_quickslot_id).GetComponent<Image>().sprite = _not_selected_sprite;

                        transform.GetChild(_current_quickslot_id).GetComponent<RectTransform>().localScale = new Vector2(_default_size, _default_size);
                    }
                }
                else
                {
                    transform.GetChild(_current_quickslot_id).GetComponent<RectTransform>().localScale = new Vector2(_default_size, _default_size);
                    transform.GetChild(_current_quickslot_id).GetComponent<Image>().sprite = _not_selected_sprite;
                    _current_quickslot_id = i;
                    transform.GetChild(_current_quickslot_id).GetComponent<Image>().sprite = _selected_sprite;
                    transform.GetChild(_current_quickslot_id).GetComponent<RectTransform>().localScale = new Vector2(_change_size, _change_size);
                }
            }
        }

    }
}
