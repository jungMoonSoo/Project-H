public class ModifierManager
{
    private readonly Unidad unidad;

    public NormalStatus NowNormalStatus => unidad.Status.statuses[level].normal;
    public AttackStatus NowAttackStatus => unidad.Status.statuses[level].attack;
    public DefenceStatus NowDefenceStatus => unidad.Status.statuses[level].defence;

    private int level = 0;

    public ModifierManager(Unidad unidad)
    {
        this.unidad = unidad;
    }

    public void SetUnitDefaltStatus(int level)
    {
        if (level < 0) level = 0;
        else if (unidad.Status.statuses.Length <= level) level = unidad.Status.statuses.Length - 1;

        this.level = level;
    }
}
