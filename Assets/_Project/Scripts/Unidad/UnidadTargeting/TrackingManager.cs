using UnityEngine;

public class TrackingManager : MonoBehaviour
{
    private HitObjectManager hitObjectManager;
    private EffectManager effectManager;

    private Unidad[] targets;
    public Unidad[] Targets => targets;

    private TrackingType trackingType = TrackingType.Near;

    private void Start()
    {
        TryGetComponent(out hitObjectManager);
        TryGetComponent(out effectManager);
    }

    public void SetType(TrackingType type) => trackingType = type;

    public UnitState CheckState(Unidad unidad)
    {
        ITrackingSystem trackingSystem = TrackingTypeHub.GetSystem(trackingType);

        if (trackingSystem.TryGetTargets(out targets, unidad.Owner, unidad.attackCollider))
        {
            Unidad target = targets[0];

            FlipX(unidad.view.transform, target.transform.position.x - unidad.transform.position.x > 0);

            if (unidad.attackCollider.OnEnter(target.unitCollider)) return UnitState.Attack;
            else return UnitState.Move;
        }

        return UnitState.Idle;
    }

    public void CreateHitObject(Unidad unidad)
    {
        HitObjectBase hitObject = hitObjectManager.GetHitObject(transform);

        hitObject.SetTargetPos(targets[0].transform.position);

        hitObject.Init(unidad, effectManager, unidad.HitObjectPosition.position);
    }

    private void FlipX(Transform trans, bool right)
    {
        if (trans.localScale.x > 0 && right) return;
        if (trans.localScale.x < 0 && !right) return;

        trans.localScale = new Vector3(-trans.localScale.x, 1, 1);
    }
}
