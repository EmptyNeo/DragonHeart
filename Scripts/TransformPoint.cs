using UnityEngine;

public class TransformPoint : MonoBehaviour
{
    [SerializeField] private Transform _point;
    public Transform Point => _point;
}
