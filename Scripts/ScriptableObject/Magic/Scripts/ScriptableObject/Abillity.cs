using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abillities/new Ability")]
public class Abillity : ScriptableObject
{
    [SerializeField] private AbilityPlaceLogic _abilityPlaceLogic;
    [SerializeField] private List<AbillityAction> _abillityAction;

    public void ApplyAction(List<Unit> units)
    {
        if (units != null)
            foreach (AbillityAction action in _abillityAction)
                foreach (Unit unit in units)
                    action.Action(unit);
    }

    public List<Unit> SelectTargets(Vector3 screenPoint)
    {
        return _abilityPlaceLogic.TryGetTargets(screenPoint);
    }
}