using UnityEngine;

public class Characterion1 : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private void Start()
    {
        GameObject item = Instantiate(_prefab, transform.position, Quaternion.identity);
    }
}