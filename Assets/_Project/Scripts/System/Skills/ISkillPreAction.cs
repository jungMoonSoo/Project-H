/// <summary>
/// 스킬 시전 시 사전 행동 interface
/// </summary>
public interface ISkillPreAction<T>
{
    /// <summary>
    /// 사전 행동
    /// </summary>
    /// <param name="model"></param>
    public void Action(T model);
}
