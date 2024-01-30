using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private Abillity _abillity;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _abillity.ApplyAction(_abillity.SelectTargets(Camera.main.ScreenPointToRay(Input.mousePosition).origin));
    }
}