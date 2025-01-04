using System.Linq;
using UnityEngine;

public class AttackUniState: MonoBehaviour, IUnidadState
{
    [Header("Settings")]
    [SerializeField] private int audioClipNumber = -1;
    [SerializeField] private float atkAnimPoint = 0.3f;

    private bool attack;
    private bool playSound;

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
        Vector2 movePos = MapManager.Instance.ClampPositionToMap(Unit.transform.position, Unit.unitCollider.Radius);

        if ((Vector2)Unit.transform.position != movePos)
        {
            Unit.StateChange(UnitState.Move);

            return;
        }

        Unidad[] enemys = UnidadManager.Instance.GetUnidads(Unit.Owner, TargetType.They).OrderBy(unit => Vector2.Distance((Vector2)unit.transform.position + unit.unitCollider.center, transform.position)).ToArray();

        if (enemys.Length > 0)
        {
            Unidad target = enemys[0];
            Vector2 direction = target.unitCollider.transform.position - transform.position;

            Unit.transform.eulerAngles = new Vector2(0, direction.x > 0 ? 180 : 0);

            if (!Unit.attackCollider.OnEllipseEnter(target.unitCollider)) Unit.StateChange(UnitState.Move);
            else
            {
                AnimatorStateInfo state = Animator.GetCurrentAnimatorStateInfo(0);

                if (state.IsName("Attack_0"))
                {
                    CheckAttackPoint(state, target);
                    CheckSoundPoint(state);
                }
            }
        }
        else Unit.StateChange(UnitState.Move);
    }

    public void OnExit()
    {
        
    }

    private void CheckAttackPoint(AnimatorStateInfo state, Unidad target)
    {
        if (state.normalizedTime % 1 > atkAnimPoint)
        {
            if (!attack)
            {
                CallbackValueInfo<DamageType> callback = StatusCalc.CalculateFinalPhysicalDamage(Unit.NowAttackStatus, target.NowDefenceStatus, 100, 0, ElementType.None);
                target.OnDamage((int)callback.value, callback.type);

                Unit.IncreaseMp(callback.type == DamageType.Miss ? 0.5f : 1);

                attack = true;
            }
        }
        else attack = false;
    }

    private void CheckSoundPoint(AnimatorStateInfo state)
    {
        if (audioClipNumber != -1)
        {
            if (state.normalizedTime % 1 > Unit.audioHandle.GetPlayTiming(audioClipNumber))
            {
                if (!playSound)
                {
                    Unit.audioHandle.OnPlay(audioClipNumber);
                    playSound = true;
                }
            }
            else playSound = false;
        }
    }
}