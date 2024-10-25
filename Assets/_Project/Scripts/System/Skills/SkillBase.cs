public abstract class SkillBase<RequireT, PrepareT, PreActionT, InvokeT, AfterActionT>
{
    public abstract ISkillRequire<RequireT> SkillRequire
    {
        get;
    }
    public abstract ISkillPrepare<PrepareT> SkillPrepare
    {
        get;
    }
    public abstract ISkillPreAction<PreActionT> SkillPreAction
    {
        get;
    }
    public abstract ISkillInvoke<InvokeT> SkillInvoke
    {
        get;
    }
    public abstract ISkillAfterAction<AfterActionT> SkillAfterAction
    {
        get;
    }
}