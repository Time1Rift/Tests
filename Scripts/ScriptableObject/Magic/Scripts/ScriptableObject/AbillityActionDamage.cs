using UnityEngine;

[CreateAssetMenu(menuName = "Abillities/Action/Damage")]
public class AbillityActionDamage : AbillityAction
{
    [SerializeField] private float _damage;

    public override void Action(Unit unit)
    {
        unit.TakeDamage(_damage);
    }
}