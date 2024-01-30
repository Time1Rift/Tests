using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(InputPlayer), typeof(MoverentPhysics1))]
public class Moverent1 : MonoBehaviour
{
    private const string Vertical = nameof(Vertical);
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _strafeSpeed = 7f;
    [SerializeField] private float _jumpSpeed = 9f;
    [SerializeField] private float _gravityFactor = 2f;

    private MoverentPhysics1 _moverentPhysics1;
    private CharacterController _characterController;
    private Vector3 _verticalVelocity;
    private Vector3 _horizontalVelocity;
    private Vector3 _playerSpeed;

    private void Awake()
    {
        _moverentPhysics1 = GetComponent<MoverentPhysics1>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _playerSpeed = _moverentPhysics1.Forward * Input.GetAxis(Vertical) * _speed + _moverentPhysics1.Right * Input.GetAxis(Horizontal) * _strafeSpeed;

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