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

        IDragDestination container; // ������ ������ ��������� ��������� � ����������� ���� ����� �������� item

        if (EventSystem.current.IsPointerOverGameObject() == false)  //  �������� �� eventData �� UI ������ � ������� bool ����������
            container = _parentCanvas.GetComponent<IDragDestination>(); //  �������� ��������� ��������� �������� ����, �����?
        else
            container = GetContainer(eventData);    // ���� �������� �� �������� ������� ��������� � ����������� ���� ����� �������� item

        if (container != null)
            DropItemIntoContainer(container);
    }

    private IDragDestination GetContainer(PointerEventData eventData)
    {

        if (eventData.pointerEnter) //  �������� �� eventData �� UI ������ � ������� bool ����������
        {
            var container = eventData.pointerEnter.GetComponentInParent<IDragDestination>(); // ���� �� �� ���� ��������� ������� �������� � ���� item, ���� ��� ��� �� ������������ null
            return container;
        }

        return null; // ���� ��� �� ������� null
    }

    private void DropItemIntoContainer(IDragDestination destination)
    {
        if (ReferenceEquals(destination, _source))  //  ���������� �������� �� ����� ��������� ������ �����������
            return;

        var destinationContainer = destination as IDragContainer;   //  ��������� �������� �� ������ ����������� ��� item
        var sourceContainer = _source as IDragContainer;            //  ��������� �������� �� ������ ����������� ��� item

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