using UnityEngine;

public class Fields : MonoBehaviour
{
    private Fields1 fields1;

    private void Awake()
    {
        fields1 = GetComponent<Fields1>();
    }

    private void OnEnable()
    {
        fields1.Speed += Show;
    }

    private void OnDisable()
    {
        fields1.Speed -= Show;
    }

    private void Show(float speed)
    {
        Debug.Log(speed);
        Debug.Log(Mathf.Abs(-20.6f));
        Debug.Log(Mathf.Abs(80f));
        Debug.Log(Mathf.Abs(0f));
        Debug.Log(Vector3.forward);
    }
}