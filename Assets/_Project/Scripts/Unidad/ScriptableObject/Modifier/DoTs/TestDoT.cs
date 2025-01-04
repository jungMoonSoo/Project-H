using UnityEngine;

[CreateAssetMenu(menuName = "UnitModifier/TestDoT", fileName = "NewTestDoT")]
public class TestDoT : NormalDoTModifier
{
    public override int Check(Unidad unidad)
    {
        base.Check(unidad);

        return 2;
    }
}
