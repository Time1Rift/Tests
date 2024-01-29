using System;
using UnityEngine;

public class Fields1 : MonoBehaviour
{
    [SerializeField] private float _speed;

    public event Action<float> Speed;

    [ContextMenu(nameof(Show))]
    private void Show()
    {
        Speed?.Invoke(_speed);
    }
}