using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    [SerializeField] private float _speed = 10;

    private Vector3 _direction;

    public void Rotate(Vector3 direction)
    {
        _direction = Vector3.RotateTowards(transform.forward, direction, _speed, 0);
        transform.rotation = Quaternion.LookRotation(_direction);
    }
}