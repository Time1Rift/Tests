using UnityEngine;

public class JoystickMovement : JoystickHandler
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private CharacterRotation _characterRotation;

    private void Update()
    {
        if (InputVector.x != 0 || InputVector.y != 0)
        {
            _characterMovement.Move(new Vector3(InputVector.x, 0, InputVector.y));
            _characterRotation.Rotate(new Vector3(InputVector.x, 0, InputVector.y));
        }
        else
        {
            _characterMovement.Move(new Vector3(Input.GetAxis(Horizontal), 0, Input.GetAxis(Vertical)));
            _characterRotation.Rotate(new Vector3(Input.GetAxis(Horizontal), 0, Input.GetAxis(Vertical)));
        }
    }
}