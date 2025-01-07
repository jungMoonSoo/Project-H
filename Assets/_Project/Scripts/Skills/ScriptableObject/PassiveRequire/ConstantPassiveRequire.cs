using UnityEngine;

[CreateAssetMenu(menuName = "Skill/PassiveRequire/ConstantPassiveRequire", fileName = "ConstantPassiveRequire")]
public class ConstantPassiveRequire: PassiveRequireBase
{
    public override PassiveRequireType RequireType => PassiveRequireType.Constant;
    public override float RequireValue => 0;


    public override bool IsRequired(Unidad owner) => true;
}