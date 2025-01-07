using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/PassiveRequire/ElementPassiveRequire", fileName = "NewElementPassiveRequire")]
public class ElementMemberPassiveRequire: PassiveRequireBase
{
    [Header("패시브 발동 조건")]
    [SerializeField] private int minMembers = 1;
    
    public override PassiveRequireType RequireType => PassiveRequireType.ElementMember;
    public override float RequireValue => minMembers;


    public override bool IsRequired(Unidad owner)
    {
        List<Unidad> allys = UnidadManager.Instance.GetUnidads(owner.Owner, TargetType.We);

        return true;
    }
}