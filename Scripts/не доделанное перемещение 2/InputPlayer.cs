using UnityEngine;

[RequireComponent(typeof(Moverent1), typeof(Rotation1))]
public class InputPlayer : MonoBehaviour
{
    private Moverent1 _moverent1;
    private Rotation1 _rotation1;

    private void Awake()
    {
        _moverent1 = GetComponent<Moverent1>();
        _rotation1 = GetComponent<Rotation1>();
    }

    private void Update()
    {

    }
}