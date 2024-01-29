using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _lookSpeed = 10;

    private PlayerInpu _playerInput;

    private Vector2 _moveDirection;
    private Vector2 _lookDirection;
    private Vector3 _offsetLook;
    private Vector3 _offsetMove;

    private Vector2 MoveDirection => _playerInput.Player.Move.ReadValue<Vector2>();
    private Vector2 LookDirection => _playerInput.Player.Look.ReadValue<Vector2>();

    private void Awake()
    {
        _playerInput = new PlayerInpu();

        _playerInput.Player.Paddle.performed += OnPaddle;

        _playerInput.Player.Shoote.performed += ctx =>
        {
            if (ctx.interaction is HoldInteraction)
                OnShoot();
        };

        //_playerInput.Player.Move.performed += ctx => OnMove();

        //_playerInput.Player.Move.performed += OnMove;
        //_playerInput.Player.Look.performed += OnLook;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        _moveDirection = MoveDirection;
        _lookDirection = LookDirection;

        Move();
        Look();
    }

    private void Look()
    {
        if (_lookDirection.sqrMagnitude < 0.1f)
            return;

        _offsetLook = new Vector3(-_lookDirection.y, _lookDirection.x, 0);
        transform.Rotate(_offsetLook * Time.deltaTime * _lookSpeed);
    }

    private void Move()
    {
        if (_moveDirection.sqrMagnitude < 0.1f)
            return;

        _offsetMove = new Vector3(_moveDirection.x, 0, _moveDirection.y);
        transform.Translate(_offsetMove * Time.deltaTime * _moveSpeed);
    }

    //private void OnMove(InputAction.CallbackContext context)
    //{
    //    _moveDirection = context.action.ReadValue<Vector2>();        
    //}

    //private void OnLook(InputAction.CallbackContext context)
    //{
    //    _lookDirection = context.action.ReadValue<Vector2>();
    //}

    //private void OnMove()
    //{
    //    _moveDirection = MoveDirection;

    //    Debug.Log(_moveDirection);
    //}

    public void OnShoot()
    {
        Debug.Log("Shoot");
    }

    public void OnPaddle(InputAction.CallbackContext context)
    {
        if (context.interaction is PaddleInteraction)
            Paddle();
    }

    public void Paddle()
    {
        Debug.Log("Paddle");
    }
}