using UnityEngine;

public class ForwardMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float _step = 5;

    public void Move()
    {
        transform.Translate(transform.forward * _step);
    }
}