using UnityEngine;

public class MiniMapMarker : MonoBehaviour
{
    private float _scale = 1f;
    private MiniMap _miniMap;
    private Transform _target;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = transform as RectTransform;
    }

    private void Update()
    {
        if (_miniMap.Owner == null)
            return;

        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 coordinate = _miniMap.Owner.InverseTransformPoint(_target.position);
        Vector2 position = new Vector2(coordinate.x, coordinate.z);
        position *= _scale;

        _rectTransform.anchoredPosition = position;
    }

    public void Initialize(MiniMap miniMap, Transform target, float scale)
    {
        _target = target;
        _scale = scale;
        _miniMap = miniMap;
    }
}