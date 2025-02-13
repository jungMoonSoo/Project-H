using UnityEngine;

public class CheckParabolaHit : MonoBehaviour, ICheckHitable
{
    [SerializeField] private EllipseCollider hitCollider;
    [SerializeField] private TrackingType trackingType = TrackingType.Near;

    [SerializeField] private float height = 5;
    [SerializeField] private float endYPos = 0;

    public HitObject HitObject { get; set; }

    private bool init;

    private Vector3 startPos;
    private Vector3 targetPos;

    private float timer;

    public void Hit()
    {
        if (!init) Init();

        if (MoveParabola()) return;

        ITrackingSystem trackingSystem = TrackingTypeHub.GetSystem(trackingType);

        if (trackingSystem.TryGetTargets(out Unidad[] targets, HitObject.Unidad.Owner, hitCollider))
        {
            Unidad target = targets[0];

            if (hitCollider.OnEllipseEnter(target.unitCollider)) HitObject.Attack(target);
        }

        Remove();
    }

    private void Remove()
    {
        init = false;

        HitObject.Remove();
    }

    private void Init()
    {
        init = true;

        timer = 0;

        transform.position = HitObject.Unidad.transform.position + Vector3.up;

        startPos = transform.position;
        targetPos = HitObject.TargetPos;
    }

    private bool MoveParabola()
    {
        if (transform.position.y >= endYPos)
        {
            timer += Time.deltaTime;

            Vector3 pos = Vector3.Lerp(startPos, targetPos, timer);

            pos.y = -4 * height * timer * timer + 4 * height * timer + Mathf.Lerp(startPos.y, endYPos, timer);

            transform.position = pos;

            return true;
        }

        return false;
    }
}
