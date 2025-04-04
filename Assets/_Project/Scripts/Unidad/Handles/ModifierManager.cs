public class ModifierManager
{
    private readonly Unidad unidad;

    public NormalStatus NowNormalStatus => unidad.Status.statuses[level].normal;
    public AttackStatus NowAttackStatus => unidad.Status.statuses[level].attack;
    public DefenceStatus NowDefenceStatus => unidad.Status.statuses[level].defence;

    private uint level = 0;

    public ModifierManager(Unidad unidad)
    {
        this.unidad = unidad;
    }

    public void SetUnitDefaltStatus(uint level)
    {
        if (unidad.Status.statuses.Length <= level) level = (uint)(unidad.Status.statuses.Length - 1);

        this.level = level;
    }
}
