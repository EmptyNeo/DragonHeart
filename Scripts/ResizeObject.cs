using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResizeObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Slider _slider;
    private bool _dragging;
    private Vector2 _initialOffset;
    private void Start()
    {
        _slider.onValueChanged.AddListener((size) =>
        {
            transform.localScale = new Vector2(size, size);
        });

    }
    private void FixedUpdate()
    {
        if (_dragging == true && Input.GetMouseButton(0)) 
        {
            Vector2 offset = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;

            transform.position = (Vector2)transform.position + offset * Time.fixedDeltaTime * 15f; 
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _dragging = true;
        _initialOffset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _dragging = false;
    }
}
