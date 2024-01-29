using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _joystick;
    [SerializeField] private Image _joystickBackground;    
    [SerializeField] private Image _joystickArea;    

    private Vector2 _joystickBackgroundStartPosition;

    protected Vector2 InputVector;

    [SerializeField] private Color _inactiveJoystickColor;    
    [SerializeField] private Color _activeJoystickColor;  
    
    private bool _isJoystickActive = false;

    private float _modifier = 2;
    private float _lengthSegment = 1;


    private void Start()
    {
        ClickEffect();

        _joystickBackgroundStartPosition = _joystickBackground.rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)  // когда мы водим польцем или мышкой по икрану 
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position, null, out Vector2 joystickPosition))
        {
            InputVector = joystickPosition * _modifier / _joystickBackground.rectTransform.sizeDelta;    //  формула не понятна
            InputVector = (InputVector.magnitude > _lengthSegment) ? InputVector.normalized : InputVector;
            _joystick.rectTransform.anchoredPosition = InputVector * (_joystickBackground.rectTransform.sizeDelta / _modifier);
        }
    }

    public void OnPointerDown(PointerEventData eventData)   // когда нажимаем польцем или мышкой по икрану
    {
        ClickEffect();

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea.rectTransform, eventData.position, null, out Vector2 joystickBacgroundPosition))
            _joystickBackground.rectTransform.anchoredPosition = joystickBacgroundPosition;
    }

    public void OnPointerUp(PointerEventData eventData)     // когда отпускаем полец или мышку от икрана
    {
        _joystickBackground.rectTransform.anchoredPosition = _joystickBackgroundStartPosition;

        ClickEffect();

        InputVector = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    private void ClickEffect()
    {
        if (_isJoystickActive)
        {
            _joystick.color = _inactiveJoystickColor;
            _isJoystickActive = false;
        }
        else
        {
            _joystick.color = _activeJoystickColor;
            _isJoystickActive = true;
        }
    }
}