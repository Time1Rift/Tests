using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Moverent1 : MonoBehaviour
{
    private const string Vertical = nameof(Vertical);
    private const string Horizontal = nameof(Horizontal);

    [SerializeField, Min(0)] private readonly float _speed = 8f;
    [SerializeField, Min(0)] private readonly float _strafeSpeed = 7f;
    [SerializeField, Min(0)] private readonly float _jumpSpeed = 9f;
    [SerializeField, Min(0)] private readonly float _gravityFactor = 2f;
    
    private CharacterController _characterController;
    private Vector3 _verticalVelocity;
    private Vector3 _horizontalVelocity;
    private Vector3 _playerSpeed;
    private Vector3 _forward;
    private Vector3 _right;
    private Transform _cameraTransform;

    private void Awake()
    {
        _cameraTransform = GetComponentInChildren<Camera>().transform;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
        _right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;

        _playerSpeed = _forward * Input.GetAxis(Vertical) * _speed + _right * Input.GetAxis(Horizontal) * _strafeSpeed;

        if (_characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _verticalVelocity = Vector3.up * _jumpSpeed;
            else
                _verticalVelocity = Vector3.down;

            _characterController.Move((_playerSpeed + _verticalVelocity) * Time.deltaTime);
        }
        else
        {
            _horizontalVelocity = _characterController.velocity;
            _horizontalVelocity.y = 0;
            _verticalVelocity += Physics.gravity * Time.deltaTime * _gravityFactor;
            _characterController.Move((_horizontalVelocity + _verticalVelocity) * Time.deltaTime);
        }
    }
}