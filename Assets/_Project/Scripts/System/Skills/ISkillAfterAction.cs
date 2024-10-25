/// <summary>
/// 스킬 시전 후 사후 행동 interface
/// </summary>
public interface ISkillAfterAction<T>
{
    /// <summary>
    /// 사후 행동
    /// </summary>
    /// <param name="model"></param>
    public void Action(T model);
}
