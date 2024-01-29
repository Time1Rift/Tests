using UnityEngine;

public class Moverent : MonoBehaviour
{
    private readonly string Horizontal = nameof(Horizontal);
    private readonly string Vertical = nameof(Vertical);

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private FootStepsSounds _stepsSounds;
    [SerializeField] private float _stepDistance;

    private float _coveredDistance = 0f;

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        float rotation = Input.GetAxis(Horizontal);

        transform.Rotate(rotation * _rotateSpeed * Time.deltaTime * Vector3.up);
    }

    private void Move()
    {
        float direction = Input.GetAxis(Vertical);

        if (direction == 0f)
        {
            _coveredDistance = 0f;
            _stepsSounds.Pause();            
            return;
        }

        float distance = direction * Time.deltaTime * _moveSpeed;
        _coveredDistance += Mathf.Abs(distance);
        transform.Translate(distance * Vector3.forward);

        if (_coveredDistance >= _stepDistance)
        {
            _coveredDistance -= _stepDistance;
            _stepsSounds.Play();
        }
    }
}