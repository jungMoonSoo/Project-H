using System.Linq;
using UnityEngine;

public class AttackUniState: MonoBehaviour, IUnidadState
{
    [Header("Settings")]
    [SerializeField] private float atkAnimPoint = 0.3f;

    private bool attack;

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

    public void OnEnter()
    {
        Animator.Play("Attack_0");
    }

    public void OnUpdate()
    {
        Unidad[] enemys = UnidadManager.Instance.unidades.Where(x => Unit.Owner != x.Owner).OrderBy(unit => Vector2.Distance((Vector2)unit.transform.position + unit.unitCollider.center, transform.position)).ToArray();

        if (enemys.Length > 0)
        {
            Unidad target = enemys[0];
            Vector2 direction = target.unitCollider.transform.position - transform.position;

            Unit.transform.eulerAngles = new Vector2(0, direction.x > 0 ? 180 : 0);

            if (!Unit.attackCollider.OnEllipseEnter(target.unitCollider))
            {
                Unit.StateChange(UnitState.Move);
            }
            else
            {
                AnimatorStateInfo state = Animator.GetCurrentAnimatorStateInfo(0);

                if (state.IsName("Attack_0"))
                {
                    if (state.normalizedTime % 1 > atkAnimPoint)
                    {
                        if (!attack)
                        {
                            CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalDamage(Unit.NowAttackStatus, target.NowDefenceStatus, 100, 0, false, null);
                            target.OnDamage((int)callback.value, callback.type);

                            Unit.IncreaseMp(callback.type == DamageType.Miss ? StatusCalc.MP_REGEN * 0.5f : StatusCalc.MP_REGEN);

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