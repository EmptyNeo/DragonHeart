using DG.Tweening;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private static Transform Transform;

    private void Start() =>  Transform = transform;

    public static void Do(float duration)
    {
        Transform.DOShakePosition(duration, 0.5f, 20, 45);
    }
}