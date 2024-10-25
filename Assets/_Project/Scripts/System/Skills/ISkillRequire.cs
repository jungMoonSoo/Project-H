/// <summary>
/// 스킬 요구사항 확인 interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISkillRequire<T>
{
    /// <summary>
    /// 스킬 요구사항을 충족했는지 확인
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public bool CheckRequire(T model);
}
