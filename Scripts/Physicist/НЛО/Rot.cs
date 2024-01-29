using UnityEngine;

public class Rot : MonoBehaviour
{
    [SerializeField] private bool _isRotate;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _rotate;

    private void Update()
    {
        if (_isRotate)
        {
            _player.rotation = Quaternion.LookRotation(_target.position - _player.position);

            //_player.rotation = Quaternion.Euler(_rotate);
        }
    }
}