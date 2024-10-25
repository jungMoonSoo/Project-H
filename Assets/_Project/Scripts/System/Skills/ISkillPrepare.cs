/// <summary>
/// 스킬의 기술적 사전작업 interface<br/>
/// 스킬 사용 시 게임을 일시정지하는 등의 기능을 구현하는 interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISkillPrepare<T>
{
    /// <summary>
    /// 스킬 선택
    /// </summary>
    /// <param name="model"></param>
    public void Begin(T model);
    /// <summary>
    /// 스킬 취소 및 사용
    /// </summary>
    /// <param name="model"></param>
    public void End(T model);
}
