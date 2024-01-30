using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abillities/Place Logic/Single Target")]
public class AbilityPlaceLogicSingleTarget : AbilityPlaceLogic
{
    public override List<Unit> TryGetTargets(Vector3 screenPoint)
    {
        if (Physics.Raycast(screenPoint, Camera.main.ScreenPointToRay(Input.mousePosition).direction, out RaycastHit hit))
            if (hit.transform.GetComponent<Unit>())
                return new List<Unit>() { hit.transform.GetComponent<Unit>() };

        return null;
    }
}