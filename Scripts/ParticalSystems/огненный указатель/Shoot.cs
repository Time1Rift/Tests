using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Rigidbody _timelate;
    [SerializeField] private float _force;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody newObject = Instantiate(_timelate, transform.position, Quaternion.identity);
            newObject.velocity = _force * transform.forward;
        }
    }
}