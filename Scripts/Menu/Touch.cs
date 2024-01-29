using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class Touch : MonoBehaviour
{
    [SerializeField] private float _scaleValue;
    [SerializeField] private float _rateChange;

    private Image _image;
    private float _startScale = 1f;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void OnButtonClick()
    {
        _image.transform.DOScale(_scaleValue, _rateChange);
        _image.transform.DOScale(_startScale, _rateChange).SetDelay(_rateChange);
    }
}