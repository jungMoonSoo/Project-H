using System.Linq;
using UnityEngine;

public class AttackUniState: MonoBehaviour, IUnidadState
{
    public Unidad Unit
    {
        get;
        set;
    }
    public Animator Animator
    {
        get;
        set;
    }
    public float AtkAnimPoint
    {
        get;
        set;
    }

    private bool attack;

    public void OnEnter()
    {
        Animator.Play("Attack_0");
    }

    public void OnUpdate()
    {
        Unidad[] enemys = GameObject.FindObjectsOfType<Unidad>().Where(x => Unit.Owner != x.Owner).ToArray();
        if (enemys.Length > 0)
        {
            Unidad target = enemys[0];
            if (!Unit.attackCollider.OnEllipseEnter(target.unitCollider))
            {
                Unit.StateChange(UnitState.Move);
            }
            else
            {
                AnimatorStateInfo state = Animator.GetCurrentAnimatorStateInfo(0);

                if (state.IsName("Attack_0"))
                {
                    if (state.normalizedTime % 1 > AtkAnimPoint)
                    {
                        if (!attack)
                        {
                            attack = true;
                        }
                    }
                    else
                    {
                         attack = false;
                    }
                }
            }
        }
    }

    public void OnExit()
    {
        
    }
}