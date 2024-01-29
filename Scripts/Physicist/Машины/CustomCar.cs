using Unity.VisualScripting;
using UnityEngine;

public class CustomCar : MonoBehaviour  //  poor implementation of Ackerman angles
{
    private const string Vertical = nameof(Vertical);
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private WheelCollider[] _frontWheels;
    [SerializeField] private WheelCollider[] _rearWheels;

    [SerializeField] private Transform[] _frontWheelsModels;
    [SerializeField] private Transform[] _rearWheelsModels;

    [Header("Physics")]
    [SerializeField] private Transform _centerOfMass;
    [SerializeField, Min(0)] private float _torque = 500f;
    [SerializeField] private float _maxAngle = 34f;

    [SerializeField] private float _debug;
    [SerializeField] private float _ackermanFactor;

    private float _acceleration;
    private float _steering;
    private float _sign;
    private float _factor;

    private void Awake()
    {
        GetComponent<Rigidbody>().centerOfMass = _centerOfMass.localPosition;
    }

    private void Update()
    {
        _acceleration = Input.GetAxis(Vertical);
        _steering = Input.GetAxis(Horizontal);

        foreach (WheelCollider wheel in _rearWheels)
            wheel.motorTorque = _acceleration * _torque;

        for (int i = 0; i < _frontWheels.Length; i++)   //  poor implementation of Ackerman angles
        {
            _sign = (i == 0) ? -1f : 1f;   //  -1 is the left wheel, 1 is the right wheel. The left wheel is at index 0, the right one is at index 1 in the array

            _factor = 1f;

            if (Mathf.Approximately(_sign, Mathf.Sign(_debug)))
                _factor = _ackermanFactor;

            Debug.Log(Mathf.Sign(0));

            _frontWheels[i].steerAngle = _steering * _maxAngle * _factor;
        }

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        for (int i = 0; i < _frontWheels.Length; i++)
        {
            _frontWheels[i].GetWorldPose(out Vector3 pos, out Quaternion quat);

            _frontWheelsModels[i].position = pos;
            _frontWheelsModels[i].rotation = quat;
        }
    }

    private void OnDrawGizmos() //  poor implementation of Ackerman angles
    {
        Gizmos.color = Color.red;

        foreach (WheelCollider wheel in _rearWheels)
        {
            Gizmos.DrawRay(wheel.transform.position, wheel.transform.right * 5f);
            Gizmos.DrawRay(wheel.transform.position, -wheel.transform.right * 5f);
        }

        for (int i = 0; i < _frontWheels.Length; i++)
        {
            //Quaternion rotation;

            //float sign = (i == 0) ? -1 : 1;   //  -1 is the left wheel, 1 is the right wheel. The left wheel is at index 0, the right one is at index 1 in the array

            //if (Mathf.Approximately(sign, Mathf.Sign(_debag)))
            //    rotation = Quaternion.AngleAxis(_debag * _ackermanFactor, Vector3.up);
            //else
            //    rotation = Quaternion.AngleAxis(_debag, Vector3.up);

            Quaternion rotation = Quaternion.AngleAxis(_frontWheels[i].steerAngle, Vector3.up); //

            Gizmos.DrawRay(_frontWheels[i].transform.position, rotation * (_frontWheels[i].transform.right * 5f));
            Gizmos.DrawRay(_frontWheels[i].transform.position, rotation * (-_frontWheels[i].transform.right * 5f));
        }
    }
}