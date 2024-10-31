using UnityEngine;

public interface IPlayerSkill: ISkill
{
    public GameObject AreaEffect
    {
        get;
    }

    public float CoolDown
    {
        get;
    }
    public bool IsCooled
    {
        get;
    }

    public void OnSelect();
    public void OnDrag(Vector2 position);
    public void EndAction();
}