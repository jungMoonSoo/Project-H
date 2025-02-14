using UnityEngine;

public class CheckJustHit : MonoBehaviour, ICheckHitable
{
    [SerializeField] private TrackingType trackingType = TrackingType.Near;

    public HitObject HitObject { get; set; }

    public void Hit()
    {
        ITrackingSystem trackingSystem = TrackingTypeHub.GetSystem(trackingType);

        if (trackingSystem.TryGetTargets(out Unidad[] targets, HitObject.Unidad.Owner, HitObject.Unidad.attackCollider)) HitObject.Attack(targets[0]);

        Remove();
    }

    private void Remove() => HitObject.Remove();
}
