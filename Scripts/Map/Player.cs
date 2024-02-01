using UnityEngine;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<MiniMap>().Create(transform);
    }
}