using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Min(0)] private float _moveSpeed;
    [SerializeField, Min(0)] private float _rotateSpeed;
    [SerializeField] private bool _isVisibleCursore;

    private PlayerInputActions _input;
    private CharacterController _controller;
    private Transform _cameraTransform;
    private Vector2 _moveDirection;
    private Vector3 _cameraForward;
    private Transform _transform;

    private Vector2 MoveDirection => _input.Player.Move.ReadValue<Vector2>();

    private void Awake()
    {
        _transform = transform;
        _input = new PlayerInputActions();
        _controller = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        Cursor.visible = _isVisibleCursore;

        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        _moveDirection = MoveDirection;
        
        if (_moveDirection.sqrMagnitude < 0.1f)
            return;

        _cameraForward = _cameraTransform.forward;
        _cameraForward.y = 0;
        _cameraForward.Normalize();

        Rotate(_cameraForward);
        Move(_cameraForward);
    }

    private void Rotate(Vector3 cameraForward)
    {
        Vector3 inputDirection = new Vector3(_moveDirection.x, 0, _moveDirection.y).normalized;

        Vector3 combinedDirection = Quaternion.LookRotation(cameraForward) * inputDirection;
        float targetAngle = Mathf.Atan2(combinedDirection.x, combinedDirection.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * _rotateSpeed);
    }

    private void Move(Vector3 cameraForward)
    {
        Vector3 movementDirection = _cameraTransform.right * _moveDirection.x + cameraForward * _moveDirection.y;
        movementDirection.y = 0;

        _controller.Move(movementDirection.normalized * _moveSpeed * Time.deltaTime);
    }
}

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterJump))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speedRotation;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private CharacterAnimator _characterAnimator;

    private IInput _input;
    private CharacterJump _characterJump;
    private CharacterController _characterController;

    public void Initialize(IInput input)
    {
        _characterJump = GetComponent<CharacterJump>();
        _characterController = GetComponent<CharacterController>();

        _input = input;
        _input.Moved += OnMoved;
        _input.Jump += OnJump;
    }

    private void OnDisable()
    {
        _input.Moved -= OnMoved;
        _input.Jump -= OnJump;
    }

    private void OnJump()
    {
        _characterJump.Jump();
    }

    private void OnMoved(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 inputDirection = new Vector3(direction.x, 0, direction.y).normalized;

            if (inputDirection.magnitude >= 0.1f)
            {
                Vector3 combinedDirection = Quaternion.LookRotation(cameraForward) * inputDirection;
                float targetAngle = Mathf.Atan2(combinedDirection.x, combinedDirection.z) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _speedRotation);
            }

            Vector3 movementDirection = cameraForward * direction.y + Camera.main.transform.right * direction.x;
            movementDirection.y = 0;

            _characterController.Move(movementDirection * _movementSpeed * Time.deltaTime);
        }

        _characterAnimator.Move(direction != Vector3.zero);
    }
}

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateBodySpeed;
    [SerializeField] private bool _isVisibleCursore;

    private event Action OnPlayerUpdate;
    private CharacterController _controller;
    private PlayerInputActions _input;
    private bool _isSubscribeMove = false;
    private Vector3 _cameraForward;
    private Vector2 _moveDirection;


    private void Awake()
    {
        _input = new PlayerInputActions();
        _controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        Cursor.visible = _isVisibleCursore;

        _input.Enable();

        _input.Player.Move.performed += MovementEnable;
        _input.Player.Move.canceled += MovementDisable;
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= MovementEnable;
        _input.Player.Move.canceled -= MovementDisable;

        _input.Disable();
    }

    private void Update()
    {
        OnPlayerUpdate?.Invoke();
    }

    private void Move()
    {
        Vector3 movementDirection = _cameraForward * _moveDirection.y + Camera.main.transform.right * _moveDirection.x;
        movementDirection.y = 0;

        _controller.Move(movementDirection.normalized * _moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        _cameraForward = Camera.main.transform.forward;
        _cameraForward.y = 0;
        _cameraForward.Normalize();

        Vector3 inputDirection = new Vector3(_moveDirection.x, 0, _moveDirection.y).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            Vector3 combinedDirection = Quaternion.LookRotation(_cameraForward) * inputDirection;
            float targetAngle = Mathf.Atan2(combinedDirection.x, combinedDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            _controller.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotateBodySpeed);
        }
    }

    private void MovementEnable(InputAction.CallbackContext context)
    {
        _moveDirection = context.action.ReadValue<Vector2>();
        _moveDirection *= _moveSpeed;

        if (_isSubscribeMove == false)
        {
            _isSubscribeMove = true;
            OnPlayerUpdate += Rotate;
            OnPlayerUpdate += Move;
        }
    }

    private void MovementDisable(InputAction.CallbackContext context)
    {
        _isSubscribeMove = false;
        OnPlayerUpdate -= Rotate;
        OnPlayerUpdate -= Move;
    }

}