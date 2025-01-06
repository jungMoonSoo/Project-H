using UnityEngine;

[CreateAssetMenu(menuName = "UnitModifier/TestDoT", fileName = "NewTestDoT")]
public class TestDoT : NormalDoTModifier
{
    public override int Cycle(Unidad unidad)
    {
        base.Cycle(unidad);

        return 2;
    }
}
