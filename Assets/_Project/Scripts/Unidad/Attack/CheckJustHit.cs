using UnityEngine;

public class CheckJustHit : MonoBehaviour, ICheckHitable
{
    [SerializeField] private TrackingType trackingType = TrackingType.Near;

    public HitObject HitObject { get; set; }

    public void Hit()
    {
        ITrackingSystem trackingSystem = TrackingTypeHub.GetSystem(trackingType);

        if (trackingSystem.TryGetTargets(out Unidad[] targets, HitObject.Unidad.Owner, HitObject.Unidad.attackCollider, 1)) HitObject.Attack(targets[0]);

        Remove();
    }

    public void Remove() => HitObject.Remove();
}
