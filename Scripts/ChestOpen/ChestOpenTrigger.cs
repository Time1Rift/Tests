using UnityEngine;

public class ChestOpenTrigger : MonoBehaviour
{
    [SerializeField] private Chest _chest;

    private bool _isOpen = false;
    private bool _hasOpen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ChestOpen>())
            _hasOpen = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ChestOpen>())
            _hasOpen = false;
    }

    private void Update()
    {
        if (_isOpen)
            return;

        if (_hasOpen && Input.GetKey(KeyCode.E))
        {
            _chest.Open();
            _isOpen = true;
        }
    }
}