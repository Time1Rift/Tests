using UnityEngine;
using SplineMesh;

public class SplineFollower : MonoBehaviour
{
    [SerializeField] private Spline _spline;
    [SerializeField] private float _speed;
    [SerializeField] private float _sensitivity;

    private float _splineRate = 0f;
    private float _input = 0f;
    private float _lastMousPosition;

    private void Start()
    {
        _lastMousPosition = Input.mousePosition.x;
    }

    private void Update()
    {
        _input += (Input.mousePosition.x -_lastMousPosition) * _sensitivity;
        _lastMousPosition = Input.mousePosition.x;
        _input = Mathf.Clamp(_input, -1f, 1f);
        Debug.Log(_input);

        _splineRate += _speed * Time.deltaTime;
        
        if (_splineRate <= _spline.nodes.Count - 1)
            Place();
    }

    private void Place()
    {
        CurveSample sample = _spline.GetSample(_splineRate);

        transform.localPosition = sample.location + Vector3.right * _input;
        transform.localRotation = sample.Rotation;
    }
}