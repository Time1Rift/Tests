using UnityEngine;

public class Sphere : MonoBehaviour
{
    private const float DefaultRadius = 1f;

    [SerializeField] private float _radius = 1f;
    [SerializeField] private GameObject _prefab;

    private void Start()
    {
        transform.localScale = Vector3.one * _radius;
    }

    public void ResetRadius() => _radius = DefaultRadius;
}