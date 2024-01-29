using UnityEngine;

public class Looking : MonoBehaviour
{
    private readonly string MouseX = "Mouse X";
    private readonly string MouseY = "Mouse Y";

    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Transform _camera;

    private void Update()
    {
        _camera.Rotate(_rotateSpeed * -Input.GetAxis(MouseY) * Time.deltaTime * Vector3.right);
        transform.Rotate(_rotateSpeed * Input.GetAxis(MouseX) * Time.deltaTime * Vector3.up);
    }
}