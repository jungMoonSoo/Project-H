public interface IUnidadState
{
    /// <summary>
    /// 해당 State의 소유 객체
    /// </summary>
    public Unidad Unit
    {
        get;
        set;
    }

    /// <summary>
    /// State의 소유 객체가 생성되었을 때 작업하는 Method
    /// </summary>
    public void Init();
    /// <summary>
    /// 해당 State로 StateChange가 발생했을 때 작업하는 Method
    /// </summary>
    public void OnEnter();
    /// <summary>
    /// 해당 State일 때 매 Frame마다 작업하는 Method
    /// </summary>
    public void OnUpdate();
    /// <summary>
    /// 해당 State에서 StateChange가 발생했을 때 작업하는 Method
    /// </summary>
    public void OnExit();
}
