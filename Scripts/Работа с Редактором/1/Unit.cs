using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField, SerializeInterface(typeof(IMovable))] private GameObject _movableObject;

    private IMovable _movable;

    private void Start()
    {
        _movable = _movableObject.GetComponent<IMovable>();
        _movable.Move();
    }
}