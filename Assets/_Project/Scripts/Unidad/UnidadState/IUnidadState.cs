public interface IUnidadState
{
    public Unidad Unit
    {
        get;
        set;
    }

    public void Init();
    public void OnEnter();
    public void OnUpdate();
    public void OnExit();
}
