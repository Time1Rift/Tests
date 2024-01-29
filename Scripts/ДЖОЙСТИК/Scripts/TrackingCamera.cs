using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _heightCamera = 12;
    [SerializeField] private float _rearDistance = 9;
    [SerializeField] private float _speedMovement = 2;

    private Vector3 _currentPosition;

    private Vector3 TargetPosition => _target.position;

    private void Start()
    {
        transform.position = new Vector3(TargetPosition.x, TargetPosition.y + _heightCamera, TargetPosition.z - _rearDistance);
        transform.rotation = Quaternion.LookRotation(TargetPosition - transform.position);
    }

    private void LateUpdate()
    {
        MoverCamera();
    }

    private void MoverCamera()
    {
        _currentPosition = new Vector3(TargetPosition.x, TargetPosition.y + _heightCamera, TargetPosition.z - _rearDistance);
        transform.position = Vector3.Lerp(transform.position, _currentPosition, _speedMovement * Time.deltaTime);
    }
}