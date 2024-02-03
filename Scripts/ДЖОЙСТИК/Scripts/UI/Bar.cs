using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private Camera _camera;

    private Vector3 _angleRotation = new Vector3 (0, 180, 0);

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        transform.Rotate(_angleRotation);
        //transform.rotation = _camera.rotation;
    }
}
