/// <summary>
/// 스킬 실제 동작
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISkillInvoke<T>
{
    /// <summary>
    /// 스킬 실제 동작
    /// </summary>
    /// <param name="model"></param>
    public void Action(T model);
}
