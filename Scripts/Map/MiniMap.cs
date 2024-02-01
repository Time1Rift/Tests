using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] private MiniMapMarker _markerPrefab;
    [SerializeField, Min(0f)] private float _scale = 1f;

    private RectTransform _rectTransform;
    private Transform _owner;

    public Transform Owner => _owner;

    public void Initialize(Transform owner)
    {
        _rectTransform = transform as RectTransform;
        _owner = owner;
    }

    public void Create(Transform target)
    {
        MiniMapMarker spawned = Instantiate(_markerPrefab, _rectTransform);
        spawned.Initialize(this, target, _scale);
    }
}