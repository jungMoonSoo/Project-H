public interface IPhaseState
{
    /// <summary>
    /// 해당 State로 전환됐을 때 동작
    /// </summary>
    public void OnEnter();
    /// <summary>
    /// State일 때 매 프레임마다 동작
    /// </summary>
    public void OnUpdate();
    /// <summary>
    /// 해당 State에서 전환됐을 때
    /// </summary>
    public void OnExit();
}