using UnityEngine;

public class MouseRotator : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Vector3 _direction;
    private Quaternion _rotation;

    private void Update()
    {
        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            RotateToMouseDirection(hit.point);
    }

    private void RotateToMouseDirection(Vector3 position)
    {
        _direction = position - transform.position;
        _rotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, 1);
    }
}