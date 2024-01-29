using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class InventoryDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 _startPosition;
    private Transform _originalParent;
    private IDragSource _source;
    private Canvas _parentCanvas;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _parentCanvas = GetComponentInParent<Canvas>();
        _source = GetComponentInParent<IDragSource>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
        _originalParent = transform.parent;

        _canvasGroup.blocksRaycasts = false;
        transform.SetParent(_parentCanvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _startPosition;
        _canvasGroup.blocksRaycasts = true;
        transform.SetParent(_originalParent);

        IDragDestination container; // создаём пустой временный контейнер с интерфейсом куда можно положить item

        if (EventSystem.current.IsPointerOverGameObject() == false)  //  попадает ли eventData на UI объект и выводит bool переменную
            container = _parentCanvas.GetComponent<IDragDestination>(); //  пытаемся присвоить интерфейс которого нету, зачем?
        else
            container = GetContainer(eventData);    // если попадаем то пытаемся достать контейнер с интерфейсом куда можно положить item

        if (container != null)
            DropItemIntoContainer(container);
    }

    private IDragDestination GetContainer(PointerEventData eventData)
    {

        if (eventData.pointerEnter) //  попадает ли eventData на UI объект и выводит bool переменную
        {
            var container = eventData.pointerEnter.GetComponentInParent<IDragDestination>(); // если да то ищем интерфейс который получает в себя item, если его нет то добавляеться null
            return container;
        }

        return null; // если нет то выводим null
    }

    private void DropItemIntoContainer(IDragDestination destination)
    {
        if (ReferenceEquals(destination, _source))  //  сравниваем является ли новый контейнер старым контейнером
            return;

        var destinationContainer = destination as IDragContainer;   //  проверяем является ли объект контейнером для item
        var sourceContainer = _source as IDragContainer;            //  проверяем является ли объект контейнером для item

        if (destinationContainer == null
            || sourceContainer == null
            || destinationContainer.GetItem() == null
            || ReferenceEquals(destinationContainer.GetItem(), sourceContainer.GetItem()))
        {
            Transfer(destination);
            return;
        }
    }

    private void Transfer(IDragDestination destination)
    {
        var draggingItem = _source.GetItem();

        if (draggingItem != null)
        {
            _source.RemoveItem();
            destination.AddItem(draggingItem);
        }
    }
}