using UnityEngine;

public class Rotation1 : MonoBehaviour
{
    private const string MouseY = "Mouse Y";
    private const string MouseX = "Mouse X";

    [SerializeField, Min(0)] private readonly float _horizontalTurnSensitivity = 7f;
    [SerializeField, Min(0)] private readonly float _verticalTurnSensitivity = 10f;
    [SerializeField, Min(0)] private readonly float _verticalMinAngle = -89f;
    [SerializeField, Min(0)] private readonly float _verticalMaxAngle = 89f;

    private Transform _cameraTransform;
    private Transform _transform;
    private float _cameraAngle;

    private void Awake()
    {
        _cameraTransform = GetComponentInChildren<Camera>().transform;
        _transform = transform;
        _cameraAngle = _cameraTransform.localEulerAngles.x;
    }

    private void Update()
    {
        _cameraAngle -= Input.GetAxis(MouseY) * _verticalTurnSensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngle, _verticalMaxAngle);
        _cameraTransform.localEulerAngles = Vector3.right * _cameraAngle;

        _transform.Rotate(Vector3.up * _horizontalTurnSensitivity * Input.GetAxis(MouseX));
    }
}